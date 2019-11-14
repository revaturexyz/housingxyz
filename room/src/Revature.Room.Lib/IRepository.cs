using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revature.Room.Lib
{
  public interface IRepository
  {
    Task<Room> GetRoom(int complexId, string roomNumber);
    Task<IEnumerable<Room>> GetRoomsOfComplex(int complexId);
    Task<IEnumerable<Room>> GetRoomsWithNumberOfBeds(int complexId, int numOfBeds);
    Task<IEnumerable<Room>> GetRoomsWithType(int complexId, RoomType roomType);
    Task<IEnumerable<Room>> GetRoomsOfGender(int complexId, Gender gender);
    Task<IEnumerable<Room>> GetRoomsBeforeLeaseEndDate(int complexId, DateTime endDate);
  }
}
