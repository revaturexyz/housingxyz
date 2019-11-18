using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Lib
{
  public interface IRepository
  {
    Task<IEnumerable<Room>> GetFilteredRoomsAsync(
      Guid complexId,
      string roomNumber,
      int? numberOfBeds,
      string roomType,
      string gender,
      DateTime? endDate);

    public Task CreateRoomAsync(Room myRoom);

    public Task<List<Room>> ReadRoomAsync(Guid roomId);

    public Task UpdateRoomAsync(Room myRoom);

    public Task DeleteRoomAsync(Guid roomId);

    public Task SaveAsync();
    public Task<IList<Guid>> GetVacantFilteredRoomsByGenderandEndDateAsync(string gender, DateTime endDate);
  }
}
