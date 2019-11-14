using System;
using System.Collections.Generic;
using System.Text;

namespace Revature.Room.Lib
{
  public class Room
  {
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
