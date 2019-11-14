using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyz.Tenant.Lib.Interface;

namespace Xyz.Tenant.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TenantsController : ControllerBase
  {

    private readonly ITenantRepository _tenantRepository;

    public TenantsController(ITenantRepository tenantRepository)
    {
      _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository), "Tenant repo cannot be null");
    }

    // GET: api/Tenants
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    /// <summary>
    /// Get all tenants by tenantID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/Tenants/5
    [HttpGet("{id}", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public string Get(int id)
    {
      //try
      //{
      //  var tenant = await _tenantRepository.GetTenantsByIdAsync(tenantId);
      //  var apiTenant = tenant.Select(t => new Tenants)
      //}
      return "value";
    }

    // POST: api/Tenants
    [HttpPost]
    public void Post([FromBody] string value)
    {
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
