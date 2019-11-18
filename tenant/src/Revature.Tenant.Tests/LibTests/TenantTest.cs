using System;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class TenantTest
  {
    [Fact]
    public void Tenant_Id_Test()
    {
      Guid newG = new Guid();
      Lib.Models.Tenant tenant = new Lib.Models.Tenant { Id = newG };
      Assert.Equal(newG, tenant.AddressId);
    }

    [Fact]
    public void Tenant_First_Name_Empty()
    {
      Assert.Throws<ArgumentException>(() => new Lib.Models.Tenant { FirstName = "" });
    }

    [Fact]
    public void Tenant_Last_Name_Empty()
    {
      Assert.Throws<ArgumentException>(() => new Lib.Models.Tenant { LastName = "" });
    }

    [Fact]
    public void Tenant_Room_Id_Empty()
    {
      Guid newG = new Guid();
      Lib.Models.Tenant tenant = new Lib.Models.Tenant { RoomId = newG };
      Assert.Equal(newG, tenant.AddressId);
    }

    [Fact]
    public void Tenant_Email_Empty()
    {
      Assert.Throws<ArgumentException>(() => new Lib.Models.Tenant { Email = "" });
    }

    [Fact]
    public void Tenant_Car_Id_Empty()
    {
      Assert.Throws<ArgumentException>(() => new Lib.Models.Tenant { CarId = -1 });
    }

    [Fact]
    public void Tenant_Address_Id_Empty()
    {
      Guid newG = new Guid();
      Lib.Models.Tenant tenant = new Lib.Models.Tenant { AddressId = newG };
      Assert.Equal(newG, tenant.AddressId);
    }
  }
}
