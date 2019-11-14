using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.DataAccess.Entities
{
	public class ProviderAccount
	{
		public ProviderAccount()
		{
			Notification = new HashSet<Notification>();
		}
		public int ProviderId { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public DateTime AccountCreated { get; set; }
		public string Status { get; set; }
		public DateTime Expire { get; set; }

		//set the primary key with guid
		public Guid CoordinatorId { get; set; }
		public CoordinatorAccount Coordinator { get; set; }

		public virtual ICollection<Notification> Notification { get; set; }
	}
}
