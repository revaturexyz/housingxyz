using System.Collections.Generic;
using System.Linq;

namespace Revature.Room.DataAccess
{
  public class DBMapper : IMapper
  {
    public Lib.Room ParseRoom(Entities.Room Room)
    {
      return new Lib.Room()
      {
        RoomID = Room.RoomID,
        ComplexID = Room.ComplexID,
        Gender = Room.Gender,
        RoomNumber = Room.RoomNumber,
        RoomType = Room.RoomType,
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd
      };
    }

    public IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB)
    {
      return roomsFromDB.Select(r => ParseRoom(r));
    }
  }
}
