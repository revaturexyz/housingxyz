using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xyz.Provider.Lib.Interface
{
  public interface IProviderRepository : IRepository<Models.Provider>
  {
    Task<IEnumerable<Models.Provider>> GetAllAsync();
    Task<IEnumerable<Models.TrainingCenter>> GetAllTrainingCentersAsync();
    Task<Models.TrainingCenter> GetTrainingCenterByProviderIdAsync(int providerId);
  }
}
