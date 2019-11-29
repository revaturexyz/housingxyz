using Revature.Tenant.DataAccess;
using Revature.Tenant.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Tenant.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in TenantRoomRepository class
  /// </summary>
  public class TenantRoomRepositoryTest
  {
    /// <summary>
    /// GetTenantsByRoomId Should Return a List of Tenants
    /// </summary>
    [Fact]
    public async Task GetTenantsByRoomIdShouldReturnList()
    {
      var options = TestDbInitializer.InitializeDbOptions("GetTenantsByRoomIdShouldReturnList");
      using var _context = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      var repo = new TenantRoomRepository(_context, mapper);

      var tenant = new DataAccess.Entities.Tenant() {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9ac7d"),
        Email = "firstname@email.com",
        Gender = "Male",
        FirstName = "Clary",
        LastName = "Colton",
        AddressId = Guid.Parse("fa4d6c6e-9650-45c9-8c6b-5aebd3f9a67c"),
        RoomId = Guid.Parse("fa4d6c6e-9650-44c9-5c6b-5aebd3f9a67c"),
        CarId = 3,
        BatchId = 3,
        TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Car = new DataAccess.Entities.Car
        {
          Id = 3,
          LicensePlate = "LicensePlate",
          Make = "Make",
          Model = "Model",
          Color = "Color",
          Year = "Year",
          State = "TX"
        },
        Batch = new DataAccess.Entities.Batch
        {
          Id = 3,
          BatchCurriculum = "C#",
          TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          StartDate = DateTime.Now,
          EndDate = DateTime.Now.AddDays(3)
        }
      };

      await _context.Tenant.AddAsync(tenant);
      await _context.SaveChangesAsync();

      var result = await repo.GetTenantsByRoomId(Guid.Parse("fa4d6c6e-9650-44c9-5c6b-5aebd3f9a67c"));

      Assert.NotNull(result);
      Assert.Equal("Clary", result.First().FirstName);

      result = await repo.GetTenantsByRoomId(Guid.NewGuid());
      Assert.True(result.Count == 0);
    }
    /// <summary>
    /// GetRoomLessTenants should return a list of Tenants who have not yet been assigned a room.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetRoomlessTenantsShouldReturnList()
    {
      var options = TestDbInitializer.InitializeDbOptions("GetTenantsByRoomIdShouldReturnList");
      using var _context = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      var repo = new TenantRoomRepository(_context, mapper);

      var tenant = new DataAccess.Entities.Tenant()
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9ac7d"),
        Email = "firstname@email.com",
        Gender = "Female",
        FirstName = "Lana",
        LastName = "Del Ray",
        AddressId = Guid.Parse("fa4d6c6e-9650-45c9-8c6b-5aebd3f9a67c"),
        RoomId = null,
        CarId = 3,
        BatchId = 3,
        TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Car = new DataAccess.Entities.Car
        {
          Id = 3,
          LicensePlate = "LicensePlate",
          Make = "Make",
          Model = "Model",
          Color = "Color",
          Year = "Year",
          State = "TX"
        },
        Batch = new DataAccess.Entities.Batch
        {
          Id = 3,
          BatchCurriculum = "C#",
          TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          StartDate = DateTime.Now,
          EndDate = DateTime.Now.AddDays(3)
        }
      };

      await _context.Tenant.AddAsync(tenant);
      await _context.SaveChangesAsync();

      var result = await repo.GetRoomlessTenants();

      Assert.NotNull(result);
      Assert.Equal("Lana", result.First().FirstName);
      Assert.IsType<List<Lib.Models.Tenant>>(result);
    }
  }
}
