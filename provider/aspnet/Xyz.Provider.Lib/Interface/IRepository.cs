using System.Threading.Tasks;

namespace Xyz.Provider.Lib.Interface
{
  // TEntity is the Library model
  // The other interfaces extend this interface which
  // implements the generic methods that are to access the DbContext
  public interface IRepository<TEntity> where TEntity : class
  {
    Task<TEntity> GetAsync(int id);
    Task<TEntity> AddAsync(TEntity newEntity, int providerId);
    Task UpdateAsync(TEntity newEntity, int providerId);
    Task RemoveAsync(int id, int providerId);
  }
}
