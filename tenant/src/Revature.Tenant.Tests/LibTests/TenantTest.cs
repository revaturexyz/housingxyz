using System;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class TenantTest
  {
    /// <summary>
    /// Tests that Empty ID throw exception
    /// </summary>
    [Fact]
    public void Tenant_Id_Test()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { Id = Guid.Empty });
    }

    /// <summary>
    /// Tests that Empty First name throw exception
    /// </summary>
    [Fact]
    public void Tenant_First_Name_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { FirstName = "" });
    }

    /// <summary>
    /// Tests that Empty Last name throw exception
    /// </summary>
    [Fact]
    public void Tenant_Last_Name_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { LastName = "" });
    }

    /// <summary>
    /// Tests that Empty room ID throw exception
    /// </summary>
    [Fact]
    public void Tenant_Room_Id_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { RoomId = Guid.Empty });
    }

    /// <summary>
    /// Tests that Empty email throw exception
    /// </summary>
    [Fact]
    public void Tenant_Email_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { Email = "" });
    }

    /// <summary>
    /// Tests that Empty car ID throw exception
    /// </summary>
    [Fact]
    public void Tenant_Car_Id_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { CarId = -1 });
    }

    /// <summary>
    /// Tests that Empty training center ID throw exception
    /// </summary>
    [Fact]
    public void Tenant_Training_Center_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Tenant { TrainingCenter = Guid.Empty });
    }
  }
}
