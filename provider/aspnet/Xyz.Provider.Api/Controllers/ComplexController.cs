using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Interface;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ComplexController : ControllerBase
  {
    private readonly IComplexRepository _complexRepository;

    public ComplexController(IComplexRepository complexRepository)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
    }

    /// <summary>
    /// Gets all complexes by provider ID
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    // GET: api/complex/provider/5
    [HttpGet("provider/{providerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiComplex>>> GetByProviderIdAsync([FromRoute]int providerId)
    {
      try
      {
        var comps = await _complexRepository.GetComplexesByProviderIdAsync(providerId);

        var apiComps = comps.Select(c => new ApiComplex
        {
          ComplexId = c.ComplexId,
          ComplexName = c.ComplexName,
          ContactNumber = c.ContactNumber,
          ApiAddress = c.Address is null ? null : new ApiAddress
          {
            AddressId = c.Address.AddressId,
            StreetAddress = c.Address.StreetAddress,
            City = c.Address.City,
            State = c.Address.State,
            ZipCode = c.Address.Zip
          },
          ApiProvider = c.Provider is null ? null : new ApiProvider
          {
            ProviderId = c.Provider.ProviderId,
            Username = c.Provider.Username,
            ContactNumber = c.Provider.ContactNumber
          }
        });
        return Ok(apiComps);
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
    /// Gets all Amenities by complex ID
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    // GET: api/complex/5/amenity
    [HttpGet("{complexId}/amenity")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiAmenity>>> GetAmenitiesByComplexIdAsync([FromRoute]int complexId)
    {
      try
      {
        IEnumerable<Amenity> amenities = await _complexRepository.GetAmenitiesByComplexIdAsync(complexId);
        var apiAmen = amenities.Select(a => new ApiAmenity
        {
          AmenityId = a.AmenityId,
          Amenity = a.AmenityType
        });
        return Ok(apiAmen);
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

    // POST: api/complex/provider/5
    [HttpPost("provider/{providerId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiComplex>> PostAsync([FromRoute]int providerId, [FromBody]ApiComplex complex)
    {
      try
      {
        var newComplex = new Complex
        {
          ComplexName = complex.ComplexName,
          ContactNumber = complex.ContactNumber
        };
        if (complex.ComplexId != 0)
        {
          newComplex.ComplexId = complex.ComplexId;
        }
        if (complex.ApiAddress != null)
        {
          if (complex.ApiAddress.AddressId != 0)
          {
            newComplex.Address = new Address
            {
              AddressId = complex.ApiAddress.AddressId,
              StreetAddress = complex.ApiAddress.StreetAddress,
              City = complex.ApiAddress.City,
              State = complex.ApiAddress.State,
              Zip = complex.ApiAddress.ZipCode
            };
          }
          else
          {
            newComplex.Address = new Address
            {
              StreetAddress = complex.ApiAddress.StreetAddress,
              City = complex.ApiAddress.City,
              State = complex.ApiAddress.State,
              Zip = complex.ApiAddress.ZipCode
            };
          }
        }

        if (complex.ApiTrainingCenter != null)
        {
          newComplex.Center = new TrainingCenter
          {
            CenterId = complex.ApiTrainingCenter.CenterId,
            CenterName = complex.ApiTrainingCenter.CenterName,
            ContactNumber = complex.ApiTrainingCenter.ContactNumber,
          };
        }

        var result = await _complexRepository.AddAsync(newComplex, providerId);
        return Created($"api/Complex/{result.ComplexId}", ApiModelFactory.MakeApiComplex(result));
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
  }
}
