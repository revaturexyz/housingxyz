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
  public class ComplexController : Controller
  {
    private readonly IRepository _complexRepository;

    public ComplexController(IRepository complexRepository)
    {
      _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
    }

    #region Create
    // POST: api/complex/createcomplex
    [HttpPost("CreateComplex")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiComplex>> CreateComplex([FromBody] ApiComplex apiComplex)
    {

      ApiComplexAddress CompAddr = new ApiComplexAddress()
      {
        StreetAddress = apiComplex.Address.StreetAddress,
        City = apiComplex.Address.City,
        State = apiComplex.Address.State,
        ZipCode = apiComplex.Address.ZipCode
      };

      ///this is for creating a Guid to send to address service 
      ApiAddress AddressToSend = new ApiAddress
      {
        ComplexAddress = CompAddr
      };

      Logic.Complex complex = new Logic.Complex()
      {
        AddressId = AddressToSend.AddressId,
        ComplexId = new Guid(),
        ProviderId = apiComplex.ProviderID,
        ContactNumber = apiComplex.ContactNumber,
        ComplexName = apiComplex.ComplexName
      };

      #region code call repo

      Logic.AmenityComplex AmenityComplex = new Logic.AmenityComplex();


      try
      {
        await _complexRepository.CreateComplexAsync(complex);

        foreach (var type in apiComplex.ComplexAmentiy)
        {
          int id = 1; //_complexRepository.ReadAmenittiesbyString(type);
          AmenityComplex.AmenityId = id;
          AmenityComplex.ComplexId = complex.ComplexId;

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
    /// This should create a new amenity type to add to database
    /// </summary>
    /// <param name="apiAmenity"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("AddAmenity")]
    //POST: api/complex/addamenity
    public async Task<ActionResult> AddAdditionalAmenityToDB(ApiAmenity apiAmenity)
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


    /// <summary>
    /// this is 
    /// </summary>
    /// <param name="apiComplexAmenityList"></param>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("CreateAmenity")]
    //POST: api/complex/createamenity
    /*public async Task<ActionResult> CreateAmenityComplexList(IEnumerable<ApiAmenity> apiComplexAmenityList)
    {
        AmenityComplex AmenityComplex = new AmenityComplex();
        foreach (var amenity in apiComplexAmenityList)
        {
            var id = _complexRepository.ReadAmenittiesbyString(amenity.AmenityType);
            AmenityComplex.AmenityId = id;
            AmenityComplex.ComplexId = amenity.ComplexId;
            try
            {
                // await _complexRepository._EditComplexAmenityAsync(AmenityComplex1);// code to edit in repo
                return NoContent();
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
    }*/

    #endregion

    #region Get
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexGuid}")]
    //GET: api/complex/{complexGuid}
    public async Task<ActionResult<ApiComplex>> GetComplexInfo([FromRoute]Guid complexGuid)
    {
      var x = await _complexRepository.ReadComplexAsync(complexGuid);
      //var address = await serviceBus.GetAddress(To, x.AddressId);
      return new ApiComplex()
      {

        //Address = address;
        ComplexName = x.ComplexName,
        ContactNumber = x.ContactNumber,
        ProviderID = x.ProviderId

      };


    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenitiesroom/{roomGuid}")]
    //GET: api/complex/amenitiesroom/{roomGuid}
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetRoomAmenities([FromRoute]Guid roomGuid)
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
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetComplexAmenities([FromRoute]Guid complexGuid)
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("amenities")]
    //GET: api/complex/amenities
    public async Task<ActionResult<IEnumerable<Logic.Amenity>>> GetAllAmenities()
    {

      try
      {
        return Ok( await _complexRepository.ReadAmenityListAsync());
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
    [HttpGet("provierId/{providerID}")]
    //GET: api/complex/provierId/{providerID}
    public ActionResult<IEnumerable<ApiComplex>> GetAllBaseOnProviderID([FromRoute] Guid providerID)
    {

      try
      {
        return Ok(/*_complexRepository.ReadComplexByProviderID(providerID)*/); //needs to do in repo
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

    #region Put

    /*[HttpPut("editcomplex")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public Task<ActionResult> EditComplex([FromBody] APIComplex apiComplex)// repo lacks await 
    {

        APIComplexAddress CompAddr = new APIComplexAddress()
        {
            StreetAddress = apiComplex.Address.StreetAddress,
            City = apiComplex.Address.City,
            State = apiComplex.Address.State,
            ZipCode = apiComplex.Address.ZipCode
        };

        ///this is for creating a Guid to send to address service 
        AddressToSent AddressToSend = new AddressToSent
        {
            ComplexAddress = CompAddr
        };

        Complex complex = new Complex()
        {
            AddressId = AddressToSend.AddressId,
            ComplexId = new Guid(),
            ProviderId = apiComplex.ProviderID,
            ContactNumber = apiComplex.ContactNumber,
            ComplexName = apiComplex.ComplexName


        };


        #region code call repo
        AmenityComplex AmenityComplex1 = new AmenityComplex();
        foreach (var type in apiComplex.ComplexAmentiy)
        {
            var id = _complexRepository.ReadAmenittiesbyString(type);
            AmenityComplex1.AmenityId = id;
            AmenityComplex1.ComplexId = complex.ComplexId;
            try
            {
               // await _complexRepository._EditComplexAmenityAsync(AmenityComplex1);// code to edit in repo
               return NoContent()
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


        try
        {
            // await _complexRepository.EditComplexAmenityAsync(complex);

            #region Code to sent address to other serivce Need to fill
            // serviceBus.sentInfor(To, data)


            #endregion

            return NoContent()

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

    }*/
    /*
    public async Task<ActionResult> EditComplexAmenity([FromBody] IEnumerable<ApiAmenity> amenityList)
    {
        AmenityComplex AmenityComplex = new AmenityComplex();
        foreach (var amenity in amenityList)
        {
            var id = _complexRepository.ReadAmenittiesbyString(amenity.AmenityType);
            AmenityComplex.AmenityId = id;
            AmenityComplex.ComplexId = amenity.ComplexId;
            try
            {
                 await _complexRepository.upda(AmenityComplex);// code to edit in repo
                return NoContent();
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
    }**/

    #endregion
  }
}
