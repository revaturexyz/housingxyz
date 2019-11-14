using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.DataAccess
{
  public interface IMapper
  {
    public Task<Entities.Room> ParseRoom(Lib.Room Room);

    public Task<Lib.Room> ParseRoom(Entities.Room Room);

    Task<IEnumerable<Lib.Room>> ParseRooms(IEnumerable<Entities.Room> roomsFromDB);
  }
}
