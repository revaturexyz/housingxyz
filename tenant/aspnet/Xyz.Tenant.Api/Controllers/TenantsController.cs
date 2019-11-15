using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Tenant.Lib.Interface;
using Xyz.Tenant.Api;
using Xyz.Tenant.Api.Models;
//using Xyz.Tenant.Lib.Exceptions;

namespace Xyz.Tenant.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TenantsController : ControllerBase
  {

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
        var apiTenant = new ApiTenant
        {
          Id = tenant.Id,
          FirstName = tenant.FirstName,
          LastName = tenant.LastName,
          Email = tenant.Email,
          AddressId = tenant.AddressId,
          RoomId = tenant.RoomId,
          CarId = tenant.CarId
        };
        return Ok(apiTenant);

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
    /// Posts Tenants to Db
    /// </summary>
    /// <param name="value"></param>
    // POST: api/Tenants
    [HttpPost("RegisterTenant", Name = "RegisterTenant")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiTenant>> PostAsync([FromBody] ApiTenant tenant)
    {
      try
      {
        var newTenant = new Tenant
        {
          Id = tenant.Id,
          FullName = tenant.FullName,
          FirstName = tenant.FirstName,
          LastName = tenant.LastName,
          Email = tenant.Email,
          AddressId = tenant.AddressId,
          RoomId = tenant.RoomId,
          CarId = tenant.CarId
        };

        var result = await _tenantRepository.AddAsync(newTenant);

        return Created($"api/Tenant/{result.TenantId}", tenant);
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

 
  ///// <summary>
  ///// Update the Tenant
  ///// </summary>
  ///// <param name="id"></param>
  ///// <param name="gender"></param>
  ///// <returns></returns>
  ///// 
  //// PUT: api/Tenants/5
  //[HttpPut("{id}")]
  //[ProducesResponseType(StatusCodes.Status200OK)]
  //[ProducesResponseType(StatusCodes.Status400BadRequest)]
  //[ProducesResponseType(StatusCodes.Status404NotFound)]
  //public async Task<ActionResult> PutAsync([FromRoute]int id, [FromBody]Tenant tenant)
  //{
  //  try
  //  {
  //    await _tenantRepository.UpdateTenant(id, tenant); //this is a repo function that takes in tenant id and tenant object
  //    return Ok();
  //  }
  //  catch(ArgumentOutOfRangeException)
  //  {
  //    return NotFound();  
  //  }
  //  catch(ArgumentNotFoundException e)
  //  {
  //    if (e.Name is "Tenant")
  //    {
  //      return NotFound();
  //    }
  //    return BadRequest(e.Message);
  //  }
  //  catch (ArgumentException e)
  //  {
  //    return BadRequest(e.Message);
  //  }
  //  catch (Exception e)
  //  {
  //    return StatusCode(500, e.Message);
  //  }
  //}
  //}

  //// DELETE: api/ApiWithActions/5
  //[HttpDelete("{id}")]
  //public void Delete(int id)
  //{

  //}

