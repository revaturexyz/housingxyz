using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Address.Lib.Interfaces
{
  public interface IDataAccess : IDisposable
  {
    public Task AddAddressAsync(Address address);
    public Task<ICollection<Address>> GetAddressesAsync(Guid? id = null, Address address = null);
    public Task<bool> DeleteAddressAsync(Guid id);
    public Task SaveAsync();
  }
}
