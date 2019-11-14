using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xyz.Address.Dbx.Models;
using Xyz.Address.Dbx.Repositories;
using Xyz.Address.Lib.Models;

namespace Xyz.Address.Tests
{
    public class AddressDataRepository
    {
      [Fact]
      public void CanCreateDB()
      {
        var options = TestDbContext.TestDbInitalizer("CanCreate");
        using (var dbcreate = TestDbContext.CreateTestDb(options))
        {
          var addyrepo = new AddressRepository(dbcreate);
          Assert.NotNull(addyrepo);
        }
      }

    [Fact]
    public void CreateAddress()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetId"); //Create Instance of DB. 
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        AddressEntity newAddy = new AddressEntity
        {
          Id = 1 
        };
        bool successful = new AddressRepository(dbcreate).Create(newAddy);
        Assert.True(successful);
      }
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        Assert.Equal(1, dbcreate.Addresses.Count());
        Assert.Equal(1, dbcreate.Addresses.Single().Id);
      }
    }

    [Fact]
    public void ReadAddress()
    {
      var options = TestDbContext.TestDbInitalizer("CanGetID"); //Create Instance of DB. 
      //Insert data into test, specifically ValidAddress1().
      Guid saveKey = AddressTestData.ValidAddress1().Key;
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.Add(AddressTestData.ValidAddress1());
        dbcreate.SaveChanges();
      }
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var AddressList = new List<AddressModel>();
        var AddressExpected =  new AddressRepository(dbcreate).Read(saveKey);
        AddressList.Add(AddressExpected);

        Assert.Equal(1, AddressList.Count());
        Assert.Equal(saveKey, AddressList[0].Key);
      }
    }

    [Fact]
    public void ReadAllAddress()
    {
      var options = TestDbContext.TestDbInitalizer("Read All Addresses"); //Create Instance of DB. 
      //Insert data into test database
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.AddRange(
          AddressTestData.ValidAddress1(), 
          AddressTestData.ValidAddress2());
        dbcreate.SaveChanges();  
      }
      
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var AddressExpected =  new AddressRepository(dbcreate).ReadAll();
        var count = AddressExpected.Count();
        Assert.Equal(2, count);
      }
    }

    [Fact]
    public void UpdateValidAddress()
    {
      var options = TestDbContext.TestDbInitalizer("Update Address");
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        dbcreate.Add(AddressTestData.ValidAddress1());
        dbcreate.SaveChanges();
      }

      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var AddressResult = new AddressRepository(dbcreate).Update(AddressTestData.UpdateValidAddress1());
        Assert.True(AddressResult);
      }
    }

    [Fact]
    public void UpdateInvalidAddress()
    {
      var options = TestDbContext.TestDbInitalizer("Update Address");
      using (var dbcreate = TestDbContext.CreateTestDb(options))
      {
        var AddressResult = new AddressRepository(dbcreate).Update(AddressTestData.UpdateInvalidAddress1());
        Assert.False(AddressResult);   
      }
    }

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
        var TestKey = AddressTestData.ValidAddress2().Key;
        var AddressResult = new AddressRepository(dbcreate).Delete(TestKey);
        Assert.True(AddressResult);
      }
    }

    [Fact]
    public void DeleteInvalidAddress()
    {
      var options = TestDbContext.TestDbInitalizer("Delete Invalid Address");
      //Want to delete Address1 but accidentally use Address2 Key. 
        using (var dbcreate = TestDbContext.CreateTestDb(options))
        {
          dbcreate.Add(
            AddressTestData.ValidAddress1());
          dbcreate.SaveChanges();
        }

        using (var dbcreate = TestDbContext.CreateTestDb(options))
        {
          var TestKey = AddressTestData.ValidAddress2().Key;
          var AddressResult = new AddressRepository(dbcreate).Delete(TestKey);
          Assert.False(AddressResult);
        }     
      }
    }
}
