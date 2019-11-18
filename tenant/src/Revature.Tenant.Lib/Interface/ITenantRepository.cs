using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Tenant.Lib.Interface
{
  /// <summary>
  /// An interface that manages data access for tenants and their cars.
  /// </summary>
  public interface ITenantRepository : IDisposable
  {
    /// <summary>
    /// Adds a new tenant object as well as its associated properties.
    /// </summary>
    /// <param name="tenant">The Tenant</param>
    public Task AddAsync(Models.Tenant tenant);

    /// <summary>
    /// Gets a tenant using their id.
    /// </summary>
    /// <param name="id">The ID of the tenant</param>
    /// <returns>A tenant</returns>
    public Task<Models.Tenant> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets a list of all tenants
    /// </summary>
    /// <returns>The collection of all tenants</returns>
    public Task<ICollection<Models.Tenant>> GetAllAsync();

    /// <summary>
    /// Deletes a tenant using their id.
    /// </summary>
    /// <param name="id">The ID of the tenant</param>
    public Task DeleteByIdAsync(Guid id);

    /// <summary>
    /// Updates values associated to a tenant.
    /// </summary>
    /// <param name="tenant">The tenant with changed values</param>
    public Task UpdateAsync(Models.Tenant tenant);

    /// <summary>
    /// This persists changes to data base. 
    /// </summary>
    public Task SaveAsync();
  }
}
