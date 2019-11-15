using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Lib.Interface
{
  public interface ITenantRepository : IDisposable
  {
    public Task AddAsync(Lib.Models.Tenant tenant);
    public Task<Models.Tenant> GetByIdAsync(int id);
    public Task<ICollection<Models.Tenant>> GetAllAsync();
    public Task DeleteByIdAsync(int id);
    public Task UpdateAsync(Models.Tenant tenant);
    public Task SaveAsync();
  }
}
