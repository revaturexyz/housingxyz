using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Revature.Tenant.Lib.Interface;
using LibMod = Revature.Tenant.Lib.Models;

namespace Revature.Tenant.Tests.ApiTests
{
  //Static class with data for ApiTests
  internal static class ApiTestData
  {
    internal static List<LibMod.Tenant> Tenants = new List<LibMod.Tenant>
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
        Batch = new LibMod.Batch
        {
            Id = 2,
            BatchCurriculum = "C#",
            TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
        },
        Car = new LibMod.Car
        {
            Id = 1,
            Color = "yellow",
            Make = "mitsubishi",
            Model = "montero sport",
            LicensePlate = "1231234",
            State = "Texas",
            Year = "1999"
        },
        TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")

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
        Batch = new LibMod.Batch
        {
            Id = 2,
            BatchCurriculum = "C#",
            TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
        },
        Car = new LibMod.Car
        {
            Id = 2,
            Color = "yellow",
            Make = "mitsubishi",
            Model = "montero sport",
            LicensePlate = "1231234",
            State = "Texas",
            Year = "1999"
        },
        TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
      },

    };

    internal static List<LibMod.Batch> Batches = new List<LibMod.Batch>
    {
      new Lib.Models.Batch
      {
        Id = 1,
        BatchCurriculum = "C#",
        TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
      },
      new Lib.Models.Batch
      {
        Id = 2,
        BatchCurriculum = "C#",
        TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
      },
      new Lib.Models.Batch
      {
        Id = 3,
        BatchCurriculum = "C#",
        TrainingCenter = Guid.Parse("d9e4077e-0e5e-4201-8801-a2ea40b8c0b5")
      }
    };

    internal static Mock<ITenantRepository> MockBatchRepo(List<LibMod.Batch> testBatches)
    {
      var mockRepo = new Mock<ITenantRepository>();
      mockRepo.Setup(repo => repo.GetBatchesAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => testBatches);

      return mockRepo;
    }
    internal static Mock<ITenantRepository> MockTenantRepo(List<LibMod.Tenant> testTenants)
    {
      var mockRepo = new Mock<ITenantRepository>();
      mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid i) =>
            {
              var tenant = testTenants.FirstOrDefault(t => t.Id == i);
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
