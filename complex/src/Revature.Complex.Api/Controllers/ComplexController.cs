using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Complex.Api.Models;
using Revature.Complex.Api.Services;
using Revature.Complex.Lib.Interface;
using Logic = Revature.Complex.Lib.Models;


namespace Revature.Complex.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplexController : Controller
  {
    private readonly IRepository _complexRepository;
    private readonly ILogger<ComplexController> log;
    private readonly IRoomServiceSender roomServiceSender;
    private readonly IAddressRequest addressRequest;
    private readonly IRoomRequest roomRequest;


    public ComplexController(IRepository complexRepository, ILogger<ComplexController> logger,
      IRoomServiceSender rss, IAddressRequest ar, IRoomRequest rr)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
      log = logger;
      roomServiceSender = rss;
      addressRequest = ar;
    }

    #region GET

    /// <summary>
    /// (GET)
    /// Call Repository to read all existed complices from database
    /// without anything as parameters
    /// then return it as enumarable collections of Api Complex model
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("Getallcomplex")]
    //GET: api/complex/Getallcomplex
    public async Task<ActionResult<IEnumerable<ApiComplex>>> GetAllComplexAsync()
    {
      try
      {
        var complices = await _complexRepository.ReadComplexListAsync();
        var apiComplices = new List<ApiComplex>();

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.
        foreach (Logic.Complex com in complices)
        {
          Guid aId = com.AddressId;
          ApiComplexAddress address = await addressRequest.GetAddressAsync(aId);
          
          ApiComplex complex = new ApiComplex
          { 
            ComplexId = com.ComplexId,
            Address = address,
            ProviderId = com.ProviderId,
            ComplexName = com.ComplexName,
            ContactNumber = com.ContactNumber,
            ComplexAmenity = await _complexRepository.ReadAmenityListByComplexIdAsync(com.ComplexId)
          };
          log.LogInformation("a list of amenities for complex Id {com.ComplexId} were found!", com.ComplexId);
          apiComplices.Add(complex);
        }

        return Ok(apiComplices);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Repository and Address service to get specific complex info
    /// by complex Id as parameter
    /// will return single Api complex model
    /// </summary>
    /// <param name="complexGuid"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexId}")]
    //GET: api/complex/{complexId}
    public async Task<ActionResult<ApiComplex>> GetComplexByIdAsync([FromRoute]Guid complexId)
    {
      try
      {
        var lcomplex = await _complexRepository.ReadComplexByIdAsync(complexId);
        _log.LogInformation("a complex with Id: {complexId} was found", complexId);

        Guid aId = lcomplex.AddressId;
        ApiComplexAddress address = await addressRequest.GetAddressAsync(aId);

        var apiComplex = new ApiComplex
        {
          ComplexId = lcomplex.ComplexId,
          Address = address,
          ProviderId = lcomplex.ProviderId,
          ComplexName = lcomplex.ComplexName,
          ContactNumber = lcomplex.ContactNumber,
          ComplexAmenity = await _complexRepository.ReadAmenityListByComplexIdAsync(lcomplex.ComplexId)
        };
        _log.LogInformation("a list of amenities for complex Id {lcomplex.ComplexId} was found!", lcomplex.ComplexId);

        return Ok(apiComplex);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Repository and Address service to get specific complex info
    /// by complex name and phone number as parameters
    /// then return single Api Complex model
    /// </summary>
    /// <param name="complexName"></param>
    /// <param name="ComplexNumber"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexName}/{ComplexNumber}")]
    //GET: api/complex/{complexName/ComplexNumber}
    public async Task<ActionResult<ApiComplex>> GetComplexByNameAndNumberAsync([FromRoute]string complexName, string complexNumber)
    {
      try
      {
        var lcomplex = await _complexRepository.ReadComplexByNameAndNumberAsync(complexName, complexNumber);
        _log.LogInformation("a complex with name: {complexName} and phone: {ComplexNumber} was found", complexName, complexNumber);

        Guid aId = lcomplex.AddressId;
        ApiComplexAddress address = await addressRequest.GetAddressAsync(aId);

        var apiComplex = new ApiComplex
        {
          ComplexId = lcomplex.ComplexId,
          Address = address,
          ProviderId = lcomplex.ProviderId,
          ComplexName = lcomplex.ComplexName,
          ContactNumber = lcomplex.ContactNumber,
          ComplexAmenity = await _complexRepository.ReadAmenityListByComplexIdAsync(lcomplex.ComplexId)
        };
        _log.LogInformation("a list of amenities for complex Id {lcomplex.ComplexId} were found!", lcomplex.ComplexId);

        return Ok(apiComplex);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Repository and Address service to get list of complex info
    /// belongs specific provider by provider Id as parameter
    /// then return it as enumarable collections of Api Complex model
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("provierId/{providerId}")]
    //GET: api/complex/provierId/{providerID}
    public async Task<ActionResult<IEnumerable<ApiComplex>>> GetComplexListByProviderId([FromRoute]Guid providerId)
    {
      try
      {
        var complices = await _complexRepository.ReadComplexByProviderIdAsync(providerId);
        _log.LogInformation("a list of complices for provider Id: {providerId} were found", providerId);

        var apiComplices = new List<ApiComplex>();

        foreach (var complex in complices)
        {
          Guid aId = complex.AddressId;
          ApiComplexAddress address = await addressRequest.GetAddressAsync(aId);

          var apiComplextoAdd = new ApiComplex
          {
            ComplexId = complex.ComplexId,
            Address = address,
            ProviderId = complex.ProviderId,
            ComplexName = complex.ComplexName,
            ContactNumber = complex.ContactNumber,
            ComplexAmenity = await _complexRepository.ReadAmenityListByComplexIdAsync(complex.ComplexId)
          };
          _log.LogInformation("a list of amenities for complex Id {complex.ComplexId} was found!", complex.ComplexId);

          apiComplices.Add(apiComplextoAdd);
        }

        return Ok(apiComplices);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Room sevice via HttpRequest to Get list of rooms by complex Id
    /// then return it as enumarable collections of Api room model
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("rooms/{complexId}")]
    //GET: api/complex/provierId/{providerID}
    public async Task<ActionResult<IEnumerable<ApiComplex>>> GetRoomListByComplexId([FromRoute]Guid complexId)
    {
      try
      {
        List<ApiRoom> apiRooms = (await roomRequest.GetRooms(complexId)).ToList();
        return Ok(apiRooms);
      }
      catch(Exception ex)
      {
        log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region POST

    /// <summary>
    /// (POST)
    /// Call Repository to insert new complex in the database
    /// Send complex address to Address Service
    /// Need to take an Api complex model as parameter
    /// </summary>
    /// <param name="apiComplex"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostComplex")]
    //Post: api/complex/PostComplex
    public async Task<ActionResult<ApiComplex>> PostComplexAsync([FromBody]ApiComplex apiComplex)
    {
      ApiComplexAddress CompAddr = new ApiComplexAddress()
      {
        StreetAddress = apiComplex.Address.StreetAddress,
        City = apiComplex.Address.City,
        State = apiComplex.Address.State,
        ZipCode = apiComplex.Address.ZipCode,
        Country = apiComplex.Address.Country,
      };

      Guid addressId = (await addressRequest.PostAddressAsync(CompAddr)).AddressId;
      Guid complexId = Guid.NewGuid();

      var complex = new Logic.Complex()
      {
        ComplexId = complexId,
        AddressId = addressId,
        ProviderId = apiComplex.ProviderId,
        ContactNumber = apiComplex.ContactNumber,
        ComplexName = apiComplex.ComplexName
      };

      var amenityComplex = new Logic.AmenityComplex();

      try
      {
        await _complexRepository.CreateComplexAsync(complex);
        _log.LogInformation("(API)new complex in the database is inserted");

        var amenities = await _complexRepository.ReadAmenityListAsync();
        _log.LogInformation("(API)list of Amenity is found");

        amenityComplex.ComplexId = complex.ComplexId;

        foreach (var amenity in apiComplex.ComplexAmenity)
        {
          foreach (var am in amenities)
          {
            if (am.AmenityType == amenity.AmenityType)
            {
              amenityComplex.AmenityId = am.AmenityId;
              amenityComplex.AmenityComplexId = Guid.NewGuid();
            }
          }

          await _complexRepository.CreateAmenityComplexAsync(amenityComplex);
          _log.LogInformation($"(API)a list of amenities for complex id: {complex.ComplexId} was created");
        }

        return Created($"api/Complex/{complex.ComplexId}", apiComplex);

      }
      catch (Exception ex)
      {
        _log.LogError($"(API){ex}: unable to create complex");
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (POST)
    /// Call Repository to insert Amenity of rooms into the database
    /// Repackage the Rooms' object and send them to Room service
    /// Needs to take enumarable collections of Api Room model as parameters
    /// </summary>
    /// <param name="apiRooms"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostRooms")]
    //POST: api/complex/PostRooms
    public async Task<ActionResult> PostRoomsAsync([FromBody]IEnumerable<ApiRoom> apiRooms)
    {
      var apiRoomtoSends = new List<ApiRoomtoSend>();
      var amenityRoom = new Logic.AmenityRoom();

      try
      {
        foreach (var apiRoom in apiRooms)
        {
          var arts = new ApiRoomtoSend
          {
            RoomId = Guid.NewGuid(),
            RoomNumber = apiRoom.RoomNumber,
            ComplexId = apiRoom.ComplexId,
            NumberOfBeds = apiRoom.NumberOfBeds,
            RoomType = apiRoom.ApiRoomType,
            LeaseStart = apiRoom.LeaseStart,
            LeaseEnd = apiRoom.LeaseEnd,
            QueOperator = 0,
          };

          await roomServiceSender.SendRoomsMessages(arts);

          amenityRoom.AmenityRoomId = Guid.NewGuid();
          amenityRoom.RoomId = arts.RoomId;

          foreach (var amenity in apiRoom.Amenities)
          {
            amenityRoom.AmenityId = amenity.AmenityId;
            await _complexRepository.CreateAmenityRoomAsync(amenityRoom);
            _log.LogInformation("a list of amenities with room id: {0} was created", arts.RoomId);
          }
        }

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        _log.LogError(ex, "error while creating room");
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region PUT

    /// <summary>
    /// (PUT)
    /// Call Repo to update complex and amenity complex in the database
    /// Send updated address to Address service
    /// Needs to take Single Api Complex model as parameter
    /// </summary>
    /// <param name="apiComplex"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("editcomplex")]
    //PUT: api/complex/editcomplex
    public async Task<ActionResult> PutComplexAsync([FromBody]ApiComplex apiComplex)
    {
      var compAddr = new ApiComplexAddress()
      {
        AddressId = apiComplex.Address.AddressId,
        StreetAddress = apiComplex.Address.StreetAddress,
        City = apiComplex.Address.City,
        State = apiComplex.Address.State,
        ZipCode = apiComplex.Address.ZipCode,
        Country = apiComplex.Address.Country,
      };

      var complex = new Logic.Complex()
      {
        ComplexId = apiComplex.ComplexId,
        AddressId = apiComplex.Address.AddressId,
        ProviderId = apiComplex.ProviderId,
        ContactNumber = apiComplex.ContactNumber,
        ComplexName = apiComplex.ComplexName
      };

      await _complexRepository.DeleteAmenityComplexAsync(complex.ComplexId);
      _log.LogInformation($"(API)old amenities for complex id: {apiComplex.ComplexId} is deleted");

      var amenityComplex = new Logic.AmenityComplex();

      try
      {
        await _complexRepository.UpdateComplexAsync(complex);
        _log.LogInformation("(API) complex is updated");

        var amenities = await _complexRepository.ReadAmenityListAsync();
        _log.LogInformation("(API) list of amenity is read");

        Guid amenityComplexId;
        amenityComplex.ComplexId = complex.ComplexId;

        foreach (var amenity in apiComplex.ComplexAmenity)
        {
          foreach (var am in amenities)
          {
            if (am.AmenityType == amenity.AmenityType)
            {
              amenityComplex.AmenityId = am.AmenityId;

              amenityComplexId = Guid.NewGuid();
              amenityComplex.AmenityComplexId = amenityComplexId;
            }
          }

          await _complexRepository.CreateAmenityComplexAsync(amenityComplex);
          _log.LogInformation("(API)new list of amenity of complex is created");
        }

        //send ApiComplexAddress to Address service to update the address

        return StatusCode(200);

      }
      catch (Exception ex)
      {
        _log.LogError($"(API){ex}: unable to update complex");
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (PUT)
    /// Call Repo to delete and re-add new list of Amenity
    /// re-pack the Api Room model to Api RoomtoSend model
    /// send Api RoomtoSend object to Room service to delete single room
    /// </summary>
    /// <param name="apiRoom"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("editroom")]
    //PUT: api/complex/editroom
    public async Task<ActionResult> PutRoomAsync([FromBody]ApiRoom apiRoom)
    {
      var arts = new ApiRoomtoSend();
      var amenityRoom = new Logic.AmenityRoom();

      try
      {
        arts.RoomId = apiRoom.RoomId;
        arts.RoomNumber = apiRoom.RoomNumber;
        arts.ComplexId = apiRoom.ComplexId;
        arts.NumberOfBeds = apiRoom.NumberOfBeds;
        arts.RoomType = apiRoom.ApiRoomType;
        arts.LeaseStart = apiRoom.LeaseStart;
        arts.LeaseEnd = apiRoom.LeaseEnd;
        arts.QueOperator = 2;

        amenityRoom.AmenityRoomId = Guid.NewGuid();
        amenityRoom.RoomId = arts.RoomId;

        await _complexRepository.DeleteAmenityRoomAsync(apiRoom.RoomId);
        _log.LogInformation(")Amenity of Room Id {apiRoom.RoomId} is deleted", apiRoom.RoomId);

        await roomServiceSender.SendRoomsMessages(arts);

        foreach (var amenity in apiRoom.Amenities)
        {
          amenityRoom.AmenityId = amenity.AmenityId;
          await _complexRepository.CreateAmenityRoomAsync(amenityRoom);
          _log.LogInformation("list of amenity with room id: {arts.RoomId} is created", arts.RoomId);
        }

        return StatusCode(200);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region DELETE
    /// <summary>
    /// (DELETE)
    /// Call Repo to delete complex and amenity complex in the database
    /// Send complex Id to Address service to delete the address
    /// Needs a complex Id as parameter
    /// </summary>
    /// <param name="apiComplex"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("deletecomplex/{complexId}")]
    //PUT: api/complex/deletecomplex/{complexId}
    public async Task<ActionResult> DeleteComplexAsync([FromRoute]Guid complexId)
    {
      try
      {
        ApiRoomtoSend arts = new ApiRoomtoSend
        {
          ComplexId = complexId,
          QueOperator = 3
        };

        //send complexId to toom service to delete all rooms belongs to the complex
        //receive deleted room ids from room service to delete amenity of rooms
        await roomServiceSender.SendRoomsMessages(arts);

        await _complexRepository.DeleteAmenityComplexAsync(complexId);
        _log.LogInformation("deleted amenity of complex Id: {complexId}", complexId);

        await _complexRepository.DeleteComplexAsync(complexId);
        _log.LogInformation("deleted complex of complex Id: {complexId}", complexId);

        return StatusCode(200);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (DELETE)
    /// Call Repo to delete amenity of room in the database
    /// re-pack Api Room as Api RoomtoSend
    /// Send RoomtoSend to Room serivice to delete single room
    /// Needs Api Room object as parameter
    /// </summary>
    /// <param name="apiComplex"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("deleteroom")]
    //PUT: api/complex/deleteroom
    public async Task<ActionResult> DeleteRoomAsync([FromBody]ApiRoom room)
    {
      try
      {
        var roomtoDelete = new ApiRoomtoSend
        {
          RoomId = room.RoomId,
          RoomNumber = room.RoomNumber,
          ComplexId = room.ComplexId,
          NumberOfBeds = room.NumberOfBeds,
          RoomType = room.ApiRoomType,
          LeaseStart = room.LeaseStart,
          LeaseEnd = room.LeaseEnd,
          QueOperator = 1
        };

        //send {send} to room service to delete a room
        await _roomServiceSender.SendRoomsMessages(roomtoDelete);

        await _complexRepository.DeleteAmenityRoomAsync(room.RoomId);
        _log.LogInformation("deleted amenity of room Id: {Room.RoomId}", room.RoomId);

        return StatusCode(200);
      }
      catch (Exception ex)
      {
        _log.LogError("{ex}: Internal Server Error", ex);
        return StatusCode(500, ex.Message);
      }
    }
    #endregion
  }
}
