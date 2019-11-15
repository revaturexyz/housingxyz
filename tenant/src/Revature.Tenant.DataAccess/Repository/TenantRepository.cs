using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Revature.Tenant.Lib.Interface;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.DataAccess.Repository
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
