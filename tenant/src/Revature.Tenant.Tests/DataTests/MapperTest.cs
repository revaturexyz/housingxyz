using Revature.Tenant.DataAccess;
using System;
using Xunit;

namespace Revature.Tenant.Tests.DataTests
{
    public class MapperTest
    {
        private readonly Mapper mapper = new Mapper();

        [Fact] 
        public void LibTenantToDbTenantTest()
        {
            Lib.Models.Tenant tenant = new Lib.Models.Tenant
            {
              Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
              Email = "joemo@web.com",
              Gender = "m",
              FirstName = "Joe",
              LastName = "Mohrbacher",
              AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
              RoomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
              CarId = 1,
              BatchId = 1,
              TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
              Car = new Lib.Models.Car
              {
                Id = 0,
                LicensePlate = "LicensePlate",
                Make = "Make",
                Model = "Model",
                Color = "Color",
                Year = "Year",
                State = "TX"
              },
              Batch = new Lib.Models.Batch
              {
                Id = 0,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                BatchLanguage = "C#",
                TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
              }
            };

            DataAccess.Entities.Tenant tenants = mapper.MapTenant(tenant);
            Assert.Equal(tenant.Id, tenants.Id);
            Assert.Equal(tenant.Email, tenants.Email);
            Assert.Equal(tenant.FirstName, tenants.FirstName);
            Assert.Equal(tenant.LastName, tenants.LastName);
            Assert.Equal(tenant.AddressId, tenants.AddressId);
            Assert.Equal(tenant.RoomId, tenants.RoomId);
            Assert.Equal(tenant.CarId, tenants.CarId);
            Assert.Equal(tenant.BatchId, tenants.BatchId);
            Assert.Equal(tenant.TrainingCenter, tenants.TrainingCenter);
        }

        [Fact]
        public void DbTenantToLibTenantTest()
        {
            DataAccess.Entities.Tenant tenants = new DataAccess.Entities.Tenant
            {
                Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
                Email = "joemo@web.com",
                Gender = "m",
                FirstName = "Joe",
                LastName = "Mohrbacher",
                AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
                RoomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
                CarId = 1,
                BatchId = 1,
                TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
                Car = new DataAccess.Entities.Car
                {
                  Id = 0,
                  LicensePlate = "LicensePlate",
                  Make = "Make",
                  Model = "Model",
                  Color = "Color",
                  Year = "Year",
                  State = "TX"
                },
                Batch = new DataAccess.Entities.Batch
                {
                  Id = 0,
                  StartDate = DateTime.Now,
                  EndDate = DateTime.Now,
                  BatchLanguage = "C#",
                  TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
                }
            };

            Lib.Models.Tenant tenant = mapper.MapTenant(tenants);
            Assert.Equal(tenants.Id, tenant.Id);
            Assert.Equal(tenants.Email, tenant.Email);
            Assert.Equal(tenants.FirstName, tenant.FirstName);
            Assert.Equal(tenants.LastName, tenant.LastName);
            Assert.Equal(tenants.AddressId, tenant.AddressId);
            Assert.Equal(tenants.RoomId, tenant.RoomId);
            Assert.Equal(tenants.CarId, tenant.CarId);
            Assert.Equal(tenants.BatchId, tenant.BatchId);
            Assert.Equal(tenants.TrainingCenter, tenant.TrainingCenter);
    }

        [Fact]
        public void LibCarToDbCarTest()
        {
            Lib.Models.Car car = new Lib.Models.Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corrolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123",
                State = "TX"
               
            };

            DataAccess.Entities.Car cars = mapper.MapCar(car);
            Assert.Equal(car.Id, cars.Id);
            Assert.Equal(car.LicensePlate, cars.LicensePlate);
            Assert.Equal(car.Make, cars.Make);
            Assert.Equal(car.Model, cars.Model);
            Assert.Equal(car.Year, cars.Year);
            Assert.Equal(car.Color, cars.Color);
            Assert.Equal(car.State, cars.State);
        }

        [Fact]
        public void DbCarToLibCarTest()
        {
            DataAccess.Entities.Car cars = new DataAccess.Entities.Car
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corrolla",
                Year = "1992",
                Color = "Green",
                LicensePlate = "ABC123",
                State = "TX"
            };

            Lib.Models.Car car = mapper.MapCar(cars);
            Assert.Equal(cars.Id, car.Id);
            Assert.Equal(cars.LicensePlate, car.LicensePlate);
            Assert.Equal(cars.Make, car.Make);
            Assert.Equal(cars.Model, car.Model);
            Assert.Equal(cars.Year, car.Year);
            Assert.Equal(cars.Color, car.Color);
            Assert.Equal(cars.State, car.State);
        }
    }
}
