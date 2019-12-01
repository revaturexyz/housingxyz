using System;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Model for the response recevied from room service of available rooms, needed for the buggy json deserialization
  /// </summary>
  /// <remarks>The room service sends a tuple and there were some issues with deserializing a tuple thus creating this model was necessary</remarks>
  public class AvailRoom
  {
    /// <summary>
    /// Room Id
    /// </summary>
    public Guid item1 { get; set; }
    /// <summary>
    /// Number of beds
    /// </summary>
    public int item2 { get; set; }
  }
}
