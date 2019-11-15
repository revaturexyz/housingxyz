using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Revature.Tenant.Api.Controllers
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


        // GET: api/Tenants
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tenants/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
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
