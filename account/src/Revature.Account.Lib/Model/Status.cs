
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

    static readonly string Pending = "Pending";
    static readonly string Accepted = "Accepted";
    static readonly string Rejected = "Rejected";
    static readonly string UnderReview = "Under Review";

    public void changeStatus(int statusId)
    {
      switch (statusId)
      {
        case 1:
          StatusText = Pending;
          StatusId = 1;
          return;
        case 2:
          StatusText = Accepted;
          StatusId = 2;
          return;
        case 3:
          StatusText = Rejected;
          StatusId = 3;
          return;
        case 4:
          StatusText = UnderReview;
          StatusId = 4;
          return;
      }
      throw new ArgumentException("Only values 1-4 accepted for changing status.");
    }
  }
}
