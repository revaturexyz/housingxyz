using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Api.Models;
using Logic = Revature.Complex.Lib.Models;
using Microsoft.Extensions.Logging;
using Revature.Complex.Api.Services;


namespace Revature.Complex.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplexController : Controller
  {
    private readonly IRepository _complexRepository;
    private readonly ILogger<ComplexController> log;
    private readonly IAddressService addressServiceSender;
    private readonly IRoomServiceSender roomServiceSender;

    public ComplexController(IRepository complexRepository, ILogger<ComplexController> logger,
      IAddressService addressService, IRoomServiceSender rss)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
      log = logger;
      addressServiceSender = addressService;
      roomServiceSender = rss;
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
        List<Logic.Complex> complices = await _complexRepository.ReadComplexListAsync();
        List<ApiComplex> apiComplices = new List<ApiComplex>();

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        foreach (Logic.Complex com in complices)
        {
          ApiComplex complex = new ApiComplex
          {
            ComplexId = com.ComplexId,
            // apiaddress
            ProviderId = com.ProviderId,
            ComplexName = com.ComplexName,
            ContactNumber = com.ContactNumber,
            ComplexAmentiy = await _complexRepository.ReadAmenityListByComplexIdAsync(com.ComplexId)
          };
          log.LogInformation($"(API)Amenities for complex Id {com.ComplexId} was found!");
          apiComplices.Add(complex);
        }

        log.LogInformation("(API)All Complices is returned.");
        return Ok(apiComplices);
      }
      catch (ArgumentNullException ex)
      {
        log.LogWarning($"(API){ex.Message}: There's no complex in the database.");
        return NotFound();
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex.Message}: Internal Server Error");
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
        Logic.Complex lcomplex = await _complexRepository.ReadComplexByIdAsync(complexId);
        log.LogInformation($"(API)complex with Id: {complexId} is found");

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        ApiComplex apiComplex = new ApiComplex
        {
          ComplexId = lcomplex.ComplexId,
          // apiaddress
          ProviderId = lcomplex.ProviderId,
          ComplexName = lcomplex.ComplexName,
          ContactNumber = lcomplex.ContactNumber,
          ComplexAmentiy = await _complexRepository.ReadAmenityListByComplexIdAsync(lcomplex.ComplexId)
        };
        log.LogInformation($"(API)Amenities for complex Id {lcomplex.ComplexId} was found!");

        return Ok(apiComplex);
      }
      catch (ArgumentNullException ex)
      {
        log.LogWarning($"(API){ex}: There's no complex with id: {complexId} in the database.");
        return NotFound();
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex.Message}: Internal Server Error");
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
    public async Task<ActionResult<ApiComplex>> GetComplexByNameAndNumberAsync([FromRoute]string complexName, string ComplexNumber)
    {
      try
      {
        Logic.Complex lcomplex = await _complexRepository.ReadComplexByNameAndNumberAsync(complexName, ComplexNumber);
        log.LogInformation($"(API)complex with name: {complexName} and phone: {ComplexNumber} is found");

        //GET address from address service via complexid

        ApiComplex apiComplex = new ApiComplex
        {
          ComplexId = lcomplex.ComplexId,
          //Address =
          ProviderId = lcomplex.ProviderId,
          ComplexName = lcomplex.ComplexName,
          ContactNumber = lcomplex.ContactNumber,
          ComplexAmentiy = await _complexRepository.ReadAmenityListByComplexIdAsync(lcomplex.ComplexId)
        };
        log.LogInformation($"(API)Amenities for complex Id {lcomplex.ComplexId} was found!");

        return Ok(apiComplex);
      }
      catch (ArgumentException ex)
      {
        log.LogWarning($"(API){ex.Message}: complex with name: {complexName} and phone: {ComplexNumber} is not found");
        return NotFound();
      }
      catch (InvalidOperationException ex)
      {
        log.LogWarning($"(API){ex}");
        return Conflict(ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex}: Internal Server Error");
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
        List<Logic.Complex> complices = await _complexRepository.ReadComplexByProviderIdAsync(providerId);
        log.LogInformation($"(API)complices for provider Id: {providerId} are found");

        List<ApiComplex> apiComplices = new List<ApiComplex>();

        foreach (Logic.Complex complex in complices)
        {
          ApiComplexAddress address = new ApiComplexAddress
          {
            AddressId = complex.AddressId
          };

          ApiComplex apiComplextoAdd = new ApiComplex
          {
            ComplexId = complex.ComplexId,
            Address = address,
            ProviderId = complex.ProviderId,
            ComplexName = complex.ComplexName,
            ContactNumber = complex.ContactNumber,
            ComplexAmentiy = await _complexRepository.ReadAmenityListByComplexIdAsync(complex.ComplexId)
          };
          log.LogInformation($"(API)Amenities for complex Id {complex.ComplexId} was found!");

          apiComplices.Add(apiComplextoAdd);
        }

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        log.LogInformation("(API)List of Complex is returned");
        return Ok(apiComplices);
      }
      catch (ArgumentException ex)
      {
        log.LogWarning($"(API){ex.Message}: complex with provider Id: {providerId} is not found");
        return NotFound();
      }
      catch (InvalidOperationException ex)
      {
        log.LogWarning($"(API){ex}: complex with provider Id: {providerId} is not found");
        return Conflict(ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex}: Internal Server Error");
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
      Guid addressId = Guid.NewGuid();
      ApiComplexAddress CompAddr = new ApiComplexAddress()
      {
        AddressId = addressId,
        StreetAddress = apiComplex.Address.StreetAddress,
        City = apiComplex.Address.City,
        State = apiComplex.Address.State,
        ZipCode = apiComplex.Address.ZipCode,
        Country = apiComplex.Address.Country,
        QueOperator = 0
      };

      Guid complexId = Guid.NewGuid();

      Logic.Complex complex = new Logic.Complex()
      {
        ComplexId = complexId,
        AddressId = addressId,
        ProviderId = apiComplex.ProviderId,
        ContactNumber = apiComplex.ContactNumber,
        ComplexName = apiComplex.ComplexName
      };

      Logic.AmenityComplex AmenityComplex = new Logic.AmenityComplex();

      try
      {
        await _complexRepository.CreateComplexAsync(complex);
        log.LogInformation("(API)new complex in the database is inserted");

        List<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListAsync();
        log.LogInformation("(API)list of Amenity is found");

        AmenityComplex.ComplexId = complex.ComplexId;

        foreach (var amenity in apiComplex.ComplexAmentiy)
        {
          foreach (var am in amenities)
          {
            if (am.AmenityType == amenity.AmenityType)
            {
              AmenityComplex.AmenityId = am.AmenityId;
              AmenityComplex.AmenityComplexId = Guid.NewGuid();
            }
          }

          await _complexRepository.CreateAmenityComplexAsync(AmenityComplex);
          log.LogInformation($"(API)list of Amenity for complex id: {complex.ComplexId} is created");
        }

        #region Code to sent address to other serivce Need to fill

        try
        {
          await addressServiceSender.SendRoomsMessages(CompAddr);
        }
        catch(Exception ex)
        {
          log.LogError($"(API){ex.Message}: failed to send address to Address service");
          return StatusCode(500, ex.Message);
        }

        #endregion

        return Created($"api/Complex/{complex.ComplexId}", apiComplex);

      }
      catch (ArgumentException)
      {
        log.LogError("(API)not found");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        log.LogError($"(API){e}");
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        log.LogError($"(API){e}");
        return StatusCode(500, e.Message);
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
      List<ApiRoomtoSend> apiRoomtoSends = new List<ApiRoomtoSend>();
      ApiRoomtoSend arts = new ApiRoomtoSend();
      Logic.AmenityRoom amenityRoom = new Logic.AmenityRoom();

      try
      {
        foreach (ApiRoom apiRoom in apiRooms)
        {
          arts.RoomId = Guid.NewGuid();
          arts.RoomNumber = apiRoom.RoomNumber;
          arts.ComplexId = apiRoom.ComplexId;
          arts.Gender = "default";
          arts.NumberOfBeds = apiRoom.NumberOfBeds;
          arts.RoomType = apiRoom.ApiRoomType;
          arts.LeaseStart = apiRoom.LeaseStart;
          arts.LeaseEnd = apiRoom.LeaseEnd;
          arts.QueOperator = 0;

          apiRoomtoSends.Add(arts);

          amenityRoom.AmenityRoomId = Guid.NewGuid();
          amenityRoom.RoomId = arts.RoomId;

          IEnumerable<ApiRoomtoSend> roomtoSends = apiRoomtoSends;

          //Send {roomtoSends} to room service
          await roomServiceSender.SendRoomsMessages(apiRoomtoSends);

          foreach (ApiAmenity amenity in apiRoom.Amenities)
          {
            amenityRoom.AmenityId = amenity.AmenityId;
            await _complexRepository.CreateAmenityRoomAsync(amenityRoom);
            log.LogInformation($"(API)list of amenity with room id: {arts.RoomId} is created");
          }

        }

        return StatusCode(201);
      }
      catch (ArgumentException)
      {
        log.LogError("(API) not found");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        log.LogError($"(API){e}");
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        log.LogError($"(API){e}");
        return StatusCode(500, e.Message);
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
      ApiComplexAddress CompAddr = new ApiComplexAddress()
      {
        AddressId = apiComplex.Address.AddressId,
        StreetAddress = apiComplex.Address.StreetAddress,
        City = apiComplex.Address.City,
        State = apiComplex.Address.State,
        ZipCode = apiComplex.Address.ZipCode,
        Country = apiComplex.Address.Country,
        QueOperator = 1
      };

      Logic.Complex complex = new Logic.Complex()
      {
        ComplexId = apiComplex.ComplexId,
        AddressId = apiComplex.Address.AddressId,
        ProviderId = apiComplex.ProviderId,
        ContactNumber = apiComplex.ContactNumber,
        ComplexName = apiComplex.ComplexName
      };

      await _complexRepository.DeleteAmenityComplexAsync(complex.ComplexId);
      log.LogInformation($"(API)old amenities for complex id: {apiComplex.ComplexId} is deleted");

      Logic.AmenityComplex AmenityComplex = new Logic.AmenityComplex();

      try
      {
        await _complexRepository.UpdateComplexAsync(complex);
        log.LogInformation("(API) complex is updated");

        List<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListAsync();
        log.LogInformation("(API) list of amenity is read");

        Guid amenityComplexId;
        AmenityComplex.ComplexId = complex.ComplexId;

        foreach (var amenity in apiComplex.ComplexAmentiy)
        {
          foreach (var am in amenities)
          {
            if (am.AmenityType == amenity.AmenityType)
            {
              AmenityComplex.AmenityId = am.AmenityId;

              amenityComplexId = Guid.NewGuid();
              AmenityComplex.AmenityComplexId = amenityComplexId;
            }
          }

          await _complexRepository.CreateAmenityComplexAsync(AmenityComplex);
          log.LogInformation("(API)new list of amenity of complex is created");
        }

        //send ApiComplexAddress to Address service to update the address

        return StatusCode(200);

      }
      catch (ArgumentException ex)
      {
        log.LogError($"(API){ex.Message}");
        return NotFound();
      }
      catch (InvalidOperationException ex)
      {
        log.LogError($"(API){ex.Message}");
        return Conflict(ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex.Message}");
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
      ApiRoomtoSend arts = new ApiRoomtoSend();
      Logic.AmenityRoom amenityRoom = new Logic.AmenityRoom();

      try
      {
        arts.RoomId = apiRoom.RoomId;
        arts.RoomNumber = apiRoom.RoomNumber;
        arts.ComplexId = apiRoom.ComplexId;
        arts.Gender = "default";
        arts.NumberOfBeds = apiRoom.NumberOfBeds;
        arts.RoomType = apiRoom.ApiRoomType;
        arts.LeaseStart = apiRoom.LeaseStart;
        arts.LeaseEnd = apiRoom.LeaseEnd;
        arts.QueOperator = 1;

        amenityRoom.AmenityRoomId = Guid.NewGuid();
        amenityRoom.RoomId = arts.RoomId;

        await _complexRepository.DeleteAmenityRoomAsync(apiRoom.RoomId);
        log.LogInformation($"(API)Amenity of Room Id {apiRoom.RoomId} is deleted");

        //Send {arts} to room service through service bus

        foreach (ApiAmenity amenity in apiRoom.Amenities)
        {
          amenityRoom.AmenityId = amenity.AmenityId;
          await _complexRepository.CreateAmenityRoomAsync(amenityRoom);
          log.LogInformation($"(API)list of amenity with room id: {arts.RoomId} is created");
        }

        return StatusCode(200);
      }
      catch (ArgumentException)
      {
        log.LogError("(API) not found");
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        log.LogError($"(API){e}");
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        log.LogError($"(API){e}");
        return StatusCode(500, e.Message);
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
    [HttpDelete("deletecomplex")]
    //PUT: api/complex/deletecomplex
    public async Task<ActionResult> DeleteComplexAsync([FromBody]Guid complexId, Guid AddressId)
    {
      try
      {
        ApiComplexAddress address = new ApiComplexAddress
        {
          AddressId = AddressId,
          QueOperator = 2
        };
        //send complexId to toom service to delete all rooms belongs to the complex
        //receive deleted room ids from room service to delete amenity of rooms

        //send complex Id to Address service to delete address for the complex

        await _complexRepository.DeleteAmenityComplexAsync(complexId);
        log.LogInformation($"(API) deleted amenity of complex Id: {complexId}");

        await _complexRepository.DeleteComplexAsync(complexId);
        log.LogInformation($"(API) deleted complex of complex Id: {complexId}");

        return StatusCode(200);
      }
      catch (ArgumentException ex)
      {
        log.LogError($"(API){ex}");
        return NotFound();
      }
      catch (InvalidOperationException ex)
      {
        log.LogError($"(API){ex}");
        return Conflict(ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex}");
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
    public async Task<ActionResult> DeleteRoomAsync([FromBody]ApiRoom Room)
    {
      try
      {
        ApiRoomtoSend roomtoDelete = new ApiRoomtoSend
        {
          RoomId = Room.RoomId,
          QueOperator = 2
        };

        IEnumerable<ApiRoomtoSend> message = new List<ApiRoomtoSend>
        {
          roomtoDelete
        };

        //send {send} to room service to delete a room
        await roomServiceSender.SendRoomsMessages(message);

        await _complexRepository.DeleteAmenityRoomAsync(Room.RoomId);
        log.LogInformation($"(API) deleted amenity of room Id: {Room.RoomId}");

        return StatusCode(200);
      }
      catch (ArgumentException ex)
      {
        log.LogError($"(API){ex.Message}");
        return NotFound();
      }
      catch (InvalidOperationException ex)
      {
        log.LogError($"(API){ex.Message}");
        return Conflict(ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"(API){ex.Message}");
        return StatusCode(500, ex.Message);
      }
    }
    #endregion
  }
}
