using Revature.Room.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Revature.Room.DataAccess
{
  public class DBMapper : IMapper
  {
    private readonly RoomServiceContext _context;

    public DBMapper(RoomServiceContext context)
    {
      _context = context;
    }

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
        LeaseEnd = Room.LeaseEnd,
        NumberOfOccupants = Room.NumberOfOccupants
      };
    }

    public Entities.Room ParseRoom(Lib.Room Room)
    {
      return new Entities.Room
      {
        RoomID = Room.RoomID,
        ComplexID = Room.ComplexID,
        Gender = getGender(Room.Gender),
        RoomNumber = Room.RoomNumber,
        RoomType = getRoomtype(Room.RoomType),
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd,
        NumberOfOccupants = Room.NumberOfOccupants
      };
    }

    private RoomType getRoomtype(string roomType)
    {
      return _context.RoomType.FirstOrDefault(r => r.Type == roomType);
    }

    private Gender getGender(string gender)
    {
      return _context.Gender.FirstOrDefault(g => g.Type == gender);
    }

    public IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB)
    {
      return roomsFromDB.Select(r => ParseRoom(r));
    }
  }
}
