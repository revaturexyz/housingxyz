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
                Id = 1,
                Email = "joemo@web.com",
                Gender = "m",
                FirstName = "Joe",
                LastName = "Mohrbacher",
                AddressId = new Guid(),
                RoomId = 1,
                CarId = 1,
              Car = new Lib.Models.Car
              {
                Id = 0,
                LicensePlate = "LicensePlate",
                Make = "Make",
                Model = "Model",
                Color = "Color",
                Year = "Year",
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
                Id = 1,
                Email = "joemo@web.com",
                Gender = "m",
                FirstName = "Joe",
                LastName = "Mohrbacher",
                AddressId = new Guid(),
                RoomId = 1,
                CarId = 1,
              Cars = new DataAccess.Entities.Cars
              {
                Id = 0,
                LicensePlate = "LicensePlate",
                Make = "Make",
                Model = "Model",
                Color = "Color",
                Year = "Year",
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
        public void CarsToCarTest()
        {
            Lib.Models.Car car = new Lib.Models.Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corrolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123"
            };

            DataAccess.Entities.Cars cars = mapper.MapCar(car);
            Assert.Equal(car.Id, cars.Id);
            Assert.Equal(car.LicensePlate, cars.LicensePlate);
            Assert.Equal(car.Make, cars.Make);
            Assert.Equal(car.Model, cars.Model);
            Assert.Equal(car.Year, cars.Year);
            Assert.Equal(car.Color, cars.Color);
        }

        [Fact]
        public void CarToCarsTest()
        {
            DataAccess.Entities.Cars cars = new DataAccess.Entities.Cars
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corrolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123"
            };

            Lib.Models.Car car = mapper.MapCar(cars);
            Assert.Equal(cars.Id, car.Id);
            Assert.Equal(cars.LicensePlate, car.LicensePlate);
            Assert.Equal(cars.Make, car.Make);
            Assert.Equal(cars.Model, car.Model);
            Assert.Equal(cars.Year, car.Year);
            Assert.Equal(cars.Color, car.Color);
        }
    }
}