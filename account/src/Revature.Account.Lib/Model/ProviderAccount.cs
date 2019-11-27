using System;

namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Contains information pertaining to a particular provider which
  /// owns one or more complexes that are providing housing to
  /// a single training center.
  /// </summary>
  public class ProviderAccount
  {
    private string _name;
    public Guid ProviderId { get; set; } = Guid.NewGuid();
    public Guid CoordinatorId { get; set; }

    public string Name
    {
      get { return _name; }
      set
      {
        NotNullOrEmpty(value);
        _name = value;
      }
    }

    public string _email;
    public string Email
    {
      get { return _email; }
      set
      {
        // This line simply uses the instantiation of the MailAddress object
        // to check if the email is valid. Object is thrown away.
        _ = new System.Net.Mail.MailAddress(value);
        _email = value;
      }
    }

    /// <summary>
    /// The current status of a provider's account.
    /// </summary>
    public Status Status { get; set; }
    /// <summary>
    /// Date and time the account was created at, expressed in the format 11:59:59.
    /// </summary>
    public DateTime AccountCreatedAt { get; set; }
    /// <summary>
    /// Date and time the account expires at.
    /// </summary>
    public DateTime AccountExpiresAt { get; set; }

    /// <summary>
    /// Checks to see if a string is either null (does not exist) or empty ( "" )
    /// </summary>
    /// <param name="value"></param>
    private void NotNullOrEmpty(string value)
    {
      if (value == null)
      {
        throw new ArgumentNullException(nameof(value), "Your Input cannot be null");
      }
      if (value.Length == 0)
      {
        throw new ArgumentException("Your Input cannot be empty string.", nameof(value));
      }
    }
  }
}
