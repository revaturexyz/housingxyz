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

namespace Revature.Tenant.Tests.ApiTests
{
  public class TenantsControllerTest
  {
    //TODO will add tests for tenants controller

    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (create database)
      var options = TestDbInitializer.InitializeDbOptions("TestTenantsControllerConstructor");
      using var database = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      // act (pass repository with database into controller)
      var test = new TenantsController(new Repository(database, mapper));

      // assert (test passes if no exception thrown)
    }

    [Fact]
    public async Task GetByIdAsyncShouldGetById()
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<ITenantRepository> mockRepo = ApiTestData.MockTenantRepo(ApiTestData.Tenants.ToList());
      var controller = new TenantsController(mockRepo.Object);
      // Act (get a Tenant with an id)
     
      var colton = Guid.Parse("fa4d6c6e-9650-44c9-8c6b-5aebd3f9a67d");
      var result = await controller.GetByIdAsync(colton);
      // Assert (ensure the provider is returned with the correct values)

      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var tenant = Assert.IsAssignableFrom<ApiTenant>(ok.Value);
      Assert.NotNull(tenant);
    }
  }
}
