using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.Lib.Model
{
	public class ProviderAccount
	{
		public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Status { get; set; }
		public DateTime AccountCreated { get; set; }
		public DateTime Expire { get; set; }
		public Notification Notification { get; set; }

	}
}
