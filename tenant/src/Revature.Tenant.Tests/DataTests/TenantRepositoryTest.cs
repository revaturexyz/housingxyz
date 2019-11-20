using Xunit;
using Revature.Tenant.DataAccess.Repository;
using Revature.Tenant.DataAccess;
using System.Threading.Tasks;
using System;

namespace Revature.Tenant.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in TenantRepository class
  /// </summary>
  public class TenantRepositoryTests
  {
    /// <summary>
    /// A test for the Constructor to Construct 
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange 
      var options = TestDbInitializer.InitializeDbOptions("TestConstructor");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      // Act 
      var repo = new TenantRepository(db, mapper);
      // Assert 
    }

    /// <summary>
    /// Checks that AddAsync creates a new Tenant in Db
    /// </summary>
    [Fact]
    public async Task AddShouldAddTest()
    {
      // Arrange
      var options = TestDbInitializer.InitializeDbOptions("AddShouldAddTest");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      var repo = new TenantRepository(db, mapper);
      var tenant = new Lib.Models.Tenant
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Email = "firstname@email.com",
        Gender = "Male",
        FirstName = "Clary",
        LastName = "Colton",
        AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67c"),
        RoomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67c"),
        CarId = 1,
        BatchId = 3,
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
      };

      // Act
      await repo.AddAsync(tenant);
      await repo.SaveAsync();
      var check = await repo.GetByIdAsync(Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"));
      // Assert

      Assert.NotNull(check);
      Assert.Equal(tenant.Email, check.Email);
      Assert.Equal(tenant.Gender, check.Gender);
      Assert.Equal(tenant.FirstName, check.FirstName);
      Assert.Equal(tenant.LastName, check.LastName);
      Assert.Equal(tenant.AddressId, check.AddressId);
      Assert.Equal(tenant.RoomId, check.RoomId);
      Assert.Equal(tenant.CarId, check.CarId);
      Assert.Equal(tenant.BatchId, check.BatchId);
    }

    /// <summary>
    /// Checks that HasCarAsync Returns true if the tenant has a car
    /// </summary>
    [Fact]
    public async Task HasCarShouldReturnTrueIfHasCar()
    {
      //Arrange
      var options = TestDbInitializer.InitializeDbOptions("AddShouldAddTest");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      var repo = new TenantRepository(db, mapper);
      var tenant = new Lib.Models.Tenant
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Email = "firstname@email.com",
        Gender = "Male",
        FirstName = "Clary",
        LastName = "Colton",
        AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67c"),
        RoomId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67c"),
        CarId = 1,
        BatchId = 3,
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
      };


      //Act

      await repo.AddAsync(tenant);
      await repo.SaveAsync();

      var check = await repo.HasCarAsync(Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"));

      //Assert

      Assert.True(check);

    }


  }
}
