using System;

namespace Revature.Account.DataAccess.Entities
{
  public class Notification
  {
    public Guid NotificationId { get; set; }
    public Guid ProviderId { get; set; }
    public Guid CoordinatorId { get; set; }
    public Guid UpdateActionId { get; set; }
    public DateTime AccountExpiresAt { get; set; }
    public CoordinatorAccount Coordinator { get; set; }
    public ProviderAccount Provider { get; set; }
    public UpdateAction UpdateAction { get; set; }
  }
}
