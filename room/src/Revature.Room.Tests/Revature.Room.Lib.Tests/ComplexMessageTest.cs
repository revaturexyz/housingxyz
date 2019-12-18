using System;
using Revature.Room.Lib.Models;
using Xunit;

namespace Revature.Room.Tests.Revature.Room.Lib.Tests
{
  public class ComplexMessageTest
  {
    [Fact]
    public void ComplexMessageShouldCreate()
    {
      var newRoomId = Guid.NewGuid();
      var roomNumber = "2428B";
      var complexId = Guid.NewGuid();
      var numberOfBeds = 4;
      var roomType = "Apartment";
      var leaseStart = DateTime.Now;
      var leaseEnd = DateTime.Now.AddDays(3);
      var queOperator = 0;

      var complexMessage = new ComplexMessage()
      {
        RoomId = newRoomId,
        ComplexId = complexId,
        LeaseStart = leaseStart,
        LeaseEnd = leaseEnd,
        NumberOfBeds = numberOfBeds,
        RoomNumber = roomNumber,
        RoomType = roomType,
        QueOperator = queOperator
      };
      Assert.NotNull(complexMessage);
      Assert.True(complexMessage.RoomId == newRoomId);
      Assert.True(complexMessage.ComplexId == complexId);
      Assert.True(complexMessage.LeaseStart == leaseStart);
      Assert.True(complexMessage.LeaseEnd == leaseEnd);
      Assert.True(complexMessage.NumberOfBeds == numberOfBeds);
      Assert.True(complexMessage.RoomNumber == roomNumber);
      Assert.True(complexMessage.RoomType == roomType);
      Assert.True(complexMessage.QueOperator == queOperator);
      Assert.True(complexMessage.RoomType == roomType);
    }
  }
}
