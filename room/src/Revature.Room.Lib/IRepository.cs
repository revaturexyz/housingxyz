using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Lib
{
  public interface IRepository
  {
    /// <summary>
    /// Method that returns a list of rooms based on some parameters sent by the complex service
    /// </summary>
    /// <param name="complexId"></param>
    /// <param name="roomNumber"></param>
    /// <param name="numberOfBeds"></param>
    /// <param name="roomType"></param>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    Task<IEnumerable<Room>> GetFilteredRoomsAsync(
      Guid complexId,
      string roomNumber,
      int? numberOfBeds,
      string roomType,
      string gender,
      DateTime? endDate,
      Guid? roomId);

    /// <summary>
    /// Method that adds a room to the database
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public Task CreateRoomAsync(Room myRoom);

    /// <summary>
    /// Method that gets a specific room from the database
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public Task<List<Room>> ReadRoomAsync(Guid roomId);

    /// <summary>
    /// Method that updates a room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public Task UpdateRoomAsync(Room myRoom);

    /// <summary>
    /// Method that deletes a room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public Task DeleteRoomAsync(Guid roomId);

    /// <summary>
    /// Method that saves any changes to the database
    /// </summary>
    /// <returns></returns>
    public Task SaveAsync();

    /// <summary>
    /// Method returns a list of room ids of vacant rooms based on tenant gender and end date of their stay, for further filtering on the tenant service
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public Task<IList<Guid>> GetVacantFilteredRoomsByGenderandEndDateAsync(string gender, DateTime endDate);
  }
}
