using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.Lib.Model
{
	public class CoordinatorAccount
	{
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string TrainingName { get; set; }
		public string TrainingAddress { get; set; }
		public Notification Notification { get; set; }
	}
}
