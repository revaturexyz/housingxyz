using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Account.Api.Models
{
	public class ProviderViewModel
	{
		public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Status { get; set; }
		public DateTime AccountCreated { get; set; }
		public DateTime Expire { get; set; }
		public List<NotificationViewModel> Notification { get; set; }
	}
}
