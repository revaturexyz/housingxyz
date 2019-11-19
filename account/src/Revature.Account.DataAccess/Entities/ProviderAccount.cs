using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.DataAccess.Entities
{
  public class ProviderAccount
  {
    //set the primary key with guid
    public Guid ProviderId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime AccountCreated { get; set; }
    public string Status { get; set; }
    public DateTime Expire { get; set; }
    public Guid CoordinatorId { get; set; }
    public CoordinatorAccount Coordinator { get; set; }
  }
}
