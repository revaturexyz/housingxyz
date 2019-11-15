using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Revature.Tenant.DataAccess.Entities;
using Revature.Tenant.Lib.Interface;

namespace Revature.Tenant.DataAccess.Repository
{

  public class TenantRepository : ITenantRepository 
  {
    private readonly TenantsContext _context;
    private readonly IMapper _mapper;

    public TenantRepository(TenantsContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task AddAsync(Lib.Models.Tenant tenant)
    {
      Tenants newTenant = _mapper.MapTenant(tenant);

      await _context.Tenants.AddAsync(newTenant);
    }
    public async Task<Lib.Models.Tenant> GetByIdAsync(int id)
    {
      Tenants tenant = await _context.Tenants.FindAsync(id);

      if (tenant == null)
      {
        throw new ArgumentException();
      }

      return _mapper.MapTenant((tenant));
    }
    public Task<ICollection<Lib.Models.Tenant>> GetAllAsync()
    {
      List<Tenants> tenants = _context.Tenants.AsNoTracking().ToListAsync();
      return tenants.Select(t => new Lib.Models.Tenant
      {
        Id = 
      })
    }
    public Task DeleteByIdAsync(int id)
    {
      throw new NotImplementedException();
    }
    public Task UpdateAsync(Lib.Models.Tenant tenant)
    {
      throw new NotImplementedException();
    }
    public Task SaveAsync()
    {
      throw new NotImplementedException();
    }
    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
