using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.DataAccess.Repository
{
  public class ProviderRepository : IProviderRepository
  {
    private readonly RevatureHousingDbContext _dbContext;
    private readonly ILogger _logger;

    public ProviderRepository(RevatureHousingDbContext dbContext, ILogger<ProviderRepository> logger = null)
    {
      _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
      _logger = logger;
    }

    /// <summary>
    /// Asynchronously adds a provider to database
    /// </summary>
    /// <param name="newEntity">An Entity Provider object</param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Provider model</returns>
    public async Task<Lib.Models.Provider> AddAsync(Lib.Models.Provider newEntity, int providerId)
    {
      if (newEntity.Address is null)
      {
        _logger?.LogWarning($"{nameof(newEntity.Address)} cannot be null.");
        throw new ArgumentException("Provider address cannot be null.", nameof(newEntity));
      }
      if (newEntity.Center is null)
      {
        _logger?.LogWarning($"{nameof(newEntity.Center)} cannot be null.");
        throw new ArgumentException($"Provider training center cannot be null.", nameof(newEntity));
      }
      var addressId = newEntity.Address.AddressId;
      if (await _dbContext.Address.AnyAsync(r => r.AddressId == addressId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Invalid Address ID: Address already exists");
        throw new InvalidOperationException($"Provider address {addressId} already exists.");
      }
      try
      {
        // Adding a new Provider.
        var maxProviderId = await _dbContext.Provider.MaxAsync(p => p.ProviderId).ConfigureAwait(false);
        var newPro = new Entities.Provider
        {
          Username = newEntity.Username,
          Password = newEntity.Password,
          CompanyName = newEntity.CompanyName,
          ProviderId = maxProviderId + 1,
          ContactNumber = newEntity.ContactNumber,
        };
        var address = newEntity.Address;
        if (addressId == 0)
        {
          // creates a new address if none are given
          newPro.Address = new Address
          {
            StreetAddress = address.StreetAddress,
            City = address.City,
            State = address.State,
            Zip = address.Zip,
          };
        }
        else
        {
          newPro.AddressId = address.AddressId;
        }
        var center = newEntity.Center;
        if (center.CenterId == 0)
        {
          newPro.Center = new TrainingCenter
          {
            // adds a new address if it doesn't have one
            CenterName = center.CenterName,
            ContactNumber = center.ContactNumber,
            Address = center.Address is null ? null : new Address
            {
              StreetAddress = center.Address.StreetAddress,
              City = center.Address.City,
              State = center.Address.State,
              Zip = center.Address.Zip,
            }
          };
        }
        else
        {
          newPro.CenterId = center.CenterId;
        }
        _dbContext.Provider.Add(newPro);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        newEntity.ProviderId = newPro.ProviderId;
        return Mapper.Map(newPro);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in AddAsync Provider repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously retrieves a specific Provider from database
    /// </summary>
    /// <param name="id">Provider ID</param>
    /// <returns>Task-wrapped Library Provider model</returns>
    public async Task<Lib.Models.Provider> GetAsync(int id)
    {
      if (id <= 0)
      {
        _logger?.LogWarning($"Provider ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(id), id, "Provider ID cannot be less than 1");
      }
      var result = await _dbContext.Provider
        .Include(p => p.Center)
          .ThenInclude(c => c.Address)
        .FirstOrDefaultAsync(p => p.ProviderId == id)
        .ConfigureAwait(false);
      if (result is null)
      {
        _logger?.LogWarning($"No providers with that provider ID");
        throw new ArgumentNotFoundException("Provider", id, nameof(id));
      }
      var aProvider = Mapper.Map(result);
      aProvider.Center = Mapper.Map(result.Center);
      aProvider.Center.Address = Mapper.Map(result.Center.Address);
      return aProvider;
    }

    /// <summary>
    /// Asynchronously retrieves all providers from database
    /// </summary>
    /// <returns>Task-wrapped IEnumerable of Library Provider models</returns>
    public async Task<IEnumerable<Lib.Models.Provider>> GetAllAsync()
    {
      var pro = await _dbContext.Provider.ToListAsync().ConfigureAwait(false);
      if (pro.Count == 0)
      {
        _logger?.LogWarning("No providers in database");
      }
      return Mapper.Map(pro);
    }

    public Task RemoveAsync(int id, int providerId)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously updates a provider in database
    /// </summary>
    /// <param name="newEntity"> An Entity Provider object</param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    public async Task UpdateAsync(Lib.Models.Provider newEntity, int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid provider ID input");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID cannot be less than 1.");
      }
      var provider = await _dbContext.Provider
        .FirstOrDefaultAsync(p => p.ProviderId == providerId)
        .ConfigureAwait(false);
      if (provider is null)
      {
        _logger?.LogWarning("Provider not found in the database");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      try
      {
        provider.CompanyName = newEntity.CompanyName;
        provider.ContactNumber = newEntity.ContactNumber;
        provider.Username = newEntity.Username;
        provider.Password = newEntity.Password;

        await _dbContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in RemoveAsync for Room repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously retrieves a training center associated with a specific provider from databse
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library TrainingCenter model</returns>
    public async Task<Lib.Models.TrainingCenter> GetTrainingCenterByProviderIdAsync(int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning($"Provider ID cannot be less than 1");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID cannot be less than 1.");
      }
      if (!await _dbContext.Provider.AnyAsync(r => r.ProviderId == providerId))
      {
        _logger?.LogWarning($"No provider with that provider ID");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var prov = await _dbContext.Provider
        .Include(p => p.Center)
          .ThenInclude(p => p.Address)
        .FirstOrDefaultAsync(p => p.ProviderId == providerId)
        .ConfigureAwait(false);
      return Mapper.Map(prov.Center);
    }

    /// <summary>
    /// Asynchronously retrieves all training centers from database
    /// </summary>
    /// <returns>Task-wrapped IEnumerable of Library TrainingCenter models</returns>
    public async Task<IEnumerable<Lib.Models.TrainingCenter>> GetAllTrainingCentersAsync()
    {
      var result = await _dbContext.TrainingCenter.Include(c => c.Address).ToListAsync();
      if (result.Count == 0)
      {
        _logger?.LogWarning("No training centers in database");
      }
      return Mapper.Map(result);
    }
  }
}
