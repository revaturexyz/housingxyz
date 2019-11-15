using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.BusinessLogic;
using Xyz.Provider.Lib.Interface;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  //[Authorize]
  public class RoomController : ControllerBase
  {
    private readonly IRoomRepository _roomRepository;
    private readonly IProviderRepository _providerRepository;
    private readonly IComplexRepository _complexRepository;
    private readonly IAddressRepository _addressRepository;

    public RoomController(
      IRoomRepository roomRepository,
      IProviderRepository providerRepository,
      IAddressRepository addressRepository,
      IComplexRepository complexRepository)
    {
      _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
      _providerRepository = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
      _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository));
    }

    /// <summary>
    /// Gets all rooms
    /// </summary>
    /// <returns></returns>
    // GET: api/room/complex/5
    [HttpGet("complex/{complexId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ApiRoom>>> GetByComplexIdAsync(int complexId)
    {
      try
      {
        IEnumerable<Room> rooms = await _roomRepository.GetAllRoomsByComplexIdAsync(complexId);
        var apiRooms = rooms.Select(r => new ApiRoom
        {
          RoomId = r.RoomId,
          RoomNumber = r.RoomNumber,
          NumberOfBeds = r.NumberOfBeds,
          IsOccupied = r.NumberOfOccupants > 0,
          HasAmenity = r.HasAmenity,
          StartDate = r.StartDate,
          EndDate = r.EndDate,
          ApiAddress = r.Address is null ? null : new ApiAddress
          {
            AddressId = r.Address.AddressId,
            StreetAddress = r.Address.StreetAddress,
            City = r.Address.City,
            State = r.Address.State,
            ZipCode = r.Address.Zip
          },
          ApiGender = new ApiGender { GenderId = r.Gender.GenderId, GenderType = r.Gender.GenderType },
          ApiAmenity = r.Amenities.Select(a => new ApiAmenity
          {
            AmenityId = a.AmenityId,
            Amenity = a.AmenityType
          }).ToList(),
          ApiRoomType = new ApiRoomType { TypeId = r.RoomType.TypeId, RoomType = r.RoomType.Type },
          ApiComplex = ApiModelFactory.MakeApiComplex(r.Complex),
        });
        return Ok(apiRooms);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Gets a list of rooms by provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // GET: api/room/provider/5
    [HttpGet("provider/{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ApiRoom>>> GetByProviderIdAsync(int providerId)
    {
      try
      {
        IEnumerable<Room> rooms = await _roomRepository.GetAllRoomsByProviderIdAsync(providerId);
        var apiRooms = rooms.Select(r => new ApiRoom
        {
          RoomId = r.RoomId,
          RoomNumber = r.RoomNumber,
          NumberOfBeds = r.NumberOfBeds,
          IsOccupied = r.NumberOfOccupants > 0,
          HasAmenity = r.HasAmenity,
          StartDate = r.StartDate,
          EndDate = r.EndDate,
          ApiComplex = r.Complex is null ? null : new ApiComplex
          {
            ComplexId = r.Complex.ComplexId,
            ContactNumber = r.Complex.ContactNumber,
            ComplexName = r.Complex.ComplexName,
          },
          ApiAddress = r.Address is null ? null : new ApiAddress
          {
            AddressId = r.Address.AddressId,
            StreetAddress = r.Address.StreetAddress,
            City = r.Address.City,
            State = r.Address.State,
            ZipCode = r.Address.Zip
          },
          ApiGender = r.Gender is null ? null : new ApiGender { GenderId = r.Gender.GenderId, GenderType = r.Gender.GenderType },
          ApiAmenity = r.Amenities.Select(a => new ApiAmenity
          {
            AmenityId = a.AmenityId,
            Amenity = a.AmenityType
          }).ToList(),
          ApiRoomType = r.RoomType is null ? null : new ApiRoomType { TypeId = r.RoomType.TypeId, RoomType = r.RoomType.Type }
        });
        return Ok(apiRooms);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Returns a room at an ID
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    // GET: api/room/5
    [HttpGet("{roomId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiRoom>> GetAsync(int roomId)
    {
      try
      {
        var room = await _roomRepository.GetAsync(roomId);
        var apiRoom = new ApiRoom
        {
          RoomId = room.RoomId,
          RoomNumber = room.RoomNumber,
          NumberOfBeds = room.NumberOfBeds,
          IsOccupied = room.NumberOfOccupants > 0,
          HasAmenity = room.HasAmenity,
          StartDate = room.StartDate,
          EndDate = room.EndDate,
          ApiAddress = room.Address is null ? null : new ApiAddress
          {
            StreetAddress = room.Address.StreetAddress,
            City = room.Address.City,
            State = room.Address.State,
            ZipCode = room.Address.Zip
          },
          ApiGender = room.Gender is null ? null : new ApiGender { GenderType = room.Gender.GenderType },
          ApiAmenity = room.Amenities?.Select(a => new ApiAmenity
          {
            AmenityId = a.AmenityId,
            Amenity = a.AmenityType
          }).ToList(),
          ApiRoomType = room.RoomType is null ? null : new ApiRoomType { RoomType = room.RoomType.Type },
          ApiComplex = ApiModelFactory.MakeApiComplex(room.Complex)
        };
        return Ok(apiRoom);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Get room types
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    // GET: api/room/5/type
    [HttpGet("{roomId}/type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiRoom>> GetRoomTypesAsync(int roomId)
    {
      try
      {
        var room = await _roomRepository.GetAsync(roomId);
        var roomType = new ApiRoomType
        {
          TypeId = room.RoomType.TypeId,
          RoomType = room.RoomType.Type
        };
        return Ok(roomType);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Adds a new room using a provider ID
    /// </summary>
    /// <param name="room"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // POST: api/room/5
    [HttpPost("{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiRoom>> PostAsync([FromBody] ApiRoom room, int providerId, [FromServices] AddressLogic addressLogic)
    {
      try
      {
        Complex complex;
        if (room.ApiComplex != null)
        {
          complex = await _complexRepository.GetAsync(room.ApiComplex.ComplexId);
        }
        else
        {
          return StatusCode(400, "Complex object cannot be empty.");
        }
        var newRoom = new Room
        {
          RoomNumber = room.RoomNumber,
          NumberOfBeds = room.NumberOfBeds,
          Complex = complex,
          HasAmenity = room.HasAmenity,
          StartDate = room.StartDate,
          EndDate = room.EndDate,
          Address = room.ApiAddress is null ? new Address() : new Address
          {
            AddressId = room.ApiAddress.AddressId,
            StreetAddress = room.ApiAddress.StreetAddress,
            City = room.ApiAddress.City,
            State = room.ApiAddress.State,
            Zip = room.ApiAddress.ZipCode
          },
          Amenities = room.ApiAmenity.Select(a => new Amenity
          {
            AmenityId = a.AmenityId,
            AmenityType = a.Amenity
          }).ToArray(),
          RoomType = room.ApiRoomType is null ? null : new RoomType
          {
            TypeId = room.ApiRoomType.TypeId,
            Type = room.ApiRoomType.RoomType
          }
        };
        Address roomAddress;
        if (room.ApiAddress.AddressId == 0)
        {
          roomAddress = Mapper.Map(room.ApiAddress);
        }
        else
        {
          roomAddress = await _addressRepository.GetAsync(room.ApiAddress.AddressId);
        }
        var trCenter = await _providerRepository.GetAsync(providerId);
        var trainingCenterAddress = trCenter.Center.Address;
        // FOR TESTING GOOGLE API
        if (await addressLogic.GetDistanceAsync(roomAddress, trainingCenterAddress, 20) &&
            (await addressLogic.GetDistanceAsync(roomAddress, complex.Address, 20)))
        {
          // should use logger instead
          Console.WriteLine("Distance is OK.");
        }
        else
        {
          return BadRequest("Distance between room address and provider address must be within 20 miles.");
        }
        if (newRoom.Address.AddressId == 0)
        {
          // Adds new room to a new address
          newRoom = await _roomRepository.AddRoomWithAddressAsync(newRoom.Address, newRoom, providerId);
        }
        else
        {
          // Adds room to an existing address
          newRoom = await _roomRepository.AddAsync(newRoom, providerId);
        }
        room.RoomId = newRoom.RoomId;
        return Created($"api/Room/{room.RoomId}", room);
      }
      catch (InvalidOperationException e)
      {
        return BadRequest(e.Message);
      }
      catch (ArgumentNullException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Get all room types
    /// </summary>
    /// <returns>List of ApiRoomType</returns>
    // GET: api/room/type
    [HttpGet("type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllRoomTypesAsync()
    {
      try
      {
        var roomTypes = await _roomRepository.GetAllRoomTypesAsync();
        var allRoomTypes = roomTypes.Select(roomType => new ApiRoomType
        {
          TypeId = roomType.TypeId,
          RoomType = roomType.Type
        });
        return Ok(allRoomTypes);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Updates a room at an ID
    /// </summary>
    /// <param name="apiRoom"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // this route makes zero sense
    // PUT: api/room/5
    [HttpPut("{providerId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutAsync([FromBody]ApiRoom apiRoom, int providerId)
    {
      try
      {
        var room = await _roomRepository.GetAsync(apiRoom.RoomId);
        if (room is null)
        {
          return NotFound();
        }
        else
        {
          room.RoomId = apiRoom.RoomId;
          room.RoomNumber = apiRoom.RoomNumber;
          room.NumberOfBeds = apiRoom.NumberOfBeds;
          room.HasAmenity = apiRoom.HasAmenity;
          room.StartDate = apiRoom.StartDate;
          room.EndDate = apiRoom.EndDate;
          if (apiRoom.ApiAmenity != null)
          {
            room.Amenities = apiRoom.ApiAmenity.Select(a => new Amenity
            {
              AmenityId = a.AmenityId,
              AmenityType = a.Amenity
            }).ToList();
          }
          if (room.RoomType != null && apiRoom.ApiRoomType != null)
          {
            room.RoomType.Type = apiRoom.ApiRoomType.RoomType;
          }
          await _roomRepository.UpdateAsync(room, providerId);
          return NoContent();
        }
      } // Might need to refactor errors and exceptions for put method
      catch (InvalidOperationException e)
      {
        return BadRequest(e.Message);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(400, e.Message);
      }
    }

    /// <summary>
    /// Gets all amenities
    /// </summary>
    /// <returns></returns>
    // GET: api/room/amenity
    [HttpGet("amenity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApiAmenity>>> GetAllAmenitiesAsync()
    {
      try
      {
        var roomAmenities = (await _roomRepository.GetAllRoomAmenitiesAsync()).Select(m => new ApiAmenity
        {
          AmenityId = m.AmenityId,
          Amenity = m.AmenityType
        });
        return Ok(roomAmenities);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Get a list of amenities by a room ID
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    //  api/room/5/amenity
    [HttpGet("{roomId}/amenity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiAmenity>>> GetAmenitiesByRoomIdAsync(int roomId)
    {
      try
      {
        var room = await _roomRepository.GetAsync(roomId);
        var amen = room.Amenities.Select(a => new ApiAmenity
        {
          AmenityId = a.AmenityId,
          Amenity = a.AmenityType
        });
        return Ok(amen);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    /// <summary>
    /// Deletes a room at an ID
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // DELETE: api/room/5/provider/5
    [HttpDelete("{roomId}/provider/{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(int roomId, int providerId)
    { // NOTE: We think this is throwing wrong code if no matching id
      try
      {
        await _roomRepository.RemoveAsync(roomId, providerId);
        return Ok(); // should be no content
      }
      catch (InvalidOperationException e)
      {
        return BadRequest(e.Message);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
