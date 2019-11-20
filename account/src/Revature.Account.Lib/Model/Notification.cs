using System;

namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Encapsulates a notification a coordinator might receive regarding
  /// an action made by a provider.
  /// </summary>
  public class Notification
  {
    public Guid NotificationId { get; set; } = Guid.NewGuid();
    public Guid ProviderId { get; set; }
    public Guid CoordinatorId { get; set; }
    /// <summary>
    /// The new status that the provider in question is now under.
    /// </summary>
    public Status Status { get; set; }
    /// <summary>
    /// Date and time the associated provider account expires at, if any, in the format 11:59:59.
    /// </summary>
    public DateTime AccountExpiresAt { get; set; }
  }
}
