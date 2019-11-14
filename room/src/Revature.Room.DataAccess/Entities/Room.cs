using Revature.Room.Lib;
using System;
using System.ComponentModel.DataAnnotations;

namespace Revature.Room.DataAccess.Entities
{
  public class Room
  {
    [Key]
    public int RoomID { get; set; }

    public string RoomNumber { get; set; }
    public int NumberOfBeds { get; set; }
    public Gender Gender { get; set; }
    public RoomType RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public int ComplexID { get; set; }
  }
}
