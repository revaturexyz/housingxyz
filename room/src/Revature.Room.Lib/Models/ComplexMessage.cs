using System;

namespace Revature.Room.Lib.Models
{
  /// <summary>
  /// Model for message to be received from complex service
  /// </summary>
  public class ComplexMessage
  {
    public Guid RoomId { get; set; }
    public string RoomNumber { get; set; }
    public Guid ComplexId { get; set; }
    public int NumberOfBeds { get; set; }
    public string RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    /// <summary>
    /// it is an identifier for Room service to recongnize which method should they act(CRUD)
    /// </summary>
    public int QueOperator { get; set; }
  }
}
