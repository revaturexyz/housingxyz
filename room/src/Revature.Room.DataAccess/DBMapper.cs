using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;
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
        Gender = Room.Gender.Type,
        RoomNumber = Room.RoomNumber,
        RoomType = Room.RoomType.Type,
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd
      };
    }

    public Entities.Room ParseRoom(Lib.Room Room)
    {
      return new Entities.Room
      {
        RoomID = Room.RoomID,
        ComplexID = Room.ComplexID,
        Gender = new Entities.Gender { Type = Room.Gender },
        RoomNumber = Room.RoomNumber,
        RoomType = new Entities.RoomType { Type = Room.RoomType },
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
