using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xyz.AccountService.Api.Models
{
	public class NotificationViewModel
	{
		public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public String Status { get; set; }
		public DateTime AccountExpire { get; set; }
	}
}
