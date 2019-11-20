using System;
using System.Collections.Generic;

namespace Revature.Account.Lib.Model
{
  public class CoordinatorAccount
  {
    private string _email;
    private string _name;
    public Guid CoordinatorId { get; set; } = Guid.NewGuid();

    public string Name
    {
      get { return _name; }
      set
      {
        NotNullOrEmpty(value);
        _name = value;
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

    /// <summary>
    /// Name of the training center associated with the coordinator.
    /// </summary>
    public string TrainingCenterName { get; set; }
    /// <summary>
    /// Address of the training center associated with the coordinator.
    /// </summary>
    public string TrainingCenterAddress { get; set; }
    public virtual List<Lib.Model.Notification> Notifications { get; set; }

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
