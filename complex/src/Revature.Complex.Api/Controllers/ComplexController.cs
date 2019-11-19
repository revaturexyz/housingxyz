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


namespace Revature.Complex.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplexController : Controller
  {
    private readonly IRepository _complexRepository;
    private readonly ILogger log;

    public ComplexController(IRepository complexRepository)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
    }

    #region GET

    /// <summary>
    /// (GET) Call Repository to read all existed complices from database 
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("Getallcomplex")]
    //GET: api/complex/Getallcomplex
    public async Task<ActionResult<List<ApiComplex>>> GetAllComplexAsync()
    {
      try
      {
        List<Logic.Complex> complices = await _complexRepository.ReadComplexListAsync();
        List<ApiComplex> apiComplices = new List<ApiComplex>();

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        foreach ( Logic.Complex com in complices)
        {
          ApiComplex complex = new ApiComplex
          {
            ComplexId = com.ComplexId,
            // apiaddress
            ProviderId = com.ProviderId,
            ComplexName = com.ComplexName,
            ContactNumber = com.ContactNumber
            //complex amenity
          };
          apiComplices.Add(complex);
        }

        return Ok(apiComplices);
      }
      catch( ArgumentNullException ex )
      {
        return NotFound();
      }
      catch( Exception ex )
      {
        return StatusCode(500, ex.Message);
      }

    }

    /// <summary>
    /// (GET) Call Repository and Address service to get specific complex info by complex Id
    /// </summary>
    /// <param name="complexGuid"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexGuid}")]
    //GET: api/complex/{complexGuid}
    public async Task<ActionResult<ApiComplex>> GetComplexByIdAsync([FromRoute]Guid complexGuid)
    {
      var x = await _complexRepository.ReadComplexByIdAsync(complexGuid);
      //var address = await serviceBus.GetAddress(To, x.AddressId);
      return new ApiComplex()
      {
        ComplexId = x.ComplexId,
        //Address = address;
        ComplexName = x.ComplexName,
        ContactNumber = x.ContactNumber,
        ProviderId = x.ProviderId

      };
    }

    /// <summary>
    /// (GET) Call Repository and Address service to get specific complex info by complex name and phone number
    /// </summary>
    /// <param name="complexName"></param>
    /// <param name="ComplexNumber"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexName}/{ComplexNumber}")]
    //GET: api/complex/{complexName/ComplexNumber}
    public async Task<ActionResult<ApiComplex>> GetComplexByNameAndNumber([FromRoute]string complexName, string ComplexNumber)
    {
      try
      {
        Logic.Complex lcomplex = await _complexRepository.ReadComplexByNameAndNumberAsync(complexName, ComplexNumber);

        List<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListByComplexIdAsync(lcomplex.ComplexId);

        //GET address from address service via complexid

        ApiComplex apiComplex = new ApiComplex
        {
          ComplexId = lcomplex.ComplexId,
          //Address =
          ProviderId = lcomplex.ProviderId,
          ComplexName = lcomplex.ComplexName,
          ContactNumber = lcomplex.ContactNumber,
          ComplexAmentiy = amenities
        };
        return Ok(apiComplex);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }


    /// <summary>
    /// (GET) Call Repository and Address service to get complices' info for specific provider by provider Id
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
        List<ApiComplex> apiComplices = new List<ApiComplex>();

        foreach( Logic.Complex complex in complices)
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
          apiComplices.Add(apiComplextoAdd);
          //amenities.Add(await _complexRepository.ReadAmenityListByComplexIdAsync(complex.ComplexId));
        }

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        return Ok(apiComplices);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    #endregion

    #region POST

    /// <summary>
    /// (POST)
    /// 1. Call Repository to insert new complex in the database
    /// 2. Send complex address to Address Service
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
        ZipCode = apiComplex.Address.ZipCode
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

      #region code call repo

      Logic.AmenityComplex AmenityComplex = new Logic.AmenityComplex();

      try
      {
        await _complexRepository.CreateComplexAsync(complex);
        List<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListAsync();

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
        }

        #region Code to sent address to other serivce Need to fill
        // serviceBus.sentInfor(To, data)


        #endregion

        return Created($"api/Complex/{complex.ComplexId}", apiComplex);

      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }


      #endregion


    }

    /// <summary>
    /// (POST)
    /// 1. Call Repository to insert Amenity of rooms into the database
    /// 2. Repackage the Rooms' object and send them to Room service
    /// </summary>
    /// <param name="apiRooms"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostRooms")]
    //POST: api/complex/PostRooms
    public async Task<ActionResult> PostRooms([FromBody]IEnumerable<ApiRoom> apiRooms)
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
          apiRoomtoSends.Add(arts);

          amenityRoom.AmenityRoomId = Guid.NewGuid();
          amenityRoom.RoomId = arts.RoomId;
          foreach (ApiAmenity amenity in apiRoom.Amenities)
          {
            amenityRoom.AmenityId = amenity.AmenityId;
            await _complexRepository.CreateAmenityRoomAsync(amenityRoom);
          }

        }
        IEnumerable<ApiRoomtoSend> roomtoSends = apiRoomtoSends;

        //Send {roomtoSends} to room service

        return StatusCode(201);
      }
      catch (ArgumentException)
      {
        return NotFound();
      }
      catch (InvalidOperationException e)
      {
        return Conflict(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    #endregion

    #region PUT

    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[HttpPut("editcomplex")]
    //PUT: api/complex/editcomplex
    //public Task<ActionResult> PutComplex([FromBody]ApiComplex apiComplex)
    //{  

    //}

    #endregion

    #region DELETE

    #endregion
  }
}
