using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Data = Revature.Room.DataAccess.Entities;

namespace Revature.Room.DataAccess
{
  /// <summary>
  /// Class in charge of methods that affect the state and data in the Room DB
  /// </summary>
  public class Repository : IRepository
  {
    private readonly RoomServiceContext _context;
    private readonly IMapper _map;

    public Repository(RoomServiceContext context, IMapper mapper)
    {
      _context = context;
      _map = mapper;
    }

    /// <summary>
    /// Method that creates a room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public async Task CreateRoomAsync(Lib.Room myRoom)
    {
      Data.Room roomEntity = _map.ParseRoom(myRoom);
      await _context.AddAsync(roomEntity);
    }

   

    /// <summary>
    /// Method that updates the gender, lease start, end, and number of occupants of a room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public async Task UpdateRoomAsync(Lib.Room myRoom)
    {
      Data.Room roomEntity = await _context.Room.Where(r => r.RoomId == myRoom.RoomId)
        .Include(r => r.Gender)
        .Include(r => r.RoomType)
        .FirstOrDefaultAsync() ?? throw new ArgumentNullException("There is no such room!", nameof(roomEntity));

      roomEntity.Gender = await _context.Gender.FirstOrDefaultAsync(g => g.Type == myRoom.Gender);
      roomEntity.LeaseStart = myRoom.LeaseStart;
      roomEntity.LeaseEnd = myRoom.LeaseEnd;
      roomEntity.NumberOfOccupants = myRoom.NumberOfOccupants;
    }

    /// <summary>
    /// Method that deletes a room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task DeleteRoomAsync(Guid roomId)
    {
      var roomEntity = await _context.Room.FindAsync(roomId);
      _context.Remove(roomEntity);
    }
    /// <summary>
    /// Method that filters room based on ComplexId and other additional filters
    /// </summary>
    /// <param name="complexId"></param>
    /// <param name="roomNumber"></param>
    /// <param name="numberOfBeds"></param>
    /// <param name="roomType"></param>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException">Either ComplexId or RoomId is not in the DB</exception>
    public async Task<IEnumerable<Lib.Room>> GetFilteredRoomsAsync(
      Guid complexId,
      string roomNumber,
      int? numberOfBeds,
      string roomType,
      string gender,
      DateTime? endDate,
      Guid? roomId)
    {
      IEnumerable<Entities.Room> rooms = await _context.Room.Where(r => r.ComplexId == complexId)
                                                            .Include(r => r.Gender).Include(r => r.RoomType)
                                                            .ToListAsync() ?? throw new KeyNotFoundException("Complex Id not found");
      if (roomNumber != null)
      {
        rooms = rooms.Where(r => r.RoomNumber == roomNumber);
      }
      if (numberOfBeds != null)
      {
        rooms = rooms.Where(r => r.NumberOfBeds == numberOfBeds);
      }
      if (roomType != null)
      {
        rooms = rooms.Where(r => r.RoomType.Type == roomType);
      }
      if (gender != null)
      {
        rooms = rooms.Where(r => r.Gender.Type == gender);
      }
      if (endDate != null)
      {
        rooms = rooms.Where(r => endDate < r.LeaseEnd);
      }
      if (roomId != null)
      {
        rooms = rooms.Where(r => r.RoomId == roomId) ?? throw new KeyNotFoundException("Room Id not found");
      }
      return _map.ParseRooms(rooms);
    }

    /// <summary>
    /// Persist changes to the database. Called after a unit of work has been completed.
    /// </summary>
    /// <returns></returns>
    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
    /// <summary>
    /// Returns vacant rooms based on gender and end date
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IList<Guid>> GetVacantFilteredRoomsByGenderandEndDateAsync(string gender, DateTime endDate)
    {
      return await _context.Room
        .Where(r => r.Gender.Type.ToUpper() == gender.ToUpper() && endDate < r.LeaseEnd && r.NumberOfOccupants < r.NumberOfBeds)
        .Select(r => r.RoomId).ToListAsync();
    }
  }
}
