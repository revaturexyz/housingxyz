using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;
using Data = Revature.Room.DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace Revature.Room.DataAccess
{
  public class Repository : IRepository
  {
    private readonly RoomServiceContext _context;
    private readonly IMapper _map;
    private readonly ILogger<Repository> _logger;


    public Repository(RoomServiceContext context, IMapper mapper)
    {
      _context = context;
      _map = mapper;
    }
    public Repository(RoomServiceContext context, IMapper mapper, ILogger<Repository> logger)
    {
      _context = context;
      _map = mapper;
      _logger = logger;
    }

    public async Task CreateRoom(Lib.Room myRoom)
    {
      Data.Room roomEntity = _map.ParseRoom(myRoom);
      await _context.AddAsync(roomEntity);
      await _context.SaveChangesAsync();

      //Log here, not too sure about myRoom
      _logger.LogInformation("Successfully added room to database!", myRoom);

    }

    public async Task<List<Lib.Room>> ReadRoom(Guid roomId)
    {
      //if Guid does not exist then it will return all rooms
      if (roomId == null)
      {
        List<Data.Room> roomList = await _context.Room.Include(r => r.Gender).Include(r => r.RoomType).ToListAsync();

        _logger.LogInformation("Entered in a null Guid, so reading all rooms!", roomId);


        return _map.ParseRooms(roomList).ToList();
      }

      //Find room by Guid and return that particular room
      List<Data.Room> listRoom = await _context.Room.Include(r => r.Gender).Include(r => r.RoomType).Where(r => r.RoomID == roomId).ToListAsync();

      _logger.LogInformation("Read a particular room!", roomId);

      return _map.ParseRooms(listRoom).ToList();

    }

    //Update room by Guid
    public async Task UpdateRoom(Lib.Room myRoom)
    {
      Data.Room roomEntity = _context.Room.Where(r => r.RoomID == myRoom.RoomID)
        .Include(r => r.Gender)
        .Include(r => r.RoomType)
        .First() ?? throw new ArgumentNullException("There is not such room!", nameof(roomEntity));

      try
      {

        roomEntity.Gender.Type = myRoom.Gender;
        roomEntity.LeaseStart = myRoom.LeaseStart;
        roomEntity.LeaseEnd = myRoom.LeaseEnd;
      }
      catch(InvalidOperationException e)
      {
        _logger.LogError("Invalid operation, can't update those!", myRoom);
      }


      await _context.SaveChangesAsync();
      _logger.LogInformation("Updating room successful!",myRoom);
    }

    //Deletes room by id
    public async Task DeleteRoom(int roomId)
    {
      var roomEntity = await _context.Room.FindAsync(roomId);
      _context.Remove(roomEntity);
      _logger.LogInformation("Successfully removed room from database!", roomEntity);
    }

    public async Task<IEnumerable<Lib.Room>> GetFilteredRooms(
      Guid complexId,
      string roomNumber,
      int? numberOfBeds,
      string roomType,
      string gender,
      DateTime? endDate)
    {
      IEnumerable<Entities.Room> rooms = _context.Room.Where(r => r.ComplexID == complexId).Include(r => r.Gender).Include(r => r.RoomType);
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
      return _map.ParseRooms(rooms);
    }
  }
}
