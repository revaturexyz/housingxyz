using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.DataAccess.Entities
{
  public class UpdateAction
  {
    public Guid UpdateActionId { get; set; }
    public Guid NotificationId { get; set; }

    public string UpdateType { get; set; }

    public string SerializedTarget { get; set; }

    public Notification Notification { get; set; }

  }
}
