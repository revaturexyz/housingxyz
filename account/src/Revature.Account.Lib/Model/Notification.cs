using System;

namespace Revature.Account.Lib.Model
{
  public class Notification
  {
    public Guid NotificationId { get; set; } = Guid.NewGuid();
    public Guid ProviderId { get; set; }
    public Guid CoordinatorId { get; set; }
    public String Status { get; set; }
    /// <summary>
    /// Date and time the associated provider account expires at, if any, in the format 11:59:59.
    /// </summary>
    public DateTime AccountExpiresAt { get; set; }
  }
}
