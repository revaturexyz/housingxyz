using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Api.Models;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AmenityController : ControllerBase
  {
    private readonly IRepository _complexRepository;

    public AmenityController(IRepository complexRepository)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenities")]
    //GET: api/complex/amenities
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetAmenitiesAsync()
    {

      try
      {
        return Ok(await _complexRepository.ReadAmenityListAsync());
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
    /// This should create a new amenity type to add to database
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("PostAmenity")]
    //POST: api/complex/addamenity
    public async Task<ActionResult> PostAmenityAsync([FromBody]ApiAmenity apiAmenity)
    {
      Logic.Amenity amen = new Logic.Amenity()
      {
        AmenityType = apiAmenity.AmenityType,
        Description = apiAmenity.Description
      };
      try
      {
        await _complexRepository.CreateAmenityAsync(amen);
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

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("PutAmenity")]
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

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("deleteAmenity")]
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenitiesroom/{roomGuid}")]
    //GET: api/complex/amenitiesroom/{roomGuid}
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetRoomAmenitiesAsync([FromRoute]Guid roomGuid)
    {
      try
      {
        var x = await _complexRepository.ReadAmenityListByRoomIdAsync(roomGuid);
        return Ok(x);

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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenitiescomplex/{complexGuid}")]
    //GET: api/complex/amenitiescomplex/{complexGuid}
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetComplexAmenitiesAsync([FromRoute]Guid complexGuid)
    {
      try
      {
        var x = await _complexRepository.ReadAmenityListByComplexIdAsync(complexGuid);
        return Ok(x);
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


  }//end of class
}
