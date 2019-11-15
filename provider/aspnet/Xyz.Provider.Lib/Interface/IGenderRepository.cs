using System.Collections.Generic;
using System.Threading.Tasks;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Lib.Interface
{
  public interface IGenderRepository : IRepository<Gender>
  {
    Task UpdateGenderByRoomIdAsync(Gender gender, int roomId);
    Task<IEnumerable<Gender>> GetAllGenderTypesAsync();
  }
}
