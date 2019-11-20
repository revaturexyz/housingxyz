using System;

namespace Revature.Account.Lib.Model
{
  public class ProviderAccount
  {
    private string _name;
    public Guid ProviderId { get; set; } = Guid.NewGuid();
    public CoordinatorAccount Coordinator { get; set; }

    public string Name
    {
      get { return _name; }
      set
      {
        NotNullOrEmpty(value);
        _name = value;
      }
    }

    public string Status { get; set; }
    /// <summary>
    /// Date and time the account was created at, expressed in the format 11:59:59.
    /// </summary>
    public DateTime AccountCreatedAt { get; set; }
    /// <summary>
    /// Date and time the account expires at.
    /// </summary>
    public DateTime AccountExpiresAt { get; set; }

    private void NotNullOrEmpty(string value)
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
