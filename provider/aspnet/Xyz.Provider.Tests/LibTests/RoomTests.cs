using System;
using System.Collections.Generic;
using Xunit;

using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class RoomTests
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange (create DateTime variables)
      var start = DateTime.Now - new TimeSpan(4, 3, 2, 1);
      var end = DateTime.Now;

      // Act (create room object)
      var room = new Room
      {
        Address = new Address(),
        Amenities = new List<Amenity>(),
        Complex = new Complex(),
        Gender = new Gender(),
        RoomType = new RoomType(),
        RoomId = 1,
        RoomNumber = "5",
        NumberOfBeds = 8,
        NumberOfOccupants = 7,
        HasAmenity = true,
        StartDate = start,
        EndDate = end
      };

      // Assert (ensure that room variables are set properly)
      Assert.Equal(1, room.RoomId);
      Assert.Equal("5", room.RoomNumber);
      Assert.Equal(7, room.NumberOfOccupants);
      Assert.Equal(8, room.NumberOfBeds);
      Assert.True(room.HasAmenity);
      Assert.Equal(start, room.StartDate);
      Assert.Equal(end, room.EndDate);
      Assert.NotNull(room.Address);
      Assert.NotNull(room.Amenities);
      Assert.NotNull(room.Complex);
      Assert.NotNull(room.Gender);
      Assert.NotNull(room.RoomType);
      Assert.Empty(room.Amenities);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("!@#$%^&*()")]
    public void RoomNumberShouldBeAStringOfNumbers(string num)
    {
      // Arrange
      var room = new Room(); // default constructor
      void RoomNumberTooSmall() => room.RoomNumber = num; // try to set RoomNumber to a bad value
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(RoomNumberTooSmall); // checks if Argument and ArgumentNull Exceptions were thrown
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-65536)]
    public void NumberOfBedsShouldBePositive(int num)
    {
      // Arrange
      var room = new Room(); // default constructor
      void TooFewBeds() => room.NumberOfBeds = num; // try to set the Number of Beds to a bad value
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(TooFewBeds); // checks if ArgumentException is thrown in Action
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-65536)]
    public void NumberOfOccupantsShouldNotBeNegative(int num)
    {
      // Arrange
      var room = new Room(); // default constructor
      void TooFewOccupants() => room.NumberOfOccupants = num; // try to set the number of occupants to a bad value
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(TooFewOccupants); // checks if ArgumentException is thrown in Action
    }

    [Fact]
    public void NumberOfOccupantsShouldNotExceedNumberOfBeds()
    {
      // Arrange
      var room = new Room { NumberOfBeds = 2 }; // constructor to set up the number of beds
      void TooManyOccupants() => room.NumberOfOccupants = 65536; // try to set the number of occupants to one more than the max of an unsigned short
      // Act, Assert
      Assert.ThrowsAny<ArgumentOutOfRangeException>(TooManyOccupants); // checks if ArgumentOutOfRangeException is thrown in Action
    }

    [Fact]
    public void EndDateShouldBeNullable()
    {
      // Arrange & Act 
      var room = new Room
      {
        EndDate = null // set EndDate to null (indefinite period of time we keep the room)
      };

      // Assert (ensure the EndDate is null)
      Assert.Null(room.EndDate);
    }

    [Fact]
    public void EndDateShouldFollowStartDate()
    {
      // Arrange
      var span = new TimeSpan(4, 3, 2, 1); // set up a span for dates
      var room = new Room { StartDate = DateTime.Now, EndDate = DateTime.Now + span }; // set dates in constructor
      void EndTooEarly() => room.EndDate = room.StartDate - span; // try to set EndDate before StartDate
      void StartTooLate() => room.StartDate = room.EndDate.Value + span; // try to set StartDate after EndDate
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(EndTooEarly); // checks if ArgumentException is thrown in Action
      Assert.ThrowsAny<ArgumentException>(StartTooLate); // ditto
    }

    [Fact]
    public void OccupiedRoomShouldNotChangeGender()
    {
      // Arrange
      var room = new Room { NumberOfBeds = 2, NumberOfOccupants = 0, Gender = new Gender() { GenderId = 1, GenderType = "Male" } };
      // ^ prepare a room to be occupied with the room constructor
      void ChangeGender() => room.Gender = new Gender() { GenderId = 2, GenderType = "Female" }; // try to change the gender
      room.NumberOfOccupants = 1; // occupy the room

      // Act, Assert
      Assert.ThrowsAny<InvalidOperationException>(ChangeGender); // checks if InvalidOperationException is thrown in Action
    }
  }
}
