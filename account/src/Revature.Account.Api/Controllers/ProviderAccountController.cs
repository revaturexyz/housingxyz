using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Revature.Account.Api.Models;
using Revature.Account.Lib.Interface;

namespace Revature.Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderAccountController : ControllerBase
    {

		private readonly IGenericRepository _repo;

		public ProviderAccountController(IGenericRepository urepo)
		{
			_repo = urepo ?? throw new ArgumentNullException("Cannot be null.", nameof(urepo));
		}


		// GET: api/ProviderAccount/5
		[HttpGet("{id}", Name = "GetProviderAccountById")]
        public async Task<ActionResult> Get(Guid providerId)
        {
			var x = await _repo.GetProviderAccountById(providerId);
			if (x == null)
			{
				return NotFound();
			}
			return Ok(new ProviderViewModel()
			{
				ProviderId = x.ProviderId,				
				CoordinatorId = x.CoordinatorId,
				Name = x.Name,
				Status = x.Status,
				Password = x.Password,
				AccountCreated = x.AccountCreated,
				Expire = x.Expire
			});
		}

        // POST: api/ProviderAccount
        [HttpPost]
		public async Task<IActionResult> Post([FromBody] ProviderViewModel newProvider)
        {
			try
			{
				Lib.Model.ProviderAccount mappedProvider = new Lib.Model.ProviderAccount()
				{
					ProviderId = Guid.NewGuid(),
					Name = newProvider.Name,
					Password = newProvider.Password,
					Status = "Pending",
					AccountCreated = DateTime.Now,
					Expire = DateTime.Now.AddDays(7)
				};

				_repo.AddNewProviderAccount(mappedProvider);
				await _repo.Save();

				var newAddedProvider = await _repo.GetProviderAccountById(mappedProvider.ProviderId);

				return CreatedAtRoute("GetProviderById", new { id = newAddedProvider.ProviderId }, newAddedProvider);
			}
			catch
			{
				return BadRequest();
			}
		}

		// PUT: api/ProviderAccount/5
		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch(Guid providerId, [FromBody] ProviderViewModel provider)
		{
			var existingProvider = await _repo.GetProviderAccountById(providerId);

			if (existingProvider != null)
			{
				existingProvider.Name = provider.Name;
				existingProvider.Password = provider.Password;
				existingProvider.AccountCreated = DateTime.Now;

				await _repo.UpdateProviderAccount(existingProvider);
				await _repo.Save();
				return NoContent();
			}
			return NotFound();
		}
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid providerId)
        {
			var existingProvider = await _repo.GetProviderAccountById(providerId);
			if (existingProvider != null)
			{
				await _repo.DeleteProviderAccount(providerId);
				await _repo.Save();
				return NoContent();
			}
			return NotFound();
		}
    }
}
