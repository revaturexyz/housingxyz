using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Room.DataAccess.Entities
{
  /// <summary>
  /// Entity for room table, most attributes are assigned from the complex service except the number of occupants
  /// </summary>
  public class Room
  {
    public Guid RoomId { get; set; }

    public string RoomNumber { get; set; }

    [Range(1, int.MaxValue)]
    public int NumberOfBeds { get; set; }
    /// <summary>
    /// Updated by tenant service
    /// </summary>
    public int NumberOfOccupants { get; set; }
    public Gender Gender { get; set; }

    public RoomType RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public Guid ComplexId { get; set; }
  }
}
