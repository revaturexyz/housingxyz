using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.DataAccess.Entities
{
	public class Notification
	{
		public Guid NotificationId { get; set; }
		public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public string Status { get; set; }
		public DateTime AccountExpire { get; set; }
		public CoordinatorAccount Coordinator { get; set; }
		public ProviderAccount Provider { get; set; }
	}
}
