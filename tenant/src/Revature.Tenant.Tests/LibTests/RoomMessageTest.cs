using System;
using Revature.Tenant.Lib.Models;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class RoomMessageTest
  {
    private Guid _roomId = Guid.NewGuid();
    private readonly string _gender = "Female";

    [Fact]
    public void RoomMessageShouldCreate()
    {
      var result = new RoomMessage()
      {
        Gender = _gender,
        RoomId = _roomId,
        OperationType = 0
      };

      Assert.NotNull(result);
      Assert.True(result.Gender == _gender);
      Assert.True(result.RoomId == _roomId);
      Assert.True(result.OperationType == 0);
    }

    [Fact]
    public void RoomMessageShouldCheckValidation()
    {
      Assert.Throws<ArgumentException>(() => new RoomMessage() { RoomId = Guid.Empty });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { Gender = null });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { Gender = "" });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { Gender = "\n" });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { Gender = "        " });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { OperationType = -1 });
      Assert.Throws<ArgumentException>(() => new RoomMessage() { OperationType = 2 });
    }
  }
}
