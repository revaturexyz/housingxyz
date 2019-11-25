
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

    public static readonly string Pending = "Pending";
    public static readonly string Approved = "Approved";
    public static readonly string Rejected = "Rejected";
    public static readonly string UnderReview = "Under Review";

    public void ChangeStatus(string newStatus)
    {
      if (newStatus == Pending)
      {
        StatusId = 1;
        StatusText = Pending;
      }
      else if (newStatus == Approved)
      {
        StatusId = 2;
        StatusText = Approved;
      }
      else if (newStatus == Rejected)
      {
        StatusId = 3;
        StatusText = Rejected;
      }
      else if (newStatus == UnderReview)
      {
        StatusId = 4;
        StatusText = UnderReview;
      }
      throw new ArgumentException("Only values 1-4 accepted for changing status.");
    }
  }
}
