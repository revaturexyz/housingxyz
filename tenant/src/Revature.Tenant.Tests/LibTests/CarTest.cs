using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class CarTest
  {
    [Fact]
    public void Car_Id_Test()
    {
      Assert.ThrowsAny<ArgumentOutOfRangeException>(() => new Lib.Models.Car { Id = -1 });
    }

    [Fact]
    public void Car_License_Plate_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { LicensePlate = "" });
    }

    [Fact]
    public void Car_Make_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Make = "" });
    }

    [Fact]
    public void Car_Model_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Model = "" });
    }

    [Fact]
    public void Car_Year_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Year = "" });
    }

    [Fact]
    public void Car_Color_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Color = "" });
    }

    [Fact]
    public void Car_State_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { State = "" });
    }

  }
}
