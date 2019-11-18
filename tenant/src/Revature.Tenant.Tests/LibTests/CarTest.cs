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
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { Id = -1 });
        }

        [Fact]
        public void Car_License_Plate_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { LicensePlate = "" });
        }

        [Fact]
        public void Car_Make_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { Make = "" });
        }

        [Fact]
        public void Car_Model_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { Model = "" });
        }

        [Fact]
        public void Car_Year_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { Year = "" });
        }

        [Fact]
        public void Car_Color_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Lib.Models.Car { Color = "" });
        }

    }
}
