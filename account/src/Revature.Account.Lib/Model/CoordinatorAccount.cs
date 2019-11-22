using System;
using System.Collections.Generic;

namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Contains information on a single training coordinator who is
  /// tied to a single training center.
  /// </summary>
  public class CoordinatorAccount
  {
    private string _email;
    private string _name;
    private string _trainingCenterName;
    private string _trainingCenterAddress;

    public Guid CoordinatorId { get; set; } = Guid.NewGuid();
    public virtual List<Lib.Model.Notification> Notifications { get; set; }

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
        // This line simply uses the instantiation of the MailAddress object
        // to check if the email is valid. Object is thrown away.
        _ = new System.Net.Mail.MailAddress(value);
        _email = value;
      }
    }

    /// <summary>
    /// Name of the training center associated with the coordinator.
    /// </summary>
    ///
    //public string TrainingCenterName { get; set; }
    
    public string TrainingCenterName
    { 
      get
      {
        return this._trainingCenterName;
      }
      set
      {
        NotNullOrEmpty(value);
        _trainingCenterName = value;
      }
    }
    /// <summary>
    /// Address of the training center associated with the coordinator.
    /// </summary>
    public string TrainingCenterAddress
    {
      get
      {
        return this._trainingCenterAddress;
      }
      set
      {
        NotNullOrEmpty(value);
        _trainingCenterAddress = value;
      }
    }

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
