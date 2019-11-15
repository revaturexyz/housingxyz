using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.DataAccess.Repository
{
  public class AddressRepository : IAddressRepository
  {
    private readonly RevatureHousingDbContext _dbContext;
    private readonly ILogger _logger;

    public AddressRepository(RevatureHousingDbContext dbContext, ILogger<AddressRepository> logger = null)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
      _logger = logger;
    }

    /// <summary>
    /// Asynchronously adds address to database
    /// </summary>
    /// <param name="newEntity">A Library Address object</param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Address model</returns>
    /// NOTE: THIS METHOD IS NOT USED IN CURRENT SCOPE OF PROJECT.
    public async Task<Lib.Models.Address> AddAsync(Lib.Models.Address newEntity, int providerId)
    {
      var alreadyAdded = await _dbContext.Address
        .AnyAsync(a => a.StreetAddress == newEntity.StreetAddress && a.City == newEntity.City && a.State == newEntity.State)
        .ConfigureAwait(false);
      if (alreadyAdded)
      {
        _logger?.LogWarning("Address has already been previously added. Duplicate data can't be added.");
        throw new InvalidOperationException($"{newEntity.StreetAddress} {newEntity.City}, {newEntity.State} has already been added.");
      }

      try
      {
        // Adding a new Address into Address table.
        var newAddress = Mapper.Map(newEntity);
        _dbContext.Address.Add(newAddress);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        newEntity.AddressId = newAddress.AddressId;
        return newEntity;
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in AddAsync Address repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously retrieves an address from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task-wrapped Library Address model</returns>
    public async Task<Lib.Models.Address> GetAsync(int id)
    {
      if (id <= 0)
      {
        _logger?.LogWarning("Invalid input ID.");
        throw new ArgumentOutOfRangeException(nameof(id), id, "ID must be positive.");
      }
      var address = await _dbContext.Address.FirstOrDefaultAsync(r => r.AddressId == id).ConfigureAwait(false);
      if (address is null)
      {
        _logger?.LogWarning("No addresses with that ID.");
        throw new ArgumentNotFoundException("Address", id, nameof(id));
      }
      var resultAddress = Mapper.Map(address);
      return resultAddress;
    }

    /// <summary>
    /// Asynchronously retrieves addresses associated with a complex
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Address models</returns>
    public async Task<IEnumerable<Lib.Models.Address>> GetAddressesByComplexIdAsync(int complexId)
    {
      if (complexId <= 0)
      {
        _logger?.LogWarning($"Complex ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(complexId), complexId, "Complex ID cannot be less than 1.");
      }
      var complex = await _dbContext.Complex
        .Include(c => c.Address)
        .FirstOrDefaultAsync(c => c.ComplexId == complexId)
        .ConfigureAwait(false);
      if (complex is null)
      {
        _logger?.LogWarning($"No Complexes with that complexId");
        throw new ArgumentNotFoundException("Complex", complexId, nameof(complexId));
      }
      var rooms = await _dbContext.Room
        .Include(c => c.Address)
        .Where(r => r.ComplexId == complexId)
        .ToListAsync()
        .ConfigureAwait(false);
      var addressList = new List<Address>();
      if (rooms.Count > 0)
      {
        foreach (var room in rooms)
        {
          if (!addressList.Any(r => r.AddressId == room.Address.AddressId)) // Stops redundant addresses
          {
            addressList.Add(room.Address);
          }
        }
        if (!addressList.Any(c => c.AddressId == complex.Address.AddressId))
        {
          addressList.Add(complex.Address);
        }
      }
      else
      {
        _logger?.LogWarning($"No rooms with that address in database");
      }
      var result = addressList.Select(Mapper.Map);
      return result;
    }

    /// <summary>
    /// Asynchronously retrieves addresses associated with a provider
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Address models</returns>
    public async Task<IEnumerable<Lib.Models.Address>> GetAddressesByProviderIdAsync(int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning($"Provider ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "Provider ID cannot be less than 1");
      }
      var providerExists = await _dbContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false);
      if (!providerExists)
      {
        _logger?.LogWarning($"No provider with that provider ID");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var complexes = await _dbContext.Complex
        .Include(c => c.Address)
        .Include(c => c.Provider)
          .ThenInclude(p => p.Address)
        .Where(c => c.ProviderId == providerId)
        .ToListAsync()
        .ConfigureAwait(false);
      var addressList = new List<Address>();
      if (complexes.Count > 0)
      {
        // this is probably needlessly chatty & better as one query
        foreach (var complex in complexes)
        {
          var compId = complex.ComplexId;
          var rooms = await _dbContext.Room
            .Include(r => r.Address)
            .Where(r => r.ComplexId == compId)
            .ToListAsync()
            .ConfigureAwait(false);
          if (rooms.Count > 0)
          {
            foreach (var room in rooms)
            {
              if (!addressList.Any(r => r.AddressId == room.Address.AddressId)) // Stops redundant addresses
              {
                addressList.Add(room.Address);
              }
            }
            if (!addressList.Any(c => c.AddressId == complex.Address.AddressId)) // Stops redundant addresses
            {
              addressList.Add(complex.Address);
            }
          }
        }
      }
      // Else: there are no complexes matching
      else
      {
        _logger?.LogWarning($"No matching complexes in database");
      }
      var result = addressList.Select(Mapper.Map);
      return result;
    }

    /// <summary>
    /// Asynchronously retrieves all addressess from database
    /// </summary>
    /// <returns>Task-wrapped IEnumerable of Library Address models</returns>
    public async Task<IEnumerable<Lib.Models.Address>> GetAllAsync()
    {
      var addresses = await _dbContext.Address.ToListAsync().ConfigureAwait(false);
      return addresses.Select(Mapper.Map);
    }

    /// <summary>
    /// Asynchronously removes address from database
    /// </summary>
    /// <param name="id">address's ID</param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    /// NOTE: THIS METHOD IS NOT USED IN CURRENT SCOPE OF PROJECT.
    public async Task RemoveAsync(int id, int providerId)
    {
      if (id <= 0 || providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "Negative ID is not valid.");
      }
      // we don't need to check the provider - this is only here because of IRepository...
      var providerExists = await _dbContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false);
      if (!providerExists)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var address = await _dbContext.Address.FirstOrDefaultAsync(r => r.AddressId == id).ConfigureAwait(false);
      if (address is null)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Address", id, nameof(id));
      }
      try
      {
        _dbContext.Address.Remove(address);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in RemoveAsync for Address repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously updates address in database
    /// </summary>
    /// <param name="newEntity">A new modified address</param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    /// NOTE: THIS METHOD IS NOT USED IN CURRENT SCOPE OF PROJECT.
    public async Task UpdateAsync(Lib.Models.Address newEntity, int providerId)
    {
      if (newEntity.AddressId <= 0 || providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "Negative ID is not valid");
      }
      // we don't need to check the provider - this is only here because of IRepository...
      var providerExists = await _dbContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false);
      if (!providerExists)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var previousAddress = await _dbContext.Address.FirstOrDefaultAsync(r => r.AddressId == newEntity.AddressId).ConfigureAwait(false);
      if (previousAddress is null)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Address", newEntity.AddressId, nameof(newEntity));
      }
      try
      {
        previousAddress.StreetAddress = newEntity.StreetAddress;
        previousAddress.City = newEntity.City;
        previousAddress.State = newEntity.State;
        previousAddress.Zip = newEntity.Zip;
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in UpdateAsync for Address repo: {e.Message}.");
        throw;
      }
    }
  }
}
