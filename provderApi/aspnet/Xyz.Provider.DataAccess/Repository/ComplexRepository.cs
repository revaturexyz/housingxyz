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
  public class ComplexRepository : IComplexRepository
  {
    private readonly RevatureHousingDbContext _dbContext;
    private readonly ILogger _logger;

    public ComplexRepository(RevatureHousingDbContext dbContext, ILogger<ComplexRepository> logger = null)
    {
      _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
      _logger = logger;
    }

    /// <summary>
    /// Asynchronously adds a complex to database
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Complex model</returns>
    public async Task<Lib.Models.Complex> AddAsync(Lib.Models.Complex newEntity, int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid Provider ID: ID must be greater than zero.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, $"{providerId} is not a valid ID as it needs to be greater than zero.");
      }
      if (!await _dbContext.Provider.AnyAsync(c => c.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Invalid Provider ID: Provider with same ID not found.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var address = newEntity.Address;
      if (await _dbContext.Complex.AnyAsync(c => c.AddressId == address.AddressId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Invalid Address ID: Address belongs to another complex."); // check for repeat addresses
        throw new InvalidOperationException($"Address {address.AddressId} already exists.");
      }
      try
      {
        var newComp = new Complex
        {
          ComplexName = newEntity.ComplexName,
          ContactNumber = newEntity.ContactNumber,
          ProviderId = providerId,
          ComplexId = newEntity.ComplexId
        };
        // Create a new address if address ID is 0.
        if (address.AddressId == 0)
        {
          newComp.Address = new Address
          {
            StreetAddress = address.StreetAddress,
            City = address.City,
            State = address.State,
            Zip = address.Zip,
          };
        }
        // Use the address from the table.
        else
        {
          newComp.AddressId = address.AddressId;
        }
        _dbContext.Complex.Add(newComp);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        newEntity.ComplexId = newComp.ComplexId;
        return newEntity;
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in AddAsync Provider repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously retrieves a complex from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task-wrapped Library Complex model</returns>
    public async Task<Lib.Models.Complex> GetAsync(int id)
    {
      if (id <= 0)
      {
        _logger?.LogWarning($"Complex ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(id), id, "Complex ID cannot be less than 1");
      }
      if (!await _dbContext.Complex.AnyAsync(c => c.ComplexId == id).ConfigureAwait(false))
      {
        _logger?.LogWarning($"No complex with that complex ID");
        throw new ArgumentNotFoundException("Complex", id, nameof(id));
      }
      var complex = await _dbContext.Complex
        .Include(c => c.Address)
        .FirstOrDefaultAsync(c => c.ComplexId == id)
        .ConfigureAwait(false);
      return Mapper.Map(complex);
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously retrieves all complexes from database
    /// </summary>
    /// <returns>Task-wrapped IEnumerable of Library Complex models</returns>
    public Task<IEnumerable<Lib.Models.Complex>> GetAll()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously retrieves complexes with an associated provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Complex models</returns>
    public async Task<IEnumerable<Lib.Models.Complex>> GetComplexesByProviderIdAsync(int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning($"Provider ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "Provider ID cannot be less than 1");
      }
      if (!await _dbContext.Provider.AnyAsync(r => r.ProviderId == providerId))
      {
        _logger?.LogWarning($"No Provider with that provider ID");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      if (!await _dbContext.Complex.AnyAsync().ConfigureAwait(false))
      {
        _logger?.LogWarning($"No complexes in database");
      }
      // only returning the information the front end needs
      var complexes = await _dbContext.Complex
        .Include(c => c.Address)
        .Include(c => c.Provider)
        .Where(c => c.ProviderId == providerId)
        .ToListAsync()
        .ConfigureAwait(false);
      if (complexes.Count == 0)
      {
        _logger?.LogWarning($"No complexes matching that providerId");
      }
      return complexes.Select(Mapper.Map);
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously removes complex from database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    /// NOTE: THIS METHOD IS NOT USED IN CURRENT SCOPE OF PROJECT.
    public Task RemoveAsync(int id, int providerId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// NOT IMPLEMENTED - Asynchronously updates complex in database
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    public Task UpdateAsync(Lib.Models.Complex newEntity, int providerId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously retrieves amenities by associated complex ID
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Amenity models</returns>
    // this doesn't make any sense - returns every amenity in some room anywhere, not just in this complex
    public async Task<IEnumerable<Lib.Models.Amenity>> GetAmenitiesByComplexIdAsync(int complexId)
    {
      if (complexId <= 0)
      {
        _logger?.LogWarning($"Complex ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(complexId), complexId, "Complex ID cannot be less than 1");
      }
      if (!await _dbContext.Complex.AnyAsync(c => c.ComplexId == complexId).ConfigureAwait(false))
      {
        _logger?.LogWarning($"No complexes with that complexId");
        throw new ArgumentNotFoundException("Complex", complexId, nameof(complexId));
      }
      var rooms = await _dbContext.Room.Where(r => r.ComplexId == complexId).ToListAsync().ConfigureAwait(false);

      // filtering this query by ra.Room.ComplexId would fix the issue, I think
      var ramens = await _dbContext.RoomAmenity
        .Include(ra => ra.Amenity)
        .Include(ra => ra.Room)
        .ToListAsync()
        .ConfigureAwait(false);
      var amenList = new List<Amenity>();
      // wasteful pointless looping?
      foreach (var room in rooms) // for each room in the complex
      {
        foreach (var roar in room.RoomAmenity) // and each individual amenity globally
        {
          // Stops redundant amenities
          if (!amenList.Any(r => r.AmenityId == roar.AmenityId)) // if the amenity type is not in the list
          {
            amenList.Add(ramens.FirstOrDefault(r => r.AmenityId == roar.AmenityId).Amenity); // add the amenity type
          }
        }
      }
      var result = amenList.Select(Mapper.Map);
      return result;
    }
  }
}
