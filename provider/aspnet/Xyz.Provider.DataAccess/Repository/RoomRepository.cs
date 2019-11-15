using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.DataAccess.Repository
{
  // business logic in this class needs to be factored out into a service class
  // note: consider toasyncenumerable instead of tolistasync in net core 3
  public class RoomRepository : IRoomRepository
  {
    private readonly RevatureHousingDbContext _dbRoomContext;
    private readonly ILogger _logger;

    public RoomRepository(RevatureHousingDbContext dbContext, ILogger<RoomRepository> logger = null)
    {
      _dbRoomContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
      _logger = logger;
    }

    /// <summary>
    /// Asynchronously adds room to database
    /// </summary>
    /// <param name="newEntity">An Entity Room object</param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Room model of added Room</returns>
    public async Task<Lib.Models.Room> AddAsync(Lib.Models.Room newEntity, int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid Provider ID.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "Provider ID must be positive.");
      }
      var addressId = newEntity.Address.AddressId;
      if (!await _dbRoomContext.Address.AnyAsync(r => r.AddressId == addressId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Address with same ID not found. Invalid Address ID.");
        throw new ArgumentNotFoundException("Address", addressId, nameof(newEntity));
      }
      if (await _dbRoomContext.Room
        .AnyAsync(r => r.RoomNumber == newEntity.RoomNumber && r.AddressId == newEntity.Address.AddressId)
        .ConfigureAwait(false))
      {
        _logger?.LogWarning("Address with same room information found. Duplicate data can't be added.");
        throw new InvalidOperationException($"Room {newEntity.RoomNumber} with same address is found in database.");
      }

      // authz based on this extra param - in a repo no less - makes zero sense
      if (!await _dbRoomContext.Complex
        .AnyAsync(c => c.ProviderId == providerId && c.ComplexId == newEntity.Complex.ComplexId)
        .ConfigureAwait(false))
      {
        _logger?.LogWarning("Provider doesn't have permission to add a new Room to Complex.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission to add room.");
      }
      try
      {
        // Adding a new Room into Room table.
        var newRoom = Mapper.Map(newEntity);
        newRoom.GenderId = 3; // Indicates unassigned gender, because provider cannot set gender
        foreach (var amenity in newEntity.Amenities.Distinct())
        {
          newRoom.RoomAmenity.Add(new RoomAmenity
          {
            AmenityId = amenity.AmenityId,
            Room = newRoom
          });
        }
        _dbRoomContext.Room.Add(newRoom);
        await _dbRoomContext.SaveChangesAsync().ConfigureAwait(false);
        newEntity.RoomId = newRoom.RoomId;
        return newEntity;
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in AddAsync Room repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously adds Room with an Address to the database
    /// </summary>
    /// <param name="newAddress"></param>
    /// <param name="newRoom"></param>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped Library Room model of added Room</returns>
    public async Task<Lib.Models.Room> AddRoomWithAddressAsync(Lib.Models.Address newAddress, Lib.Models.Room newRoom, int providerId)
    {
      // Checking if the Provider ID is valid.
      if (providerId <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID must be positive.");
      }
      var room = await _dbRoomContext.Room
        .Include(r => r.Address)
        .FirstOrDefaultAsync(a => a.Address.StreetAddress.Contains(newAddress.StreetAddress) &&
          a.Address.City == newAddress.City &&
          a.Address.State == newAddress.State &&
          a.Address.Zip == newAddress.Zip &&
          a.RoomNumber == newRoom.RoomNumber)
        .ConfigureAwait(false);
      if (room is null)
      {
        if (!await _dbRoomContext.Complex
          .AnyAsync(c => c.ComplexId == newRoom.Complex.ComplexId && c.ProviderId == providerId)
          .ConfigureAwait(false))
        {
          _logger?.LogWarning("Provider doesn't have permission to add a Room to Complex.");
          throw new InvalidOperationException($"Provider {providerId} doesn't have permission to add room to the complex.");
        }
        try
        {
          var entityRoom = Mapper.Map(newRoom);
          entityRoom.Address = Mapper.Map(newAddress);
          entityRoom.GenderId = 3; // Indicates unassigned gender, because provider cannot set gender
          _dbRoomContext.Room.Add(entityRoom);
          foreach (var amenity in newRoom.Amenities.Distinct())
          {
            entityRoom.RoomAmenity.Add(new RoomAmenity { AmenityId = amenity.AmenityId });
          }
          // Adding new Room asyncly
          await _dbRoomContext.SaveChangesAsync().ConfigureAwait(false);
          newRoom.RoomId = entityRoom.RoomId;
          return newRoom;
        }
        catch (DbUpdateException e)
        {
          _logger?.LogWarning($"Error: {e.Message}");
          throw;
        }
      }
      else
      {
        _logger?.LogWarning("Room with the same data exists in database.");
        throw new InvalidOperationException($"Room {room.RoomNumber} with same address exists in database.");
      }
    }

    /// <summary>
    /// Asynchronously retrieves a Room from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Task-wrapped Library Room model of added Room</returns>
    public async Task<Lib.Models.Room> GetAsync(int id)
    {
      if (id <= 0)
      {
        _logger?.LogWarning("Invalid input ID.");
        throw new ArgumentOutOfRangeException(nameof(id), id, "ID must be positive.");
      }
      var entityRoom = await _dbRoomContext.Room.Include(r => r.Address)
        .Include(r => r.Gender)
        .Include(r => r.Type)
        .Include(r => r.Complex)
        .FirstOrDefaultAsync(r => r.RoomId == id)
        .ConfigureAwait(false);
      if (entityRoom is null)
      {
        _logger?.LogWarning("No Room with same ID found.");
        throw new ArgumentNotFoundException("Room", id, nameof(id));
      }
      var resultRooms = Mapper.Map(entityRoom);
      // this is not the place to validate this
      if (resultRooms.RoomType is null)
      {
        _logger?.LogWarning("Room does not have type");
        throw new InvalidOperationException($"Room does not have type.");
      }

      // no reason to do two queries like this
      var amenityList = await _dbRoomContext.RoomAmenity
        .Include(rm => rm.Amenity)
        .Where(rm => rm.RoomId == resultRooms.RoomId)
        .ToListAsync()
        .ConfigureAwait(false);
      resultRooms.Amenities = new List<Lib.Models.Amenity>();
      // it could never be null
      if (amenityList != null)
      {
        foreach (var amenity in amenityList)
        {
          resultRooms.Amenities.Add(Mapper.Map(amenity.Amenity));
        }
      }
      return resultRooms;
    }

    public async Task<IEnumerable<Lib.Models.Amenity>> GetAllRoomAmenitiesAsync()
    {
      var list = await _dbRoomContext.Amenity.ToListAsync().ConfigureAwait(false);
      var amens = list.Select(Mapper.Map);
      // makes no sense to check or throw this
      if (!amens.Any())
      {
        throw new ArgumentException("Amenities not found");
      }
      return amens;
    }

    /// <summary>
    /// Asynchronously retrieves all rooms associated with a specific complex from database
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Room models</returns>
    public async Task<IEnumerable<Lib.Models.Room>> GetAllRoomsByComplexIdAsync(int complexId)
    {
      if (complexId <= 0)
      {
        _logger?.LogWarning("Invalid input ID.");
        throw new ArgumentOutOfRangeException(nameof(complexId), complexId, "ID must be positive.");
      }
      if (!await _dbRoomContext.Complex.AnyAsync(c => c.ComplexId == complexId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Complex ID not found.");
        throw new ArgumentNotFoundException("Complex", complexId, nameof(complexId));
      }
      var rooms = await _dbRoomContext.Room
        .Include(r => r.Address)
        .Include(r => r.Gender)
        .Include(r => r.Type)
        .Include(r => r.Complex)
        .Where(r => r.ComplexId == complexId)
        .ToListAsync()
        .ConfigureAwait(false);
      var resultRooms = new List<Lib.Models.Room>();
      // should just include this in the first query... explicit n+1!!
      foreach (var room in rooms.Select(Mapper.Map))
      {
        var amenityList = await _dbRoomContext.RoomAmenity.Include(rm => rm.Amenity)
          .Where(rm => rm.RoomId == room.RoomId)
          .ToListAsync()
          .ConfigureAwait(false);
        room.Amenities = amenityList.Select(a => Mapper.Map(a.Amenity)).ToList();
        resultRooms.Add(room);
      }
      return resultRooms;
    }

    /// <summary>
    /// Asynchronously retrieves all rooms associated with a specific provider from database
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns>Task-wrapped IEnumerable of Library Room models</returns>
    public async Task<IEnumerable<Lib.Models.Room>> GetAllRoomsByProviderIdAsync(int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID must be positive.");
      }
      if (!await _dbRoomContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Provider with same ID not found.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      if (!await _dbRoomContext.Complex.AnyAsync(c => c.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Complex that associated to provider ID not found.");
      }
      var complexes = await _dbRoomContext.Complex
        .Where(c => c.ProviderId == providerId)
        .ToListAsync()
        .ConfigureAwait(false);
      var rooms = new List<Room>();
      // explicit N+1... why? just one query will do...
      foreach (var complex in complexes)
      {
        if (await _dbRoomContext.Room.AnyAsync(r => r.ComplexId == complex.ComplexId).ConfigureAwait(false))
        {
          rooms.AddRange(await _dbRoomContext.Room
            .Include(r => r.Address)
            .Include(r => r.Complex)
            .Include(r => r.Type)
            .Where(r => r.ComplexId == complex.ComplexId)
            .ToListAsync()
            .ConfigureAwait(false));
        }
      }

      var resultRooms = new List<Lib.Models.Room>();
      foreach (var room in rooms.Select(Mapper.Map))
      {
        var amenityList = await _dbRoomContext.RoomAmenity
          .Include(rm => rm.Amenity)
          .Where(rm => rm.RoomId == room.RoomId)
          .ToListAsync()
          .ConfigureAwait(false);
        room.Amenities = amenityList.Select(a => Mapper.Map(a.Amenity)).ToList();
        resultRooms.Add(room);
      }
      return resultRooms;
    }

    /// <summary>
    /// Asynchronously retrieves all room types from database
    /// </summary>
    /// <returns>Task-wrapped IEnumerable of Library RoomType models</returns>
    public async Task<IEnumerable<Lib.Models.RoomType>> GetAllRoomTypesAsync()
    {
      var list = await _dbRoomContext.RoomType.ToListAsync().ConfigureAwait(false);
      var roomTypes = list.Select(Mapper.Map);
      return roomTypes;
    }

    /// <summary>
    /// Asynchronously removes a room from database
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    public async Task RemoveAsync(int id, int providerId)
    {
      if (id <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(id), id, "ID must be positive.");
      }
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID must be positive.");
      }
      if (!await _dbRoomContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Provider with same ID not found.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var room = await _dbRoomContext.Room.FirstOrDefaultAsync(r => r.RoomId == id).ConfigureAwait(false);
      if (room is null)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Room", id, nameof(id));
      }
      // why more than one query
      var complex = await _dbRoomContext.Complex
        .FirstOrDefaultAsync(c => c.ComplexId == room.ComplexId)
        .ConfigureAwait(false);
      if (complex.ProviderId != providerId)
      {
        _logger?.LogWarning("Provider doesn't have permission to change the room.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission " +
          $"to remove room {id}.");
      }
      if (room.NumberOfOccupants > 0)
      {
        _logger?.LogWarning("Provider cannot update the room because the room is occupied.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission " +
          $"to modify room {id} because the room is occupied.");
      }
      try
      {
        _dbRoomContext.Room.Remove(room);
        await _dbRoomContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in RemoveAsync for Room repo: {e.Message}.");
        throw;
      }
    }

    /// <summary>
    /// Asynchronously updates a Room in database
    /// </summary>
    /// <param name="newEntity"></param>
    /// <param name="providerId"></param>
    /// <returns>Task</returns>
    public async Task UpdateAsync(Lib.Models.Room newEntity, int providerId)
    {
      if (providerId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentOutOfRangeException(nameof(providerId), providerId, "ID must be positive.");
      }
      if (newEntity.RoomId <= 0)
      {
        _logger?.LogWarning("Invalid ID input.");
        throw new ArgumentException($"Room ID {newEntity.RoomId} must be positive.", nameof(newEntity));
      }
      if (!await _dbRoomContext.Provider.AnyAsync(p => p.ProviderId == providerId).ConfigureAwait(false))
      {
        _logger?.LogWarning("Provider with same ID not found.");
        throw new ArgumentNotFoundException("Provider", providerId, nameof(providerId));
      }
      var room = await _dbRoomContext.Room
        .FirstOrDefaultAsync(r => r.RoomId == newEntity.RoomId)
        .ConfigureAwait(false);
      if (room is null)
      {
        _logger?.LogWarning("ID not found in the database.");
        throw new ArgumentNotFoundException("Room", newEntity.RoomId, nameof(newEntity));
      }

      var complex = await _dbRoomContext.Complex
        .FirstOrDefaultAsync(c => c.ComplexId == room.ComplexId)
        .ConfigureAwait(false);

      if (complex.ProviderId != providerId)
      { // the room isn't associated with the provider
        _logger?.LogWarning("Provider doesn't have permission to change the room.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission " +
          $"to update room {room.RoomId}.");
      }

      if (room.NumberOfOccupants > 0)
      { // can't change the room info if there are occupants
        _logger?.LogWarning("Provider cannot update the room because the room is occupied.");
        throw new InvalidOperationException($"Provider {providerId} doesn't have permission " +
          $"to modify room {room.RoomId} because the room is occupied.");
      }
      try
      {
        // setting the values
        room.NumberOfBeds = newEntity.NumberOfBeds;
        room.StartDate = newEntity.StartDate;
        room.EndDate = newEntity.EndDate;
        room.RoomNumber = newEntity.RoomNumber;

        room.RoomAmenity.Clear();
        var newAmenities = new List<Amenity>();
        // this needs cleanup for sure
        foreach (var amenity in newEntity.Amenities)
        { // using a loop to add the proper amenities
          var oddCondition = await _dbRoomContext.Amenity
            .AnyAsync(a => a.AmenityType == amenity.AmenityType || a.AmenityId == amenity.AmenityId)
            .ConfigureAwait(false);
          if (!oddCondition && !newAmenities.Any(na => na.AmenityType == amenity.AmenityType))
          {
            var newAmenity = new Amenity
            {
              AmenityType = amenity.AmenityType
            };
            newAmenities.Add(newAmenity);
          }
          else if (!newAmenities.Any(na => na.AmenityType == amenity.AmenityType)) // surely this condition should be inverted
          {
            var existingAmenity = await _dbRoomContext.Amenity
              .FirstOrDefaultAsync(a => a.AmenityId == amenity.AmenityId)
              .ConfigureAwait(false);
            newAmenities.Add(existingAmenity);
          }
        }

        var distinctNewAmenities = newAmenities.Distinct();

        foreach (var amenity in distinctNewAmenities)
        { // use loop to give those amenites to a room
          if (!await _dbRoomContext.RoomAmenity
            .AnyAsync(ra => ra.AmenityId == amenity.AmenityId && ra.RoomId == room.RoomId)
            .ConfigureAwait(false)) // this condition is surely always true
          {
            room.RoomAmenity.Add(new RoomAmenity { Amenity = amenity });
          }
        }
        await _dbRoomContext.SaveChangesAsync().ConfigureAwait(false);
      }
      catch (DbUpdateException e)
      {
        _logger?.LogWarning($"Error in UpdateAsync for Room repo: {e.Message}.");
        throw;
      }
    }
  }
}
