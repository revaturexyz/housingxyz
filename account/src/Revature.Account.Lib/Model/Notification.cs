using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.Lib.Model
{
	public class Notification
	{
		public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public String Status { get; set; }
		public DateTime AccountExpire { get; set; }
	}
}
