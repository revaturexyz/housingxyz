using System;
using System.Collections.Generic;

namespace Revature.Account.DataAccess.Entities
{
  public class CoordinatorAccount
  {
    public CoordinatorAccount()
    {
      Notifications = new HashSet<Notification>();
    }

    public Guid CoordinatorId { get; set; }
    public string TrainingCenterName { get; set; }
    public string TrainingCenterAddress { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid ProviderId { get; set; }
    public virtual ICollection<Entities.ProviderAccount> Providers { get; set; }
    public virtual ICollection<Entities.Notification> Notifications { get; set; }
  }
}
