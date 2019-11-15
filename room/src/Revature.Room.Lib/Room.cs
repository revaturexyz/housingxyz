using System;

namespace Revature.Room.Lib
{
  public class Room
  {
    public Guid RoomID { get; set; }
    public string RoomNumber { get; set; }
    public int NumberOfBeds { get; set; }
    public string Gender { get; set; }
    public string RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public Guid ComplexID { get; set; }
  }
}
