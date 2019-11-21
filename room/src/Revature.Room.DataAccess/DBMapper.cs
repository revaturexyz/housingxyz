using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Room.DataAccess
{
  /// <summary>
  /// Mapper class that maps between DB Entity Objects and Business Logic Objects
  /// </summary>
  public class DBMapper : IMapper
  {
    private readonly RoomServiceContext _context;

    public DBMapper(RoomServiceContext context)
    {
      _context = context;
    }

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
    public async Task<Entities.Room> ParseRoomAsync(Lib.Room Room)
    {
      return new Entities.Room
      {
        RoomId = Room.RoomId,
        ComplexId = Room.ComplexId,
        Gender = await getGenderAsync(Room.Gender),
        RoomNumber = Room.RoomNumber,
        RoomType = await getRoomTypeAsync(Room.RoomType),
        NumberOfBeds = Room.NumberOfBeds,
        LeaseStart = Room.LeaseStart,
        LeaseEnd = Room.LeaseEnd,
        NumberOfOccupants = Room.NumberOfOccupants
      };
    }

    /// <summary>
    /// Method that searches the DB for Room type
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns></returns>
    private async Task<RoomType> getRoomTypeAsync(string roomType)
    {
      return await _context.RoomType.FirstOrDefaultAsync(r => r.Type == roomType);
    }

    /// <summary>
    /// Method that searches the DB for gender
    /// </summary>
    /// <param name="gender"></param>
    /// <returns></returns>
    private async Task<Gender> getGenderAsync(string gender)
    {
      return await _context.Gender.FirstOrDefaultAsync(g => g.Type == gender);
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
