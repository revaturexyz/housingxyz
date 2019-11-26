using Revature.Address.DataAccess;
using System.Linq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;

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
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var addyrepo = new Address.DataAccess.DataAccess(dbcreate);
        Assert.NotNull(addyrepo);
      }
    }

    /// <summary>
    /// This tests checks if an address can
    /// be added to the database
    /// </summary>
    [Fact]
    public void CreateAddress()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetId"); //Create Instance of DB. 
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        Address.Lib.Address newAddy = new Address.Lib.Address
        {
          Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
          Street = "1100 N E St",
          City = "Arlington",
          State = "Texas",
          Country = "US",
          ZipCode = "76010"
        };
        var mapper = new Mapper();
        bool successful = (new Address.DataAccess.DataAccess(dbcreate, mapper).AddAddressAsync(newAddy)).Result;
        Assert.True(successful);
      }
    }

    /// <summary>
    /// This tests if we can successfully retrieve
    /// an address from the database using its id
    /// </summary>
    [Fact]
    public void GetAddress()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetID"); //Create Instance of DB. 
      //Insert data into test, specifically ValidAddress1().
      Guid saveId = AddressTestData.ValidAddress1().Id;
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.Add(AddressTestData.ValidAddress1());
        dbcreate.SaveChanges();
      }
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var mapper = new Mapper();
        var AddressList = new List<Address.Lib.Address>();
        Address.DataAccess.DataAccess repo = new Address.DataAccess.DataAccess(dbcreate,mapper);
        Task<ICollection<Address.Lib.Address>> AddressExpected = repo.GetAddressAsync(saveId);
        AddressList.Add(AddressExpected.Result.First());

        Assert.Single(AddressList);
        Assert.Equal(saveId, AddressList[0].Id);
      }
    }

    /// <summary>
    /// This tests checks we can successfully delete an
    /// address from the database using its id
    /// </summary>
    [Fact]
    public void DeleteAddress()
    {
      var options = TestDbContext.TestDbInitalizer("Delete Address");
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
        var TestId = AddressTestData.ValidAddress2().Id;
        var AddressResult = new Address.DataAccess.DataAccess(dbcreate).DeleteAddressAsync(TestId).Result;
        Assert.True(AddressResult);
      }
    }
  }
}
