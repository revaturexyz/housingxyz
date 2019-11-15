using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Revature.Room.DataAccess.Entities
{
  public class Room
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RoomID { get; set; }

    public string RoomNumber { get; set; }
    public int NumberOfBeds { get; set; }

    public Gender Gender { get; set; }

    public RoomType RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public Guid ComplexID { get; set; }
  }
}
