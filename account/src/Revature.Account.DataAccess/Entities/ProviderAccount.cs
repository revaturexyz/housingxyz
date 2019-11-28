using System;
using System.Collections.Generic;

namespace Revature.Account.DataAccess.Entities
{
  public class ProviderAccount
  {
    public Guid ProviderId { get; set; }
    public Guid CoordinatorId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string StatusText { get; set; }
    public DateTime AccountCreatedAt { get; set; }
    public DateTime AccountExpiresAt { get; set; }
    public CoordinatorAccount Coordinator { get; set; }
    public List<Notification> Notifications { get; set; }
  }
}
