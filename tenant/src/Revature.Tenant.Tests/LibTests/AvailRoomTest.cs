using System;
using Revature.Tenant.Lib.Models;
using Xunit;
namespace Revature.Tenant.Tests.LibTests
{
  public class AvailRoomTest
  {
    [Fact]
    public void AvailRoomShouldCreate()
    {
      var roomId = Guid.NewGuid();
      var result = new AvailRoom()
      {
        item1 = roomId,
        item2 = 3
      };
      Assert.NotNull(result);
      Assert.True(result.item1 == roomId);
      Assert.True(result.item2 == 3);
    }
  }
}
