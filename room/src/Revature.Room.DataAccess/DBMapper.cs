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
    /// Maps RoomId, ComplexId, Gender, RoomNumber, RoomType, NumberOfBeds, LeaseStart, LeaseEnd, NumberOfOccupants 
    /// </summary>
    /// <param name="roomEntity"></param>
    /// <returns></returns>
    public Lib.Room ParseRoom(Entities.Room roomEntity)
    {
      var room = new Lib.Room()
      {
        RoomId = roomEntity.RoomId,
        ComplexId = roomEntity.ComplexId,
        RoomNumber = roomEntity.RoomNumber,
        RoomType = roomEntity.RoomType.Type,
        NumberOfBeds = roomEntity.NumberOfBeds,
        NumberOfOccupants = roomEntity.NumberOfOccupants
      };
      room.SetLease(roomEntity.LeaseStart, roomEntity.LeaseEnd);
      if (roomEntity.Gender != null) room.Gender = roomEntity.Gender.Type;
      return room;
    }

    /// <summary>
    /// Method that converts a Business Logic Room Object to a DB Entities Room Object
    /// Maps RoomId, ComplexId,RoomNumber, NumberOfBeds, LeaseStart, LeaseEnd, NumberOfOccupants
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    /// <remarks>Gender and RoomType are assigned at repo</remarks>
    public Entities.Room ParseRoom(Lib.Room room)
    {
      return new Entities.Room
      {
        RoomId = room.RoomId,
        ComplexId = room.ComplexId,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        LeaseStart = room.LeaseStart,
        LeaseEnd = room.LeaseEnd,
        NumberOfOccupants = room.NumberOfOccupants
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
