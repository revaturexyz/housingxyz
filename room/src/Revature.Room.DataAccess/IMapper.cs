using System.Collections.Generic;
using System.Threading.Tasks;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;

namespace Revature.Room.DataAccess
{
  public interface IMapper
  {
    public Task<Entities.Room> ParseRoom(Lib.Room Room);
    public Task<Lib.Room> ParseRoom(Entities.Room Room);
    Task<IEnumerable<Lib.Room>> ParseRooms(List<Entities.Room> roomsFromDB);
  }
}
