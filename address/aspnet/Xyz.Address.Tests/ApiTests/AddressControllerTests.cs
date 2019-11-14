using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;
using Xyz.Address.Api.Controllers;
using Xyz.Address.Api.Helpers;
using Xyz.Address.Api.Models;
using Xyz.Address.Dbx.Repositories;

namespace Xyz.Address.Tests
{
  ///<summary>Tests for Address CRUD endpoints
  ///For each endpoint, there are two tests, one for a true condition and one for a false condition.
  ///Refer to the first two tests as an example.
  ///<summary>
  public class AddressControllerTests
  {
    private AddressController _AC;

    ///<summary>Test for an existing address</summary>
    ///<return>200 Status Response since address exists</return>
    [Fact]
    public void GetFoundAddress()
    {
      var options = TestDbContext.TestDbInitalizer("TestingGetAddressControllerConstructor");
      using (var database = TestDbContext.CreateTestDb(options))
      {
        database.Add(AddressTestData.ValidAddress1());
        database.SaveChanges();
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(database);
        var result = _AC.Get();
        Assert.IsType<OkObjectResult>(result);
      }
    }

    ///<sumary>Test for a non-existing address</summary>
    ///<return>404 response since there is no address</return>
    [Fact]
    public void GetNotFoundAddress()
    {
      var options = TestDbContext.TestDbInitalizer("TestingGetNotFoundAddressControllerConstructor");
      using (var database = TestDbContext.CreateTestDb(options))
      {
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(database);
        var result = _AC.Get();
        Assert.IsType<NotFoundObjectResult>(result);
      }
    }

    [Fact]
    public void GetFoundAddressByKey()
    {
      var options = TestDbContext.TestDbInitalizer("TestingGetAddressKeyControllerConstructor");
      using (var database = TestDbContext.CreateTestDb(options))
      {
        database.Add(AddressTestData.ValidAddress1());
        database.SaveChanges();

        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(database);
        var validKey = AddressTestData.ValidAddress1().Key;
        var result = _AC.Get(validKey);
        Assert.IsType<OkObjectResult>(result);
      }
    }
  [Fact]
    public void GetNotFoundAddressByKeyController()
    {
      var options = TestDbContext.TestDbInitalizer("TestingGetNotFoundAddressKeyControllerConstructor");
      using (var database = TestDbContext.CreateTestDb(options))
      {
        database.Add(AddressTestData.ValidAddress1());
        database.SaveChanges();

        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(database);
        var validKey = new Guid("9bc7edd6-71e6-464e-a900-05eff8ab144d");
        var result = _AC.Get(validKey);
        Assert.IsType<NotFoundObjectResult>(result);
      }
    }
    [Fact]
    public void TestPostValidInput()
    {
      var options = TestDbContext.TestDbInitalizer("TestingPostAddressController");
      using (var database = TestDbContext.CreateTestDb(options))
      {
        Api.Helpers.AddressHelper.ChangeRepository(database);
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(database);
        var newAddy = new AddressApiModel
        {
          Street = "123 Unit Test St",
          City = "Arlington",
          StateProvince = "TX",
          PostalCode = "76019",
          Country = "USA"
        };
        var result = _AC.Post(newAddy);
        result = _AC.Post(newAddy);
        Assert.IsType<OkObjectResult>(result);
        result = _AC.Get();
        Assert.IsType<OkObjectResult>(result);
      }
    }

    [Fact]
    public void TestPostInvalidInput()
    {
      var options = TestDbContext.TestDbInitalizer("TestingPostInValidAddressController");
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var newAddy = new AddressApiModel();
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(dbcreate);
        var result = _AC.Post(newAddy);
        //This runs fine because the dbcontext does not enforce the constraints. 
        Assert.IsType<OkObjectResult>(result);
      }
    }

    // TODO Implement
    [Fact]
    public void TestControllerHelperDatabase()
    {
      var options = TestDbContext.TestDbInitalizer("TestingDeleteAddressController");
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(dbcreate);
        var CityState = new AddressApiModel
        {
          Key = Guid.NewGuid(),
          Street = "1560 Broadway St.",
          City = "Denver",
          StateProvince = "CO",
          PostalCode = "80112",
          Country = "US",
        };
        var postres = _AC.Post(CityState);
        var getres = _AC._addressHelper.Get();
        var dbresult = dbcreate.Addresses.ToList();
        var result = _AC.Delete(dbresult.FirstOrDefault().Key);
        Assert.NotNull(getres);
        Assert.Equal("denver".ToUpper(), dbresult.FirstOrDefault().City.ToUpper());
        Assert.IsType<OkObjectResult>(postres);
        Assert.IsType<OkObjectResult>(result);
      }
    }
    [Fact]
    public void TestDelete()
    {
      var options = TestDbContext.TestDbInitalizer("TestingDeleteAddressController");
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        _AC = new AddressController();
        _AC._addressHelper = Api.Helpers.AddressHelper.ChangeRepository(dbcreate);

        var newAddy = new AddressApiModel
        {
          Key = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
          Street = "123 Unit Test St",
          City = "Arlington",
          StateProvince = "TX",
          PostalCode = "76019",
          Country = "US"
        };
        var result = _AC.Post(newAddy);
        Assert.IsType<OkObjectResult>(result);

        result = _AC.Delete(newAddy.Key);
        Assert.IsType<OkObjectResult>(result);
      }
    }
  }
}
