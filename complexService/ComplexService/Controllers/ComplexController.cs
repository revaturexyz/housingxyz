using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplexServiceApi.Controllers.Model;
using ComplexServiceDatabase.Model;
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
        // POST: api/complex/provider/5
        [HttpPost("CreateComplex")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task CreateComplex([FromBody] APIComplex)
        {
            Complex complex = new Complex()
            {

            }

        }
        
        #endregion

    }
}