using System;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class RoomInfo
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
  }
}
