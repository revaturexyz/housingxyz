using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplexServiceApi.Controllers.Model;
using ComplexServiceLogic.Model;
using ComplexServiceDatabase.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


/*
 * 
 * when a provider adds a new complex,

 * required to give number of apartments
required to give capacity per apartment
required to give available amenities both apartment and community
required to give lease terms per apartment
required to give address of both apartment and community
manager is notified for approval (note: manager should be coordinator)

 * 
 * 
 * 
 * 
 */
namespace ComplexServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexController : Controller
    {
        private readonly IComplexRepository _complexRepository;

        public ComplexController(IComplexRepository complexRepository)
        {
            _complexRepository = complexRepository ?? throw new ArgumentNullException(nameof(complexRepository), "Complex repo cannot be null");
        }


        #region Create
        // POST: api/complex/createcomplex
        [HttpPost("CreateComplex")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIComplex>> CreateComplex([FromBody] APIComplex apiComplex)
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

            Complex complex1 = new Complex()
            {
                AddressId = AddressToSend.AddressGuid,
                ComplexId = new Guid(),
                ProviderId = apiComplex.ProviderID,
                ContactNumber = apiComplex.ContactNumber,
                ComplexName = apiComplex.ComplexName
                
                
            };




            #region code call repo

            AmenityComplex AmentityComplex1 = new AmenityComplex();
            foreach (var type in apiComplex.ComplexAmentiy)
            {
                var id = _complexRepository.ReadAmenittiesbyString(type);
                AmentityComplex1.AmenityId = id;
                AmentityComplex1.ComplexId = complex1.ComplexId;
                try
                {
                    await _complexRepository.CreateAmenityComplexAsync(AmentityComplex1);

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
                await _complexRepository.CreateComplexAsync(complex1);

                #region Code to sent address to other serivce Need to fill
               // serviceBus.sentInfor(To, data)


                #endregion
                
                return Created($"api/Complex/{complex1.ComplexId}", apiComplex);

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
        [HttpPost("CreateAmentity")]
        public ActionResult CreateAmenity(Amenity apiAmenity)
        {
          return  _complexRepository.CreateAmenityAsync(amenity)
        }


        #endregion

        #region Get
        [HttpGet("{complexGuid}")]
        public async Task<ActionResult<APIComplex>> GetComplexInfo([FromRoute]Guid complexGuid)
        {
            var x = await _complexRepository.ReadComplexAsync(complexGuid);
            //var address = await serviceBus.GetAddress(To, x.AddressId);
            return new APIComplex()
            {
                //code to get address in this line
                //Address = address;
                ComplexName = x.ComplexName,
                ContactNumber = x.ContactNumber,
                ProviderID = x.ProviderId
                
            };

            
        }

        [HttpGet("amenitiesroom/{roomGuid}")]
        public ActionResult<IEnumerable<Amenity>> GetRoomAmenities([FromRoute]Guid roomGuid)
        {
            try
            {
                var x = _complexRepository.ReadAmenityListByRoomId(roomGuid);
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

        [HttpGet("amenitiescomplex/{roomGuid}")]
        public ActionResult<IEnumerable<Amenity>> GetComplexAmenities([FromRoute]Guid roomGuid)
        {
            try
            {
                var x = _complexRepository.ReadAmenityListByComplexId(roomGuid);
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

        [HttpGet("amenities")]

        public ActionResult<IEnumerable<Amenity>> GetAllAmenities()
        {

            try
            {
                return Ok(_complexRepository.ReadAmenityList());
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
    }



}