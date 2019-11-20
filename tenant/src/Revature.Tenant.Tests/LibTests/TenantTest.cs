using System;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class TenantTest
  {
    [Fact]
    public void Tenant_Id_Test()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { Id = Guid.Empty });
    }

    [Fact]
    public void Tenant_First_Name_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { FirstName = "" });
    }

    [Fact]
    public void Tenant_Last_Name_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { LastName = "" });
    }

    [Fact]
    public void Tenant_Room_Id_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { RoomId = Guid.Empty });
    }

    [Fact]
    public void Tenant_Email_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { Email = "" });
    }

    [Fact]
    public void Tenant_Car_Id_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { CarId = -1 });
    }

    [Fact]
    public void Tenant_Training_Center_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { TrainingCenter = Guid.Empty });
    }
  }
}
