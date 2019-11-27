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

    /// <summary>
    /// Test for making sure that creating a room should create a room and that the
    /// properties of the room should have something populating them
    /// </summary>
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
      Assert.NotNull(newRoom);
      Assert.True(newRoom.RoomId == newRoomId);
      Assert.True(newRoom.ComplexId == newComplexId);
      Assert.True(newRoom.Gender == newGender);
      Assert.True(newRoom.RoomNumber == newRoomNumber);
      Assert.True(newRoom.RoomType == newRoomType);
      Assert.True(newRoom.NumberOfBeds == newNumOfBeds);
      Assert.True(newRoom.NumberOfOccupants == newNumOfOccupants);
      Assert.True(newRoom.LeaseStart == newLeaseStart);
      Assert.True(newRoom.LeaseEnd == newLeaseEnd);
    }

    /// <summary>
    /// Test to throw an exception if inserting the lease date is not valid
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidLease()
    {
      Action invalidCreate = () => new BL.Room().SetLease(newLeaseStart, newLeaseStart);
      Assert.Throws<ArgumentException>(invalidCreate);
      
    }

    /// <summary>
    /// Test to throw an exception if inserting a room number that is not accepted
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidRoomNumber()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = ""});
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = null });
    }

    /// <summary>
    /// Test to throws an exception if inserting invalid number of beds
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidNumberOfBeds()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfBeds = 0});
      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfBeds = -1 });
    }

    /// <summary>
    /// Test that throws an exception if number of occupants is not a valid value
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidNumberOfOccupants()
    {

      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfOccupants = -1 });

      var room = new BL.Room() { NumberOfBeds = 2 };
      Assert.Throws<ArgumentException>(() => room.NumberOfOccupants = 3);
    }

    /// <summary>
    /// Test that throws an exception if the room type is not a valid value
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidRoomType()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomType = "" });
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomType = null });
    }
  }
}
