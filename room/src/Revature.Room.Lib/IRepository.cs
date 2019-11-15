using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Lib
{
  public interface IRepository
  {
    Task<IEnumerable<Room>> GetFilteredRooms(
      int complexId,
      string roomNumber,
      int? numberOfBeds,
      RoomType? roomType,
      Gender? gender,
      DateTime? endDate);
    public Task CreateRoom(Room myRoom);
  }
}
