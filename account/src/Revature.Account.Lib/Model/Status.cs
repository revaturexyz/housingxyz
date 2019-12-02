
using System;
namespace Revature.Account.Lib.Model
{
  /// <summary>
  /// Represents the current status of a notification. Currently the ids:
  /// 1. Pending
  /// 2. Accepted
  /// 3. Rejected
  /// 4. Under Review
  ///
  /// These values are seeded in the context.
  /// </summary>
  public class Status
  {
    public static readonly string Unassigned = "Unassigned";
    public static readonly string Pending = "Pending";
    public static readonly string Approved = "Approved";
    public static readonly string Rejected = "Rejected";
    public static readonly string UnderReview = "Under Review";

    public Status() { }

    public Status(string newStatus)
    {
      StatusText = newStatus;
    }

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
          _statusText = value;
        }
        else if (value == Approved)
        {
          _statusText = value;
        }
        else if (value == Rejected)
        {
          _statusText = value;
        }
        else if (value == UnderReview)
        {
          _statusText = value;
        }
        else
          throw new ArgumentException("Only Pending, Accepted, Rejected, and Under Review.");
      }
    }
  }
}
