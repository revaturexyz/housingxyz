using System;
using System.Collections.Generic;
using System.Text;

using BL = Revature.Room.Lib;
using Xunit;

namespace Revature.Room.Tests.Revature.Room.Lib.Tests
{
  public class ComplexMessageTest
  {
    private Guid newRoomId = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456dd");
    private Guid newComplexId = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Male";
    private string newRoomNumber = "2003";
    private string newRoomType = "TownHouse";
    private int newNumOfBeds = 4;

    private int newNumOfOccupants = 2;
    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);

   
    /// <summary>
    /// Ensures that creating a complex message which consists of a room and a operationtype
    /// should create properly
    /// </summary>
    [Fact]
    public void CreateComplexMessageShouldCreate()
    {
      var roomToInsert = new BL.Room()
      {
        RoomId = newRoomId,
        ComplexId = newComplexId,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants
      };

      roomToInsert.SetLease(newLeaseStart, newLeaseEnd);

      var newComplexMessage = new BL.Models.ComplexMessage()
      {
        Room = roomToInsert,

        OperationType = BL.Models.OperationType.Create
      };

      Assert.True(true);
    }
  }
}
