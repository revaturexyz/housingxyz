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
  public class AmenityController : ControllerBase
  {
    private readonly IRepository _complexRepository;
    private readonly ILogger<AmenityController> log;
    public AmenityController(IRepository complexRepository, ILogger<AmenityController> logger)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
      log = logger;
    }

    #region GET

    /// <summary>
    /// (GET) Call Repository to get all exixted amenities from the database
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenities")]
    //GET: api/amenity/amenities
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetAmenitiesAsync()
    {

      try
      {
        IEnumerable<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListAsync();
        log.LogInformation("(API)list of all amenity is found");

        return Ok(amenities);
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
    /// (GET) Call Repository to get amenities from the database for specific room by room Id
    /// </summary>
    /// <param name="roomGuid"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenitiesroom/{roomGuid}")]
    //GET: api/amenity/amenitiesroom/{roomGuid}
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetRoomAmenitiesAsync([FromRoute]Guid roomGuid)
    {
      try
      {
        IEnumerable<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListByRoomIdAsync(roomGuid);
        log.LogInformation($"(API)list of amenity for room Id: {roomGuid} is found");

        return Ok(amenities);

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
    /// (GET) Call Repository to get amenities from the database for specific complex by complex Id
    /// </summary>
    /// <param name="complexGuid"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenitiescomplex/{complexGuid}")]
    //GET: api/amenity/amenitiescomplex/{complexGuid}
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetComplexAmenitiesAsync([FromRoute]Guid complexGuid)
    {
      try
      {
        IEnumerable<Logic.Amenity> amenities = await _complexRepository.ReadAmenityListByComplexIdAsync(complexGuid);
        log.LogInformation($"(API)list of amenity for room Id: {complexGuid} is found");

        return Ok(amenities);
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

    #endregion

    #region POST

    /// <summary>
    /// (POST) Call Repository to insert a new Amenity into the database
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostAmenity")]
    //POST: api/amenity/addamenity
    public async Task<ActionResult> PostAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      Logic.Amenity amen = new Logic.Amenity()
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.CreateAmenityAsync(amen);
        log.LogInformation($"(API)new amenity: {amen.AmenityType} is added");

        return StatusCode(201);
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

    #endregion

    #region PUT

    /// <summary>
    /// (PUT) Call Repository to update existed single amenity in the database
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("PutAmenity")]
    //PUT: api/amenity/PutAmenity
    public async Task<ActionResult> PutAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      Logic.Amenity amenity = new Logic.Amenity()
      {
        AmenityId = apiAmenity.AmenityId,
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.UpdateAmenityAsync(amenity);
        log.LogInformation($"(API)new amenity: {amenity.AmenityType} is updated.");

        return StatusCode(201);
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

    #endregion

    #region DELETE

    /// <summary>
    /// (DELETE) Call Repository to delete existed single amenity in the database
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("deleteAmenity")]
    //DELETE: api/amenity/deleteAmenity
    public async Task<ActionResult> DeleteAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      Logic.Amenity amenity = new Logic.Amenity()
      {
        AmenityId = apiAmenity.AmenityId,
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.DeleteAmenityAsync(amenity);
        log.LogInformation($"(API)amenity: {amenity.AmenityType} is deleted");

        return StatusCode(201);
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

    #endregion

  }//end of class
}
