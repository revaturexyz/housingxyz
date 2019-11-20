using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Revature.Tenant.Lib.Interface;
using LibMod = Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Tests.ApiTests
{
  internal static class ApiTestData
  {

    internal static List<LibMod.Tenant> Tenant = new List<LibMod.Tenant>
    {

      new LibMod.Tenant
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Email = "colton@hotmail.com",
        Gender = "Male",
        FirstName = "Colton",
        LastName = "Clary",
        AddressId = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5"),
        RoomId = Guid.Parse("7b774b35-ce79-4fa9-b860-93b4fa1ddc6a"),
        CarId = 1,
        BatchId = 2,
        Car = new Lib.Models.Car
        {
          Id = 1,
          LicensePlate = "LicensePlate",
          Make = "Make",
          Model = "Model",
          Color = "Color",
          Year = "Year",
          State = "TX"
        },
      },

      new LibMod.Tenant
      {
        Id = Guid.Parse("68be3efb-9f25-46f5-bfdd-3c82363cd362"),
        Email = "Greg@hotmail.com",
        Gender = "Male",
        FirstName = "Greg",
        LastName = "V",
        AddressId = Guid.Parse("e2c6939b-0f22-4746-b2f4-036a1c74015e"),
        RoomId = Guid.Parse("7ec98f58-e416-4682-a3c8-457eafedc938"),
        CarId = 2,
        BatchId = 2,
        Car = new Lib.Models.Car
        {
          Id = 1,
          LicensePlate = "LicensePlate",
          Make = "Make",
          Model = "Model",
          Color = "Color",
          Year = "Year",
          State = "TX"
        },
      },
    };

    internal static Mock<ITenantRepository> MockTenantRepo(List<LibMod.Tenant> testTenant)
    {
      var mockRepo = new Mock<ITenantRepository>();
      mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid i) =>
            {
              var tenant = testTenant.FirstOrDefault(t => t.Id == i);
              if (tenant == null)
              {
                throw new ArgumentException();
              }
              return tenant;
            });
      return mockRepo;
    }
  }
}
