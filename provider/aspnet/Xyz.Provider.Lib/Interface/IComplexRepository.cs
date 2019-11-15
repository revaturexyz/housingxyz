using System.Collections.Generic;
using System.Threading.Tasks;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Lib.Interface
{
  public interface IComplexRepository : IRepository<Complex>
  {
    Task<IEnumerable<Complex>> GetComplexesByProviderIdAsync(int providerId);
    Task<IEnumerable<Amenity>> GetAmenitiesByComplexIdAsync(int complexId);
  }
}
