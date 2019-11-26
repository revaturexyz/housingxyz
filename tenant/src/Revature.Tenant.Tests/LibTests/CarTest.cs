using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class CarTest
  {

    /// <summary>
    /// Tests that Empty LP throw exception
    /// </summary>
    [Fact]
    public void Car_License_Plate_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { LicensePlate = "" });
    }

    /// <summary>
    /// Tests that Empty make throw exception
    /// </summary>
    [Fact]
    public void Car_Make_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Make = "" });
    }

    /// <summary>
    /// Tests that Empty Model throw exception
    /// </summary>
    [Fact]
    public void Car_Model_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Model = "" });
    }

    /// <summary>
    /// Tests that Empty year throw exception
    /// </summary>
    [Fact]
    public void Car_Year_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Year = "" });
    }

    /// <summary>
    /// Tests that color throw exception
    /// </summary>
    [Fact]
    public void Car_Color_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { Color = "" });
    }

    /// <summary>
    /// Tests that Empty car state throw exception
    /// </summary>
    [Fact]
    public void Car_State_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Car { State = "" });
    }

  }
}
