using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Address.Lib.Interfaces
{

  /// <summary>
  /// DataAccess interface for dependency injection, specifies the
  /// retrieval, insertion, and deletion of addresses from the database
  /// </summary>
  public interface IDataAccess : IDisposable
  {
    public Task<bool> AddAddressAsync(Address address);
    public Task<ICollection<Address>> GetAddressAsync(Guid? id = null, Address address = null);
    public Task<bool> DeleteAddressAsync(Guid? id);
    public Task SaveAsync();
  }
}
