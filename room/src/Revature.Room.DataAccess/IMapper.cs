using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.DataAccess
{
  public interface IMapper
  {
    public Lib.Room ParseRoom(Entities.Room Room);
    public Entities.Room ParseRoom(Lib.Room Room);


    IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB);
  }
}
