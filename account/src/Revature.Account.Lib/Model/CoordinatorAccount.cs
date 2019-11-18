using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.Lib.Model
{
	public class CoordinatorAccount
	{
		private string _email;
		private string _name;
    private string _password;

    public Guid CoordinatorId { get; set; }
		public string Name 
		{
			get { return _name;  }
			set
			{
				ValidateInput(value); // really need it?
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
		public string Password
    {
      get { return _password; }
      set
      {

      }
    }
		public string TrainingName { get; set; }
		public string TrainingAddress { get; set; }
    public ICollection<Notification> Notification { get; set; }

		private void ValidateInput(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("Your Input cannot be null", nameof(value));
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("Your Input cannot be empty string.", nameof(value));
			}

		}
	}
}
