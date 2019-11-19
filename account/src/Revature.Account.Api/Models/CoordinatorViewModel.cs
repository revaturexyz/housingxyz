using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revature.Account.Api.Models
{
	public class CoordinatorViewModel
	{
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string TrainingCenterLocation { get; set; }
		public string TrainingAddress { get; set; }
		public NotificationViewModel Notification { get; set; }
	}
}
