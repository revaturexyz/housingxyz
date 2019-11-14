using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xyz.Provider.Api.Controllers;
using Xyz.Provider.Api.Models;
using Xyz.Provider.DataAccess.Repository;
using Xyz.Provider.Tests.DataTests;

namespace Xyz.Provider.Tests.ApiTests.ControllerTests
{
  public class AddressControllerTest
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (create database)
      var options = TestDbInitializer.InitializeDbOptions("TestAddressControllerConstructor");
      using var db = TestDbInitializer.CreateTestDb(options);

      // act (pass repository with database into controller)
      var test = new AddressController(new AddressRepository(db));

      // assert (test passes if no exception thrown)
    }

    [Fact]
    public async Task GetShouldGetComplex()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAddressByComplexID");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new AddressController(new AddressRepository(db));

      // act (attempt to get an address list by a complex id)
      var result = await controller.GetByComplexIdAsync(2);

      // assert (assure the address list is received)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var test = Assert.IsAssignableFrom<IEnumerable<ApiAddress>>(ok.Value);
      Assert.NotEmpty(test);
    }

    [Fact]
    public async Task GetShouldGetProvider()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAddressByProviderID");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new AddressController(new AddressRepository(db));

      // act (get a list of addresses by a provider id)
      var result = await controller.GetByProviderIdAsync(1);

      // assert (assure the list is received)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var test = Assert.IsAssignableFrom<IEnumerable<ApiAddress>>(ok.Value);
      Assert.NotEmpty(test);
    }

    [Fact]
    public async Task BadIdShouldBeNotFound()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetWithBadIDs");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new AddressController(new AddressRepository(db));

      // act (try to get addresses by a bad provider id)
      var result = await controller.GetByProviderIdAsync(-1);

      // assert (assure the controller returns the NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result.Result);

      // act (try to get addresses by a bad complex id)
      result = await controller.GetByComplexIdAsync(-1);

      // assert (assure the controller returns the NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result.Result);
    }
  }
}
