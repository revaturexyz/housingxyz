using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Tenant.Lib.Interface;
using Xyz.Tenant.Api;
using Xyz.Tenant.Api.Models;

namespace Xyz.Tenant.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TenantsController : ControllerBase
  {

    //private readonly IDataAccess db;

    //public TenantsController(IDataAccess dataAccess)
    //{
    //  db = dataAccess;
    //}
    private readonly ITenantRepository _tenantRepository;

    public TenantsController(ITenantRepository tenantRepository)
    {
      _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository), "Tenant Cannot be null");
    }

    /// <summary>
    /// Get all tenants
    /// </summary>
    /// <returns></returns>
        // GET: api/Tenants
    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ApiTenant>>> GetAllAsync()
    {
      var tenant = await _tenantRepository.GetAllAsync();

      return tenant.Select(t => new ApiTenant
      {
        Id = t.Id,
        FirstName = t.FirstName,
        LastName = t.LastName,
        Email = t.Email,
        AddressId = t.AddressId,
        RoomId = t.RoomId,
        CarId = t.CarId

      });
      
    }
    /// <summary>
    /// Get Tenant by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/Tenants/5
    //api/[controller]
    [HttpGet("{id}", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Xyz.Tenant.Api.Models.ApiTenant>> GetByIdAsync([FromRoute] int id)
    {
      try
      {
        var tenant = await _tenantRepository.GetByIdAsync(id);//this is a repository function that should be async and return Task<IEnumerable<Xyz.Tenant.Lib.Models.Tenant>>
        var apiTenant = new ApiTenant { Id = tenant.Id,
                                        FirstName = tenant.FirstName,
                                        LastName = tenant.LastName,
                                        Email = tenant.Email,
                                        AddressId = tenant.AddressId,
                                        RoomId = tenant.RoomId,
                                        CarId = tenant.CarId };                                             //given an int Id
        return Ok(apiTenant);

      }
      catch(ArgumentException)
      {
        return NotFound();
      }
      catch(Exception e)
      {
        return StatusCode(500, e.Message);
      }

    }
    /// <summary>
    /// Posts Tenants to Db
    /// </summary>
    /// <param name="value"></param>
    // POST: api/Tenants
    [HttpPost("RegisterTenant", Name = "RegisterTenant")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async void PostAsync([FromBody] ApiTenant tenant)
        {
      //TODO: Implement PostAsync
      //    var libTenant = await _tenantRepository.
      //    {
      //      Id = c
      //    }
      //var result = await _complexRepository.AddAsync(libTenant, providerId);
      //return Created($"api/Complex/{result.ComplexId}", ApiModelFactory.MakeApiComplex(result));
      
        }

        // PUT: api/Tenants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
