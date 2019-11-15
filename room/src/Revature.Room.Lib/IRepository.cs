using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Lib
{
  public interface IRepository
  {
    Task<IEnumerable<Room>> GetFilteredRooms(
      Guid complexId,
      string roomNumber,
      int? numberOfBeds,
      string roomType,
      string gender,
      DateTime? endDate);

    public Task CreateRoom(Room myRoom);

    public Task<List<Room>> ReadRoom(Guid roomId);

    public Task UpdateRoom(Room myRoom);

    public Task DeleteRoom(int roomId);
  }
}
