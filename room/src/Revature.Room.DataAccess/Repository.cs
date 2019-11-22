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
    /// Method that creates a Room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public async Task CreateRoomAsync(Lib.Room myRoom)
    {
      Data.Room roomEntity = _map.ParseRoom(myRoom);
      roomEntity.Gender = await _context.Gender.FirstAsync(g => g.Type == myRoom.Gender);
      roomEntity.RoomType = await _context.RoomType.FirstAsync(r => r.Type == myRoom.RoomType);
      await _context.AddAsync(roomEntity);
    }

    /// <summary>
    /// Method that gets a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task<Lib.Room> ReadRoomAsync(Guid roomId)
    {
      return _map.ParseRoom(await _context.Room.Where(r => r.RoomId == roomId).Include(r => r.Gender).Include(r => r.RoomType).FirstAsync());
    }

    /// <summary>
    /// Method that updates the gender, lease start, end, and number of occupants of a Room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    public async Task UpdateRoomAsync(Lib.Room myRoom)
    {
      Data.Room roomEntity = await _context.Room.Where(r => r.RoomId == myRoom.RoomId)
        .Include(r => r.Gender)
        .Include(r => r.RoomType)
        .FirstAsync();

      roomEntity.Gender = await _context.Gender.FirstOrDefaultAsync(g => g.Type == myRoom.Gender);
      roomEntity.LeaseStart = myRoom.LeaseStart;
      roomEntity.LeaseEnd = myRoom.LeaseEnd;
      roomEntity.NumberOfOccupants = myRoom.NumberOfOccupants;
    }

    /// <summary>
    /// Method that deletes a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public async Task DeleteRoomAsync(Guid roomId)
    {
      var roomEntity = await _context.Room.FindAsync(roomId);
      _context.Remove(roomEntity);
    }

    /// <summary>
    /// Method that filters Room based on ComplexId and other additional filters
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
    /// <summary>
    /// Method that updates the room occupants when a tenant is assigned a room
    /// </summary>
    /// <param name="roomId"></param>
    public async Task AddRoomOccupantsAsync(Guid roomId)
    {
      Entities.Room roomToUpdate = await _context.Room.FirstAsync(r => r.RoomId == roomId);
      roomToUpdate.NumberOfOccupants++;
    }
  }
}
