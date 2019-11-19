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
    public class NotificationController : ControllerBase
    {
		private readonly IGenericRepository _repo;

		public NotificationController(IGenericRepository urepo)
		{
			_repo = urepo ?? throw new ArgumentNullException("Cannot be null.", nameof(urepo));
		}

		// GET: api/Notification/5
		[HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult> GetNotificationByProviderId(Guid providerId)
        {
			    var x = await _repo.GetProviderAccountById(providerId);
			    if (x == null)
			    {
				    return NotFound();
			    }

			    var nofi = await _repo.GetNotificationById(providerId);

          return Ok(new NotificationViewModel
			    {
				    ProviderId = nofi.ProviderId,
				    CoordinatorId = nofi.CoordinatorId,
				    Status = nofi.Status,
				    AccountExpire = nofi.AccountExpire

			    }); 
        }

        // POST: api/Notification
        [HttpPost]
        public async Task<ActionResult> Post([FromBody, Bind("ProviderId, CoordinatorId")] NotificationViewModel notification)
        {
			try
			{
				Lib.Model.Notification mappedNotification = new Lib.Model.Notification()
				{
					ProviderId = notification.ProviderId,
					CoordinatorId = notification.CoordinatorId,
					Status = "Pending",
					AccountExpire = DateTime.Now.AddDays(7)
				};

				_repo.AddNewNotification(mappedNotification);
				await _repo.Save();

				var newAddedNotification = await _repo.GetNotificationById(mappedNotification.ProviderId);

				return CreatedAtRoute("Get", new { id = newAddedNotification.ProviderId }, newAddedNotification);

			}
			catch
			{
				return BadRequest();
			}
		}

        // PATCH: api/Notification/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(Guid providerId, [FromBody] NotificationViewModel notification)
        {
			var provider = await _repo.GetProviderAccountById(providerId);
			if (provider == null)
			{
				return NotFound();
			}
			var existingNotification = await _repo.GetNotificationById(providerId);
			if (existingNotification != null)
			{
				existingNotification.Status = notification.Status;
				if (existingNotification.Status == "Under Review") 
				{
					existingNotification.AccountExpire = DateTime.Now.AddDays(30);
				}
				if (existingNotification.Status == "Rejected")
				{
					existingNotification =null;
					provider = null;
				}
				await _repo.UpdateNotification(existingNotification);
				await _repo.UpdateProviderAccount(provider);
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
				await _repo.DeleteNotificationById(providerId);
				await _repo.Save();
				return NoContent();
			}
			return NotFound();
		}
	}
}
