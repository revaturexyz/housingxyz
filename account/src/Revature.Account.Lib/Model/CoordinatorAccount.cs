using System;
using System.Collections.Generic;

namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Contains individual information for a coordinators whome are tied to a single training center.
  /// </summary>
  public class CoordinatorAccount
  {
    
    private string _email;
    private string _name;
    private string _trainingCenterName;
    private string _trainingCenterAddress;

    /// <summary>
    /// Guid based Id for the managing-coordinator who manages this message.
    /// </summary>
    public Guid CoordinatorId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// References a list of notifications associated with a given coordinator.
    /// </summary>
    public virtual List<Lib.Model.Notification> Notifications { get; set; }

    /// <summary>
    /// Coordinator's full name.
    /// </summary>
    public string Name
    {
      get { return _name; }
      set
      {
        NotNullOrEmpty(value);
        _name = value;
      }
    }

    /// <summary>
    /// Coordinator's valid email
    /// </summary>
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
