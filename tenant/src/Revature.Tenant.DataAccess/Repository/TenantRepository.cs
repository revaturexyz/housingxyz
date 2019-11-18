using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revature.Tenant.DataAccess.Entities;
using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess.Repository
{
  /// <summary>
  /// A repository for managing data access for tenant onjects and their cars.
  /// </summary>
  public class TenantRepository : ITenantRepository 
  {
    private readonly TenantsContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new tenant repository given a suitable tenant data source.
    /// </summary>
    /// <param name="context">The data source</param>
    /// <param name="mapper">The mapper</param>
    public TenantRepository(TenantsContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Adds a new tenant object as well as its associated properties.
    /// </summary>
    /// <param name="tenant">The Tenant</param>
    public async Task AddAsync(Lib.Models.Tenant tenant)
    {
      Tenants newTenant = _mapper.MapTenant(tenant);

      await _context.Tenants.AddAsync(newTenant);
    }

    /// <summary>
    /// Gets a tenant using their id.
    /// </summary>
    /// <param name="id">The ID of the tenant</param>
    /// <returns>A tenant</returns>
    public async Task<Lib.Models.Tenant> GetByIdAsync(int id)
    {
      Tenants tenant = await _context.Tenants.Include(t => t.Cars).FirstAsync(t => t.Id == id);

      if (tenant == null)
      {
        throw new ArgumentException();
      }

      return _mapper.MapTenant((tenant));
    }

    /// <summary>
    /// Gets a list of all tenants
    /// </summary>
    /// <returns>The collection of all tenants</returns>
    public async Task<ICollection<Lib.Models.Tenant>> GetAllAsync()
    {
      List<Tenants> tenants = await _context.Tenants.Include(t => t.Cars).AsNoTracking().ToListAsync();

      return tenants.Select((_mapper.MapTenant)).ToList();
    }

    /// <summary>
    /// Deletes a tenant using their id.
    /// </summary>
    /// <param name="id">The ID of the tenant</param>
    public async Task DeleteByIdAsync(int id)
    {
      Tenants tenant = await _context.Tenants.FindAsync(id);

      _context.Remove(tenant);
    }

    /// <summary>
    /// Updates values associated to a tenant.
    /// </summary>
    /// <param name="tenant">The tenant with changed values</param>
    public async Task UpdateAsync(Lib.Models.Tenant tenant)
    {
      Tenants currentTenant = await _context.Tenants.FindAsync(tenant.Id);

      if (currentTenant == null)
      {
        throw new InvalidOperationException("Invalid Tenant Id");
      }

      Tenants newTenant = _mapper.MapTenant(tenant);
      _context.Entry(currentTenant).CurrentValues.SetValues(newTenant);
    }

    /// <summary>
    /// This persists changes to data base. 
    /// </summary>
    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }

    #region IDisposable Support
    private bool _disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
        }

        _disposedValue = true;
      }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      Dispose(true);
    }
    #endregion
  }
}
