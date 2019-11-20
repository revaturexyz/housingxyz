using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Represents the current status of a notification. Currently the ids:
  /// 1 = Pending
  /// 2 = Accepted
  /// 3 = Rejected
  /// 4 = Under Review
  ///
  /// These values are seeded in the context.
  /// </summary>
  public class Status
  {
    public int StatusId { get; set; }
    public string StatusText { get; set; }
  }
}
