
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
    public static readonly string Pending = "Pending";
    public static readonly string Approved = "Approved";
    public static readonly string Rejected = "Rejected";
    public static readonly string UnderReview = "Under Review";

    public int StatusId { get; private set; }
    private string _statusText;
    public string StatusText
    {
      get
      {
        return _statusText;
      }

      set
      {
        if (value == Pending)
        {
          StatusId = 1;
          _statusText = value;
        }
        else if (value == Approved)
        {
          StatusId = 2;
          _statusText = value;
        }
        else if (value == Rejected)
        {
          StatusId = 3;
          _statusText = value;
        }
        else if (value == UnderReview)
        {
          StatusId = 4;
          _statusText = value;
        }
        else
          throw new ArgumentException("Only Pending, Accepted, Rejected, and Under Review.");
      }
    }
  }
}
