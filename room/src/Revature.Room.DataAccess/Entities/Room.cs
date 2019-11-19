using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Room.DataAccess.Entities
{
  public class Room
  {
    public Guid RoomId { get; set; }

    public string RoomNumber { get; set; }

    [Range(1, int.MaxValue)]
    public int NumberOfBeds { get; set; }

    public int NumberOfOccupants { get; set; }
    public Gender Gender { get; set; }

    public RoomType RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public Guid ComplexId { get; set; }
  }
}
