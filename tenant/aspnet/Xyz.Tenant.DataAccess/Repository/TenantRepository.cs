using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyz.Tenant.Lib.Interface;
using Xyz.Tenant.Lib.Models;

namespace Xyz.Tenant.DataAccess.Repository
{
  public class TenantRepository : ITenantRepository 
  {
    public Task AddAsync()
    {
      throw new NotImplementedException();     
    }
    public Task<Lib.Models.Tenant> GetByIdAsync(int id)
    {
      throw new NotImplementedException();
    }
    public Task<ICollection<Lib.Models.Tenant>> GetAllAsync()
    {
      throw new NotImplementedException();
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
