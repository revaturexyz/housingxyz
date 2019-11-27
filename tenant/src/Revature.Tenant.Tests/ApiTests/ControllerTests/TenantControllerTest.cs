using System;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using Revature.Tenant.DataAccess.Repository;
using Revature.Tenant.Tests.DataTests;
using Revature.Tenant.DataAccess;
using Revature.Tenant.Api.Controllers;
using Revature.Tenant.Api.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Revature.Tenant.Lib.Interface;
using Microsoft.Extensions.Logging;

namespace Revature.Tenant.Tests.ApiTests
{
  public class TenantControllerTest
  {
    /// <summary>
    /// Tests that Constructor for Tenant Controller successfully constructs
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (create database)
      var mockLogger = new Mock<ILogger<TenantController>>();
      var options = TestDbInitializer.InitializeDbOptions("TestTenantControllerConstructor");
      using var database = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();

      // act (pass repository with database into controller)
      var test = new TenantController(new TenantRepository(database, mapper), mockLogger.Object);

      // assert (test passes if no exception thrown)
    }

    /// <summary>
    /// Tests that Controller Method, GetByIdAsync(), Returns Ok result with ApiTenant
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetByIdShouldGetByIdAsync()
    {
      // Arrange (create a moq repo and use it for the controller)
      var mockLogger = new Mock<ILogger<TenantController>>();
      Mock<ITenantRepository> mockRepo = ApiTestData.MockTenantRepo(ApiTestData.Tenants.ToList());
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);
      // Act (get a Tenant with an id)

      var colton = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");
      var result = await controller.GetByIdAsync(colton);

      // Assert (ensure the provider is returned with the correct values)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var tenant = Assert.IsAssignableFrom<ApiTenant>(ok.Value);
      Assert.NotNull(tenant);
    }
    /// <summary>
    /// Tests that Tenant Controller Method, GetAllBatches(), returns OK Object Result and List of Library Batches
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetAllBatchesByTCShouldGetAllByTCAsync()
    {
      //Arrange (create a moq repo and use it for the controller)
      Mock<ITenantRepository> mockRepo = ApiTestData.MockBatchRepo(ApiTestData.Batches.ToList());
      var options = TestDbInitializer.InitializeDbOptions("GetAllBatchesByTCShouldGetAllByTCAsync");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();

      var mockLogger = new Mock<ILogger<TenantController>>();
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);


      //Act (get all batches)

      var result = await controller.GetAllBatches("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");

      //Assert

      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var batches = Assert.IsAssignableFrom<System.Collections.Generic.List<Lib.Models.Batch>>(ok.Value);
      Assert.NotNull(batches);
    }

    /// <summary>
    /// Tests that Tenant Controller Method, PostAsync, returns Object Result and ApiTenant
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PostShouldPost()
    {
      // Arrange (create a moq repo and use it for the controller)
      var mockLogger = new Mock<ILogger<TenantController>>();
      Mock<ITenantRepository> mockRepo = ApiTestData.MockTenantRepo(ApiTestData.Tenants.ToList());
      mockRepo.Setup(r => r.AddAsync(It.IsAny<Lib.Models.Tenant>()));
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);

      //Act
      var result = await controller.PostAsync(new ApiTenant
      {
        AddressId = Guid.Parse("fa4d8c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        ApiAddress = null,
        ApiBatch = null,
        ApiCar = null,
        BatchId = 1,
        CarId = 1,
        Email = "e@mail.com",
        FirstName = "Victoria",
        Gender = "Female",
        Id = null,
        LastName = "Something Spanish",
        RoomId = null,
        TrainingCenter = Guid.Parse("fa416c6e-9650-44c9-8c6b-5aebd3f9a670")
      });

      //Assert
      var ok = Assert.IsAssignableFrom<ObjectResult>(result.Result);
      var tenant = Assert.IsAssignableFrom<string>(ok.Value);
      Assert.NotNull(tenant);
    }

    /// <summary>
    /// Tests that UpdateAsync() Returns Status Code 204
    /// </summary>
    [Fact]
    public async Task UpdateAsyncShouldReturnStatusCode204()
    {
      //Arrange (create a moq repo and use it for the controller)
      Mock<ITenantRepository> mockRepo = ApiTestData.MockBatchRepo(ApiTestData.Batches.ToList());
      var options = TestDbInitializer.InitializeDbOptions("GetAllBatchesByTCShouldGetAllByTCAsync");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();

      var mockLogger = new Mock<ILogger<TenantController>>();
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);

      //Act
      var apiTenant = new ApiTenant
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Email = "colton@colton.com",
        Gender = "male",
        FirstName = "Colton",
        LastName = "Clary",
        AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        ApiBatch = new ApiBatch
        {
          TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          Id = 1,
          BatchCurriculum = "c#"
        },
        ApiCar = new ApiCar
        {
          Id = 1,
          Color = "y",
          LicensePlate = "123",
          Make = "s",
          Model = "2",
          State = "w",
          Year = "l"
        },
        ApiAddress = new ApiAddress
        {
          State = "sdl",
          AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          City = "l",
          Country = "l",
          Street = "s",
          ZipCode = "l"
        }
      };
      var result = await controller.UpdateAsync(apiTenant);

      //Assert
      var ok = Assert.IsAssignableFrom<StatusCodeResult>(result);
    }

    [Fact]
    public async Task DeleteShouldReturnStatusCode204()
    {
      //Arrange (create a moq repo and use it for the controller)
      Mock<ITenantRepository> mockRepo = ApiTestData.MockBatchRepo(ApiTestData.Batches.ToList());
      var options = TestDbInitializer.InitializeDbOptions("DeleteShouldReturnStatusCode204");
      using var db = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();

      var mockLogger = new Mock<ILogger<TenantController>>();
      var controller = new TenantController(mockRepo.Object, mockLogger.Object);

      //Act (Add a tenant to be deleted)
      var apiTenant = new ApiTenant
      {
        Id = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        Email = "colton@colton.com",
        Gender = "male",
        FirstName = "Colton",
        LastName = "Clary",
        AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
        ApiBatch = new ApiBatch
        {
          TrainingCenter = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          Id = 1,
          BatchCurriculum = "c#"
        },
        ApiCar = new ApiCar
        {
          Id = 1,
          Color = "y",
          LicensePlate = "123",
          Make = "s",
          Model = "2",
          State = "w",
          Year = "l"
        },
        ApiAddress = new ApiAddress
        {
          State = "sdl",
          AddressId = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d"),
          City = "l",
          Country = "l",
          Street = "s",
          ZipCode = "l"
        }
      };

      var result = await controller.DeleteAsync("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");

      //Arrange
      var ok = Assert.IsAssignableFrom<StatusCodeResult>(result);
    }
  }
}
