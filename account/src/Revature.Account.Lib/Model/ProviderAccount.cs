using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.Lib.Model
{
	public class ProviderAccount
	{
    private string _name;
    private string _password;

    public Guid ProviderId { get; set; }
		public Guid CoordinatorId { get; set; }
		public string Name
    {
      get { return _name; }
      set
      {
        ValidateInput(value);
      }
    }
		public string Password
    {
      get { return _password;  }
      set
      {
        ValidateInput(value);
      }
    }
		public string Status { get; set; }
		public DateTime AccountCreated { get; set; }
		public DateTime Expire { get; set; }
		public Notification Notification { get; set; }

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
