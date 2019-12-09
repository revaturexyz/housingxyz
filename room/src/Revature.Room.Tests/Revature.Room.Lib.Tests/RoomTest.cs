using System;
using Xunit;
using BL = Revature.Room.Lib;
namespace Revature.Room.Tests.Revature.Room.Lib.Tests
{
  public class RoomTest
  {
    private Guid _newRoomId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private Guid _newComplexId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private readonly string _newGender = "Female";
    private readonly string _newRoomNumber = "2002";
    private readonly string _newRoomType = "Dormitory";
    private readonly int _newNumOfBeds = 4;

    private readonly int _newNumOfOccupants = 2;
    private readonly DateTime _newLeaseStart = new DateTime(2000, 1, 1);
    private readonly DateTime _newLeaseEnd = new DateTime(2001, 12, 31);

    /// <summary>
    /// Test for making sure that creating a room should create a room and that the
    /// properties of the room should have something populating them
    /// </summary>
    [Fact]
    public void RoomShouldCreate()
    {
      var newRoom = new BL.Room()
      {
        RoomId = _newRoomId,
        ComplexId = _newComplexId,
        Gender = _newGender,
        RoomNumber = _newRoomNumber,
        RoomType = _newRoomType,
        NumberOfBeds = _newNumOfBeds,
        NumberOfOccupants = _newNumOfOccupants
      };
      newRoom.SetLease(_newLeaseStart, _newLeaseEnd);
      Assert.NotNull(newRoom);
      Assert.True(newRoom.RoomId == _newRoomId);
      Assert.True(newRoom.ComplexId == _newComplexId);
      Assert.True(newRoom.Gender == _newGender);
      Assert.True(newRoom.RoomNumber == _newRoomNumber);
      Assert.True(newRoom.RoomType == _newRoomType);
      Assert.True(newRoom.NumberOfBeds == _newNumOfBeds);
      Assert.True(newRoom.NumberOfOccupants == _newNumOfOccupants);
      Assert.True(newRoom.LeaseStart == _newLeaseStart);
      Assert.True(newRoom.LeaseEnd == _newLeaseEnd);
    }

    /// <summary>
    /// Test to throw an exception if inserting the lease date is not valid
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidLease()
    {
      void InvalidCreate() => new BL.Room().SetLease(_newLeaseStart, _newLeaseStart);
      Assert.Throws<ArgumentException>(InvalidCreate);

    }

    /// <summary>
    /// Test to throw an exception if inserting a room number that is not accepted
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidRoomNumber()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = "" });
      Assert.Throws<ArgumentException>(() => new BL.Room() { RoomNumber = null });
    }

    /// <summary>
    /// Test to throws an exception if inserting invalid number of beds
    /// </summary>
    [Fact]
    public void RoomShouldRejectInvalidNumberOfBeds()
    {
      Assert.Throws<ArgumentException>(() => new BL.Room() { NumberOfBeds = 0 });
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
