using System;

namespace Revature.Room.Lib
{
  public class Room
  {
    private int _numberOfBeds;
    public Guid RoomID { get; set; }
    public string RoomNumber { get; set; }
    public int NumberOfBeds
    {
      get => _numberOfBeds;
      set
      {
        if (value > 0) _numberOfBeds = value;
      }
    }
    public int NumberOfOccupants { get; set; }
    public string Gender { get; set; }
    public string RoomType { get; set; }
    public DateTime LeaseStart { get; set; }
    public DateTime LeaseEnd { get; set; }

    public Guid ComplexID { get; set; }
  }
}
