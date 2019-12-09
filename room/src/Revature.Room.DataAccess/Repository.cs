using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;
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
      var roomEntity = _map.ParseRoom(myRoom);
      roomEntity.Gender = null;
      roomEntity.RoomType = await _context.RoomType.FirstAsync(r => r.Type == myRoom.RoomType);
      await _context.AddAsync(roomEntity);
      await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Method that gets a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when room is not found in the database</exception>
    public async Task<Lib.Room> ReadRoomAsync(Guid roomId)
    {
      return _map.ParseRoom(await _context.Room.Where(r => r.RoomId == roomId).Include(r => r.Gender).Include(r => r.RoomType).FirstAsync());
    }

    /// <summary>
    /// Method that updates the lease start and end of a Room
    /// </summary>
    /// <param name="myRoom"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when room isn't found in DB</exception>
    /// <remarks>Update room method for the complex service</remarks>
    public async Task UpdateRoomAsync(Lib.Room myRoom)
    {
      var roomEntity = await _context.Room.Where(r => r.RoomId == myRoom.RoomId)
        .Include(r => r.Gender)
        .Include(r => r.RoomType)
        .FirstAsync();

      roomEntity.LeaseStart = myRoom.LeaseStart;
      roomEntity.LeaseEnd = myRoom.LeaseEnd;

      await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Method that deletes a Room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when room Id isn't found</exception>
    public async Task DeleteRoomAsync(Guid roomId)
    {
      var roomEntity = await _context.Room.FindAsync(roomId);
      _context.Remove(roomEntity);
      await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes all rooms based on given complex ID and returns all room IDs that have been deleted
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when room to be deleted isn't found in DB</exception>
    public async Task<List<Guid>> DeleteComplexRoomAsync(Guid complexId)
    {
      var roomEntity = await _context.Room.Where(r => r.ComplexId == complexId).Select(r => r.RoomId).ToListAsync();

      foreach (var r in roomEntity)
      {
        await DeleteRoomAsync(r);
      }

      await _context.SaveChangesAsync();

      return roomEntity;
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
    public async Task<IList<Tuple<Guid, int>>> GetVacantFilteredRoomsByGenderandEndDateAsync(string gender, DateTime endDate)
    {
      return await _context.Room
        .Where(r => (r.Gender == null || r.Gender.Type.ToUpper() == gender.ToUpper()) && endDate < r.LeaseEnd && r.NumberOfOccupants < r.NumberOfBeds)
        .Select(r => new Tuple<Guid, int>(r.RoomId, r.NumberOfBeds))
        .ToListAsync();
    }

    /// <summary>
    /// Method that updates the room occupants when a tenant is assigned a room
    /// </summary>
    /// <param name="roomId"></param>
    /// <exception cref="InvalidOperationException">Thrown when a room matching the roomId is not found, or the gender type isn't found </exception>
    /// <remarks>Sets a room's gender when Gender is null, i.e. when the room was previously unoccupied</remarks>
    public async Task AddRoomOccupantsAsync(Guid roomId, string tenantGender)
    {
      var roomToUpdate = await _context.Room.Where(r => r.RoomId == roomId).Include(r => r.Gender).FirstAsync();
      roomToUpdate.NumberOfOccupants++;
      if (roomToUpdate.Gender == null)
      {
        roomToUpdate.Gender = await _context.Gender.FirstAsync(g => g.Type.ToUpper() == tenantGender.ToUpper());
      }
      await SaveAsync();
    }

    /// <summary>
    /// Method that updates occupants when an occupant vacates a room
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when room isn't found</exception>
    /// <remarks>Reverts gender of room back to null if updated room is empty</remarks>
    public async Task SubtractRoomOccupantsAsync(Guid roomId)
    {
      var roomToUpdate = await _context.Room.Where(r => r.RoomId == roomId).Include(r => r.Gender).FirstAsync();
      roomToUpdate.NumberOfOccupants--;
      if (roomToUpdate.NumberOfOccupants == 0)
      {
        roomToUpdate.Gender = null;
      }
      await SaveAsync();
    }
  }
}
