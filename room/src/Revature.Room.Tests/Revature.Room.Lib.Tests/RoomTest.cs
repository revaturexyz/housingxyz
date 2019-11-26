using System;
using Xunit;
using BL = Revature.Room.Lib;
namespace Revature.Room.Tests.Revature.Room.Lib.Tests
{
  public class RoomTest
  {
    private Guid newRoomId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private Guid newComplexId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Female";
    private string newRoomNumber = "2002";
    private string newRoomType = "Dormitory";
    private int newNumOfBeds = 4;

    private int newNumOfOccupants = 2;
    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);
    [Fact]
    public void RoomShouldCreate()
    {
        var newRoom = new BL.Room()
        {
          RoomId = newRoomId,
          ComplexId = newComplexId,
          Gender = newGender,
          RoomNumber = newRoomNumber,
          RoomType = newRoomType,
          NumberOfBeds = newNumOfBeds,
          NumberOfOccupants = newNumOfOccupants
        };
      newRoom.SetLease(newLeaseStart, newLeaseEnd);
      Assert.True(true);
    }
    [Fact]
    public void RoomShouldRejectInvalidLease()
    {
      var newRoom = new BL.Room()
      {
        RoomId = newRoomId,
        ComplexId = newComplexId,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants
      };
      Action invalidCreate = () => newRoom.SetLease(newLeaseStart, newLeaseStart);
      Assert.Throws<ArgumentException>(invalidCreate);
      
    }
    [Fact]
    public void RoomShouldRejectInvalidRoomNumber()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = ""});
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = null });
    }
    [Fact]
    public void RoomShouldRejectInvalidNumberOfBeds()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfBeds = 0});
      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfBeds = -1 });
    }
    [Fact]
    public void RoomShouldRejectInvalidNumberOfOccupants()
    {

      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfOccupants = -1 });

      var room = new BL.Room() { NumberOfBeds = 2 };
      Assert.Throws<ArgumentException>(() => room.NumberOfOccupants = 3);
    }

    [Fact]
    public void RoomShouldRejectInvalidRoomType()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomType = "" });
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomType = null });
    }
  }
}
