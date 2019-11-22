using System.Collections.Generic;
using System.Linq;

namespace Revature.Room.DataAccess
{
  /// <summary>
  /// Mapper class that maps between DB Entity Objects and Business Logic Objects
  /// </summary>
  public class DBMapper : IMapper
  {
    /// <summary>
    /// Method that converts a DB Entities Room Object to a Business Logic Library Room Object
    /// </summary>
    /// <param name="Room"></param>
    /// <returns></returns>
    public Lib.Room ParseRoom(Entities.Room Room)
    {
      return new Lib.Room()
      {
        RoomId = Room.RoomId,
        ComplexId = Room.ComplexId,
        Gender = Room.Gender.Type,
        RoomNumber = Room.RoomNumber,
        RoomType = Room.RoomType.Type,
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd,
        NumberOfOccupants = Room.NumberOfOccupants
      };
    }

    /// <summary>
    /// Method that converts a Business Logic Room Object to a DB Entities Room Object
    /// </summary>
    /// <param name="Room"></param>
    /// <returns></returns>
    public Entities.Room ParseRoom(Lib.Room Room)
    {
      return new Entities.Room
      {
        RoomId = Room.RoomId,
        ComplexId = Room.ComplexId,
        RoomNumber = Room.RoomNumber,
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd,
        NumberOfOccupants = Room.NumberOfOccupants
      };
    }

    /// <summary>
    /// Method that parses a list of DB Entities Rooms to Business Logic Rooms
    /// </summary>
    /// <param name="roomsFromDB"></param>
    /// <returns></returns>
    public IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB)
    {
      return roomsFromDB.Select(r => ParseRoom(r));
    }
  }
}
