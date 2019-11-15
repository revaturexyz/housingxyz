using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public Repository(RoomServiceContext context, IMapper mapper, ILogger<Repository> logger)
    {
      _context = context;
      _map = mapper;
      _logger = logger;
    }

    public async Task CreateRoom(Lib.Room myRoom)
    {
      Data.Room roomEntity = await _map.ParseRoom(myRoom);
      await _context.AddAsync(roomEntity);
      await _context.SaveChangesAsync();

      //Log here, not too sure about myRoom
      _logger.LogInformation("Successfully added room to database!", myRoom);

    }

    public async Task<IEnumerable<Lib.Room>> GetFilteredRooms(
      int complexId,
      string roomNumber,
      int? numberOfBeds,
      RoomType? roomType,
      Gender? gender,
      DateTime? endDate)
    {
      IEnumerable<Entities.Room> rooms = _context.Room.Where(r => r.ComplexID == complexId);
      if (roomNumber != null)
      {
        rooms.Where(r => r.RoomNumber == roomNumber);
      }
      if (numberOfBeds != null)
      {
        rooms.Where(r => r.NumberOfBeds == numberOfBeds);
      }
      if (roomType != null)
      {
        rooms.Where(r => r.RoomType == roomType);
      }
      if (gender != null)
      {
        rooms.Where(r => r.Gender == gender);
      }
      if (endDate != null)
      {
        rooms.Where(r => endDate < r.LeaseEnd);
      }
      return await _map.ParseRooms(rooms);
    }
  }
}
