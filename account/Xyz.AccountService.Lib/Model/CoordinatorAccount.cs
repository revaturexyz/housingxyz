using System;
using System.Collections.Generic;
using System.Text;

namespace Xyz.AccountService.Lib.Model
{
	public class CoordinatorAccount
	{
		private string _email;
		private string _name;

		public Guid CoordinatorId { get; set; }
		public string Name 
		{
			get { return _name;  }
			set
			{
				ValidateName(value); // really need it?
			}
		}
		public string Email
		{
			get { return _email; }
			set 
			{
				try
				{
					System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(value);
				}
				catch (FormatException ex)
				{
					throw ex;
				}
				_email = value;
			}
		}
		public string Password { get; set; }
		public string TrainingCenterLocation { get; set; }
		public string TrainingAddress { get; set; }
		public Notification Notification { get; set; }

		private void ValidateName(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("Name cannot be null", nameof(value));
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("Name cannot be empty string.", nameof(value));
			}

		}
	}
}
