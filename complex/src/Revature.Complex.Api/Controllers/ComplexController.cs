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

    #region Create
    // POST: api/complex/createcomplex
    [HttpPost("PostComplex")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //GET: api/complex/PostComplex
    public async Task<ActionResult<ApiComplex>> PostComplexAsync([FromBody]ApiComplex apiComplex)
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
        ProviderId = apiComplex.ProviderId,
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
          Guid id = Guid.NewGuid(); //_complexRepository.ReadAmenittiesbyString(type);
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

    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("Getallcomplex")]
    //POST: api/complex/Getallcomplex
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

    #endregion

    #region Get
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{complexGuid}")]
    //GET: api/complex/{complexGuid}
    public async Task<ActionResult<ApiComplex>> GetComplexByIdAsync([FromRoute]Guid complexGuid)
    {
      var x = await _complexRepository.ReadComplexAsync(complexGuid);
      //var address = await serviceBus.GetAddress(To, x.AddressId);
      return new ApiComplex()
      {

        //Address = address;
        ComplexName = x.ComplexName,
        ContactNumber = x.ContactNumber,
        ProviderId = x.ProviderId

      };
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("provierId/{providerID}")]
    //GET: api/complex/provierId/{providerID}
    public async Task<ActionResult<IEnumerable<ApiComplex>>> GetComplexListByProviderId([FromRoute]Guid providerId)
    {
      try
      {
        List<Logic.Complex> complices = await _complexRepository.ReadComplexByProviderIdAsync(providerId);
        List<List<Logic.Amenity>> amenities = new List<List<Logic.Amenity>>();

        foreach( Logic.Complex complex in complices)
        {
          amenities.Add(await _complexRepository.ReadAmenityListByComplexIdAsync(complex.ComplexId));
        }

        //foreach complex, get address from address service
        //create Apicomplex object for each complex we have
        //return them.

        return Ok();
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
