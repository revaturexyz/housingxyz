﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.DataAccess.Entities
{
	public class CoordinatorAccount
	{
		public CoordinatorAccount()
		{
			Notification = new HashSet<Notification>();
		}
		public int CoordinatorId { get; set; }
		public string TrainingName { get; set; }
		public string TrainingAddress { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public Guid ProviderId { get; set; }
		public  ProviderAccount Provider { get; set; }
		public virtual ICollection<Notification> Notification { get; set; }
	}
}
