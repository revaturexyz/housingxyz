using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Revature.Address.DataAccess;
using Xunit;

namespace Revature.Address.Tests.DataAccess.Tests
{
  /// <summary>
  /// This class runs tests for data access
  /// functionality
  /// </summary>
  public class DataAccessTests
  {
    /// <summary>
    /// This tests checks if a database can be
    /// successfully created
    /// </summary>
    [Fact]
    public void CanCreateDB()
    {
      var options = TestDbContext.TestDbInitalizer("CanCreate");
      var mapper = new Mapper();
      using var dbcreate = TestDbContext.CreateTestDb(options);
      var logger = new NullLogger<Address.DataAccess.DataAccess>();
      var addyrepo = new Address.DataAccess.DataAccess(dbcreate, mapper, logger);
      Assert.NotNull(addyrepo);
    }

    /// <summary>
    /// This tests checks if an address can
    /// be added to the database
    /// </summary>
    [Fact]
    public async void CreateAddressAsync()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetId"); //Create Instance of DB. 
      using var dbcreate = TestDbContext.CreateTestDb(options);
      var newAddy = new Address.Lib.Address
      {
        Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Street = "1100 N E St",
        City = "Arlington",
        State = "Texas",
        Country = "US",
        ZipCode = "76010"
      };
      var mapper = new Mapper();
      var logger = new NullLogger<Address.DataAccess.DataAccess>();
      var successful = await new Address.DataAccess.DataAccess(dbcreate, mapper, logger).AddAddressAsync(newAddy);
      Assert.True(successful);
    }

    /// <summary>
    /// This tests if we can successfully retrieve
    /// an address from the database using its id
    /// </summary>
    [Fact]
    public async Task GetAddressAsync()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetID"); //Create Instance of DB. 
      //Insert data into test, specifically ValidAddress1().
      var saveId = AddressTestData.ValidAddress1().Id;
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.Add(AddressTestData.ValidAddress1());
        dbcreate.SaveChanges();
      }
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var mapper = new Mapper();
        var logger = new NullLogger<Address.DataAccess.DataAccess>();
        var repo = new Address.DataAccess.DataAccess(dbcreate, mapper, logger);
        var addresses = await repo.GetAddressAsync(saveId);
        var addressResult = addresses.First();

        Assert.Equal(saveId, addressResult.Id);
      }
    }

    /// <summary>
    /// This tests checks we can successfully delete an
    /// address from the database using its id
    /// </summary>
    [Fact]
    public async Task DeleteAddressAsync()
    {
      var options = TestDbContext.TestDbInitalizer("Delete Address");
      var mapper = new Mapper();
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.AddRange(
          AddressTestData.ValidAddress1(),
          AddressTestData.ValidAddress2());
        dbcreate.SaveChanges();
      }

      //We want to delete Address2. 
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var testId = AddressTestData.ValidAddress2().Id;
        var logger = new NullLogger<Address.DataAccess.DataAccess>();
        var addressResult = await new Address.DataAccess.DataAccess(dbcreate, mapper, logger).DeleteAddressAsync(testId);
        Assert.True(addressResult);
      }
    }
  }
}
