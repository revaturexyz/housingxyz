using Revature.Tenant.DataAccess;
using System;
using Xunit;

namespace Revature.Tenant.Tests.DataTests
{
    public class MapperTest
    {
        private readonly Mapper mapper = new Mapper();

        [Fact] 
        public void TenantsToTenantTest()
        {
            Lib.Models.Tenant tenant = new Lib.Models.Tenant
            {
                Id = Guid.NewGuid(),
                Email = "joemo@web.com",
                Gender = "m",
                FirstName = "Joe",
                LastName = "Mohrbacher",
                AddressId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                CarId = 1,
              Car = new Lib.Models.Car
              {
                LicensePlate = "LicensePlate",
                Make = "Make",
                Model = "Model",
                Color = "Color",
                Year = "Year",
                State = "TX"
              },
            };

            DataAccess.Entities.Tenants tenants = mapper.MapTenant(tenant);
            Assert.Equal(tenant.Id, tenants.Id);
            Assert.Equal(tenant.Email, tenants.Email);
            Assert.Equal(tenant.FirstName, tenants.FirstName);
            Assert.Equal(tenant.LastName, tenants.LastName);
            Assert.Equal(tenant.AddressId, tenants.AddressId);
            Assert.Equal(tenant.RoomId, tenants.RoomId);
            Assert.Equal(tenant.CarId, tenants.CarId);
        }

        [Fact]
        public void TenantToTenantsTest()
        {
            DataAccess.Entities.Tenants tenants = new DataAccess.Entities.Tenants
            {
                Id = Guid.NewGuid(),
                Email = "joemo@web.com",
                Gender = "m",
                FirstName = "Joe",
                LastName = "Mohrbacher",
                AddressId = Guid.NewGuid(),
                RoomId = Guid.NewGuid(),
                CarId = 1,
              Car = new DataAccess.Entities.Car
              {
                LicensePlate = "LicensePlate",
                Make = "Make",
                Model = "Model",
                Color = "Color",
                Year = "Year",
                State = "TX"
              },
            };

            Lib.Models.Tenant tenant = mapper.MapTenant(tenants);
            Assert.Equal(tenants.Id, tenant.Id);
            Assert.Equal(tenants.Email, tenant.Email);
            Assert.Equal(tenants.FirstName, tenant.FirstName);
            Assert.Equal(tenants.LastName, tenant.LastName);
            Assert.Equal(tenants.AddressId, tenant.AddressId);
            Assert.Equal(tenants.RoomId, tenant.RoomId);
            Assert.Equal(tenants.CarId, tenant.CarId);
        }

        [Fact]
        public void CarToDataCarTest()
        {
            Lib.Models.Car car = new Lib.Models.Car
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123",
                State = "TX"
            };

            DataAccess.Entities.Car dataCar = mapper.MapCar(car);
            Assert.Equal(car.Id, dataCar.Id);
            Assert.Equal(car.LicensePlate, dataCar.LicensePlate);
            Assert.Equal(car.Make, dataCar.Make);
            Assert.Equal(car.Model, dataCar.Model);
            Assert.Equal(car.Year, dataCar.Year);
            Assert.Equal(car.Color, dataCar.Color);
            Assert.Equal(car.State, dataCar.State);
        }

        [Fact]
        public void dataCarToCarTest()
        {
            DataAccess.Entities.Car dataCar = new DataAccess.Entities.Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corrolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123",
                State = "TX"
            };

            Lib.Models.Car car = mapper.MapCar(dataCar);
            Assert.Equal(dataCar.Id, car.Id);
            Assert.Equal(dataCar.LicensePlate, car.LicensePlate);
            Assert.Equal(dataCar.Make, car.Make);
            Assert.Equal(dataCar.Model, car.Model);
            Assert.Equal(dataCar.Year, car.Year);
            Assert.Equal(dataCar.Color, car.Color);
            Assert.Equal(dataCar.State, car.State);
    }
    }
}
