
using System;
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

    private const string pendingNotification = "Pending";
    private const string acceptedNotification = "Accepted";
    private const string rejectedNotification = "Rejected";
    private const string underReviewNotification = "Under Review";

    public void changeStatus(int statusId)
    {
      switch (statusId)
      {
        case 1:
          StatusText = pendingNotification;
          StatusId = 1;
          return;
        case 2:
          StatusText = acceptedNotification;
          StatusId = 2;
          return;
        case 3:
          StatusText = rejectedNotification;
          StatusId = 3;
          return;
        case 4:
          StatusText = underReviewNotification;
          StatusId = 4;
          return;
      }
      throw new ArgumentException("Only values 1-4 accepted for changing status.");
    }
  }
}
