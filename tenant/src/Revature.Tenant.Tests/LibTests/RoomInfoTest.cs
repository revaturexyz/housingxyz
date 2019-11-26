using System;
using System.Collections.Generic;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class RoomInfoTest
  {
    [Fact]
    public void Number_Of_Beds_Negative()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.RoomInfo { NumberOfBeds = -1 });
    }

    [Fact]
    public void Number_Of_Beds_Zero()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.RoomInfo { NumberOfBeds = 0 });
    }

    [Fact]
    public void Room_Id_Test()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.RoomInfo { RoomId = Guid.Empty });
    }

    [Fact]
    public void RoomInfoShouldCreate()
    {
      var roomId = Guid.NewGuid();
      var createdRoom = new Lib.Models.RoomInfo()
      {
        NumberOfBeds = 4,
        RoomId = roomId,
        Tenants = new List<Lib.Models.Tenant>()
      };
      Assert.NotNull(createdRoom);
      Assert.True(createdRoom.RoomId == roomId);
      Assert.True(createdRoom.NumberOfBeds == 4);
    }
  }
}
