using System.Collections.Generic;
using System.Threading.Tasks;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Lib.Interface
{
  public interface IAddressRepository : IRepository<Address>
  {
    Task<IEnumerable<Address>> GetAddressesByProviderIdAsync(int providerId);

    Task<IEnumerable<Address>> GetAddressesByComplexIdAsync(int complexId);
  }
}
