using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revature.Room.DataAccess.Entities;
using Revature.Room.Lib;

namespace Revature.Room.DataAccess
{
  public class Repository : IRepository
  {
    private readonly RoomServiceContext _context;
    private readonly IMapper _map;

    public Repository(RoomServiceContext context, IMapper mapper)
    {
      _context = context;
      _map = mapper;
    }

    public async Task<Lib.Room> GetRoom(int complexId, string roomNumber)
    {
      return await _map.ParseRoom(_context.Room.FirstOrDefault(r => r.ComplexID == complexId && r.RoomNumber == roomNumber));
    }

    public async Task<IEnumerable<Lib.Room>> GetRoomsBeforeLeaseEndDate(int complexId, DateTime endDate)
    {
      return await _map.ParseRooms(_context.Room.Where(r => r.ComplexID == complexId && endDate < r.LeaseEnd).ToList());
    }

    public async Task<IEnumerable<Lib.Room>> GetRoomsOfComplex(int complexId)
    {
      return await _map.ParseRooms(_context.Room.Where(r => r.ComplexID == complexId).ToList());
    }

    public async Task<IEnumerable<Lib.Room>> GetRoomsOfGender(int complexId, Gender gender)
    {
      return await _map.ParseRooms(_context.Room.Where(r => r.ComplexID == complexId && r.Gender == gender).ToList());
    }

    public async Task<IEnumerable<Lib.Room>> GetRoomsWithNumberOfBeds(int complexId, int numOfBeds)
    {
      //return roomsFromDB.Select(async r => await _map.ParseRoom(r));
      return await _map.ParseRooms(_context.Room.Where(r => r.ComplexID == complexId && r.NumberOfBeds == numOfBeds).ToList());
    }

    public async Task<IEnumerable<Lib.Room>> GetRoomsWithType(int complexId, RoomType roomType)
    {
      return await _map.ParseRooms(_context.Room.Where(r => r.ComplexID == complexId && r.RoomType == roomType).ToList());
    }
  }
}
