using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.DataAccess.Entities
{
  public class CoordinatorAccount
  {
    public CoordinatorAccount()
    {
      Notification = new HashSet<Notification>();
    }
    public Guid CoordinatorId { get; set; }
    public string TrainingName { get; set; }
    public string TrainingAddress { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Guid ProviderId { get; set; }
    public virtual ICollection<Entities.ProviderAccount> Providers { get; set; }
    public virtual ICollection<Entities.Notification> Notifications { get; set; }
  }
}
