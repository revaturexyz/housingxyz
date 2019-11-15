using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xyz.AccountService.Api.Models
{
	public class CoordinatorViewModel
	{
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string TrainingName { get; set; }
		public string TrainingAddress { get; set; }
		public NotificationViewModel Notification { get; set; }
	}
}
