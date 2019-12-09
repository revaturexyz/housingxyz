using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revature.Complex.Api.Models;
using Revature.Complex.Lib.Interface;
using Logic = Revature.Complex.Lib.Models;

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
    /// (GET)
    /// Call Repository to get all existed amenities from the database
    /// without any parameter. Then return it as enumerable collections of amenities
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
        log.LogInformation("a list of all amenities was found");

        return Ok(amenities);
      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to find amenity", ex);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Repository to get amenities from the database
    /// for single specific room by room Id as parameter
    /// Then return it as enumerable collections of amenities
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
        log.LogInformation("a list of amenities for room Id: {roomGuid} was found", roomGuid);

        return Ok(amenities);

      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to find amenity for room id: {roomGuid}", ex, roomGuid);
        return StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// (GET)
    /// Call Repository to get amenities from the database
    /// for specific complex by complex Id as parameter
    /// then return it as enumerable collections of amenities
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
        log.LogInformation("a list of amenities for complex Id: {complexGuid} was found", complexGuid);

        return Ok(amenities);
      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to find amenity for complex id: {complexGuid}", ex, complexGuid);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region POST

    /// <summary>
    /// (POST)
    /// Call Repository to insert a new Amenity into the database
    /// Need to take a Api amenity model as parameter
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostAmenity")]
    //POST: api/amenity/addamenity
    public async Task<ActionResult> PostAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      var amen = new Logic.Amenity()
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.CreateAmenityAsync(amen);
        log.LogInformation("new amenity: {amen.AmenityType} is added", amen.AmenityType);

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to create amenity", ex);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region PUT

    /// <summary>
    /// (PUT)
    /// Call Repository to update existed single amenity in the database
    /// Need to take an Api Amenity model as parameter 
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("PutAmenity")]
    //PUT: api/amenity/PutAmenity
    public async Task<ActionResult> PutAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      var amenity = new Logic.Amenity()
      {
        AmenityId = apiAmenity.AmenityId,
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.UpdateAmenityAsync(amenity);
        log.LogInformation("new amenity: {amenity.AmenityType} is updated.", amenity.AmenityType);

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to update amenity", ex);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

    #region DELETE

    /// <summary>
    /// (DELETE)
    /// Call Repository to delete existed single amenity in the database
    /// Need to take an Api Amenity model as parameter
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("deleteAmenity")]
    //DELETE: api/amenity/deleteAmenity
    public async Task<ActionResult> DeleteAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      var amenity = new Logic.Amenity()
      {
        AmenityId = apiAmenity.AmenityId,
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.DeleteAmenityAsync(amenity);
        log.LogInformation("amenity: {amenity.AmenityType} is deleted", amenity.AmenityType);

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        log.LogError("{ex}, unable to delete amenity", ex);
        return StatusCode(500, ex.Message);
      }
    }

    #endregion

  }//end of class
}
