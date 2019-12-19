using System;

namespace Revature.Account.Lib.Model
{
  public class UpdateAction
  {
    public Guid UpdateActionId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// String title of intended action, i.e. "UpdateOccupiedRoom".
    /// </summary>
    public string UpdateType { get; set; }

    /// <summary>
    /// Foreign key of the notification this action belongs to.
    /// </summary>
    public Guid NotificationId { get; set; }

    /// <summary>
    /// The serialized object to be used by the action.
    /// </summary>
    public string SerializedTarget { get; set; }

  }
}
