using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Provider.Lib.Exceptions;
using Xyz.Provider.Lib.Interface;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GenderController : ControllerBase
  {
    private readonly IGenderRepository _genderRepository;

    public GenderController(IGenderRepository genderRepository)
    {
      _genderRepository = genderRepository ?? throw new ArgumentNullException(nameof(genderRepository), "Gender repo cannot be null");
    }

    /// <summary>
    /// Gets all genders
    /// </summary>
    /// <returns></returns>
    // GET: api/gender
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Gender>>> GetAsync()
    {
      var genders = await _genderRepository.GetAllGenderTypesAsync();
      return Ok(genders);
    }

    /// <summary>
    /// Update the gender of a room
    /// </summary>
    /// <param name="id"></param>
    /// <param name="gender"></param>
    /// <returns></returns>
    // PUT: api/gender/room/5
    [HttpPut("room/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PutAsync([FromRoute]int id, [FromBody]Gender gender)
    {
      try
      {
        await _genderRepository.UpdateGenderByRoomIdAsync(gender, id);
        return Ok();
      }
      catch (ArgumentOutOfRangeException)
      {
        return NotFound();
      }
      catch (ArgumentNotFoundException e)
      {
        if (e.Name is "Room")
        {
          return NotFound();
        }
        return BadRequest(e.Message);
      }
      catch (ArgumentException e)
      {
        return BadRequest(e.Message);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
