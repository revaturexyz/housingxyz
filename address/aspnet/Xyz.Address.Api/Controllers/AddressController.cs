using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Helpers = Xyz.Address.Api.Helpers;
using Domain = Xyz.Address.Lib.Models;
using API = Xyz.Address.Api.Models;
using Data = Xyz.Address.Dbx.Models;
using Filters = Xyz.Address.Api.Filters;
using Xyz.Address.Api.Helpers;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Xyz.Address.Dbx.Repositories;

namespace Xyz.Address.Api.Controllers
{
    ///<summary> Controller for Address CRUD Endpoints. </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public AddressHelper _addressHelper = Helpers.AddressHelper.Instance();

        ///<summary> Endpoint to get all Addresses. </summary>
        ///<return> if(found) returns all addresses in a status code 200 Response (else) returns a status code 404 response.</return>
        [HttpGet]
        [Route("getaddresslist")]
        public IActionResult Get()
        {
          var addressList = _addressHelper.Get();
          if(addressList.Count() == 0) return NotFound("Unable to fetch list of addresses");
          return Ok(addressList); 
        }

        ///<summary> Endpoint to get a specific address given a Key </summary>
        ///<param name="Key"> Key to get the Address </param>
        ///<return> if(found) returns a address in a status code 200 Response (else) returns a status code 404 response. </return>
        [HttpGet]
        [Route("getaddress")]
        public IActionResult Get([FromQuery] Guid Key)
        {
            var addressModel = _addressHelper.Get(Key);
            if (addressModel == null)  return NotFound(string.Format("Key {0}, is invalid,", Key));
            return Ok(addressModel);   
        }
        
        ///<summary> Endpoint to add a address, which is validated using Google Maps Geocoding API </summary>
        ///<param name="_newAddress"> The model that is created from the Query String </param>
        ///<return> if(success) returns a status code 200 Response (else) returns a status code 404 response. </return>
        [HttpPost]
        [Route("postaddress")]
        [Filters.ValidateAddressWithGoogle]
        public IActionResult Post([FromQuery] API.AddressApiModel _newAddress)
        {
          if(ModelState.IsValid && _addressHelper.Post(_newAddress.ConvertToEntity())){
            return Ok("Address was sucessfully added");
          }
          return NotFound("Could not add the given address");
        }

        ///<summary> Endpoint to update a address, which is validated using Google Maps Geocoding API </summary>
        ///<param name="_updateAddress"> The model that is created from the Query String </param>
        ///<return> if(success) returns a status code 200 Response (else) returns a status code 404 response. </return>
        [HttpPut]
        [Route("putaddress")]
        [Filters.ValidateAddressWithGoogle]
        public IActionResult Put([FromQuery] API.AddressApiModel _updateAddress)
        {
          if(ModelState.IsValid && _addressHelper.Put(_updateAddress.ConvertToEntity())){
            return Ok("Successfully updated address");
          }
          return NotFound("Unable to update address");
        }

        ///<summary> Endpoint to delete a specific address given a Key </summary>
        ///<param name="key"> Key to delete the Address </param>
        ///<return> if(deleted) returns a status code 200 Response (else) returns a status code 404 response. </return>
        [HttpDelete]
        [Route("deleteaddress")]
        public IActionResult Delete([FromQuery] Guid key)
        {
          if(!_addressHelper.Delete(key)) return NotFound("Unable to delete address");
          return Ok("Successfully deleted address");
        }
    }
}