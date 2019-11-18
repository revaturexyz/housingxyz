using System;
using System.Collections.Generic;
using System.Text;
using Entity = Revature.Complex.DataAccess.Entities;
using Revature.Complex.DataAccess;
using Logic = Revature.Complex.Lib.Models;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Revature.Complex.DataAccess.Repository;

namespace Revature.Complex.Tests.DataTests
{
  public class RepositoryTest
  {
    # region data to test 
    public static Guid cId1 = Guid.NewGuid();
    public static Guid aId1 = Guid.NewGuid();
    public static Guid pId1 = Guid.NewGuid();
    public static Guid cId2 = Guid.NewGuid();
    public static Guid aId2 = Guid.NewGuid();
    public static Guid pId2 = Guid.NewGuid();
    public static Guid rId = Guid.NewGuid();
    public static Guid amId1 = Guid.NewGuid();
    public static Guid amId2 = Guid.NewGuid();
    public static Guid acId1 = Guid.NewGuid();
    public static Guid acId2 = Guid.NewGuid();
    public static Guid arId1 = Guid.NewGuid();
    public static Guid arId2 = Guid.NewGuid();

    public Logic.Complex complex1 = new Logic.Complex
    {
      ComplexId = cId1,
      AddressId = aId1,
      ProviderId = pId1,
      ComplexName = "Liv+",
      ContactNumber = "1234567890"
    };

    public Logic.Complex complex2 = new Logic.Complex
    {
      ComplexId = cId2,
      AddressId = aId2,
      ProviderId = pId2,
      ComplexName = "SampleComplex",
      ContactNumber = "9876543210"
    };

    public Entity.Complex complexE1 = new Entity.Complex
    {
      ComplexId = cId1,
      AddressId = aId1,
      ProviderId = pId1,
      ComplexName = "Liv+",
      ContactNumber = "1234567890"
    };

    public Entity.Complex complexE2 = new Entity.Complex
    {
      ComplexId = cId2,
      AddressId = aId2,
      ProviderId = pId2,
      ComplexName = "SampleComplex",
      ContactNumber = "9876543210"
    };

    public Logic.AmenityRoom ar = new Logic.AmenityRoom
    {
      AmenityRoomId = arId1,
      RoomId = rId,
      AmenityId = amId1
    };

    public Logic.AmenityComplex ac = new Logic.AmenityComplex
    {
      AmenityComplexId = acId1,
      ComplexId = cId1,
      AmenityId = amId1
    };

    public Entity.AmenityComplex acE1 = new Entity.AmenityComplex
    {
      AmenityComplexId = acId1,
      ComplexId = cId1,
      AmenityId = amId1
    };

    public Entity.AmenityComplex acE2 = new Entity.AmenityComplex
    {
      AmenityComplexId = acId2,
      ComplexId = cId1,
      AmenityId = amId2
    };

    public Logic.Amenity amenity = new Logic.Amenity
    {
      AmenityId = amId1,
      AmenityType = "Fridge",
      Description = "frozen"
    };

    public Entity.Amenity am1 = new Entity.Amenity
    {
      AmenityId = amId1,
      AmenityType = "Fridge",
      Description = "to freeze"
    };

    public Entity.Amenity am2 = new Entity.Amenity
    {
      AmenityId = amId2,
      AmenityType = "Test2",
      Description = "to heat"
    };

    public Entity.AmenityRoom arE1 = new Entity.AmenityRoom
    {
      AmenityRoomId = arId1,
      RoomId = rId,
      AmenityId = amId1
    };

    public Entity.AmenityRoom arE2 = new Entity.AmenityRoom
    {
      AmenityRoomId = arId2,
      RoomId = rId,
      AmenityId = amId2
    };
    #endregion 

    [Fact]
    public async void CreateComplexAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("CreateComplexAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      string result = await repo.CreateComplexAsync(complex1);
      Guid checker = testContext.Complex.First().ComplexId;

      Assert.Equal(checker, cId1);
    }

    [Fact]
    public async void ReadComplexListTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadComplexListTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(complexE1);
      testContext.Add(complexE2);
      testContext.SaveChanges();

      List<Logic.Complex> list = await repo.ReadComplexListAsync();

      Assert.Equal("Liv+", list[0].ComplexName);
      Assert.Equal("9876543210", list[1].ContactNumber);
    }

    [Fact]
    public async void ReadComplexAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadComplexAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(complexE1);

      Logic.Complex read = await repo.ReadComplexAsync(cId1);

      Assert.Equal(cId1, read.ComplexId);
    }

    [Fact]
    public async void UpdateComplexAsync()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("UpdateComplexAsync")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(complexE1);
      testContext.SaveChanges();

      Logic.Complex update = new Logic.Complex
      {
        ComplexId = cId1,
        ComplexName = "Liv++",
        ContactNumber = "7894561231"
      };

      string result = await repo.UpdateComplexAsync(update);
      result = testContext.Complex.Find(cId1).ComplexName;
      string phone = testContext.Complex.Find(cId1).ContactNumber;

      Assert.Equal("Liv++", result);
      Assert.Equal("7894561231", phone);
    }

    [Fact]
    public async void DeleteComplexTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("DeleteComplexTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(complexE1);
      testContext.Add(complexE2);

      string result = await repo.DeleteComplexAsync(cId1);

      result = testContext.Complex.First().ComplexName;
      string phone = testContext.Complex.First().ContactNumber;

      Assert.Equal("SampleComplex", result);
      Assert.Equal("9876543210", phone);
    }

    [Fact]
    public async void CreateAmenityRoomAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("CreateAmenityRoomAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      string result = await repo.CreateAmenityRoomAsync(ar);
      Guid check = testContext.AmenityRoom.First().AmenityRoomId;

      Assert.Equal(check, ar.AmenityRoomId);
    }

    [Fact]
    public async void CreateAmenityComplexAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("CreateAmenityComplexAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      string result = await repo.CreateAmenityComplexAsync(ac);
      Guid check = testContext.AmenityComplex.First().AmenityComplexId;

      Assert.Equal(check, ac.AmenityComplexId);
    }

    [Fact]
    public async void CreateAmenityAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("CreateAmenityAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      string result = await repo.CreateAmenityAsync(amenity);

      Guid check = testContext.Amenity.First().AmenityId;

      Assert.Equal(amenity.AmenityId, check);
    }

    [Fact]
    public async void ReadAmenityListTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(am1);
      testContext.Add(am2);
      testContext.SaveChanges();

      List<Logic.Amenity> am = await repo.ReadAmenityListAsync();

      Assert.Equal(am1.AmenityId, am[0].AmenityId);
      Assert.Equal("Test2", am[1].AmenityType);
    }

    [Fact]
    public async void ReadAmenityListByComplexIdTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListByComplexIdTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(complexE1);
      testContext.Add(am1);
      testContext.Add(am2);
      testContext.Add(acE1);
      testContext.Add(acE2);
      testContext.SaveChanges();

      List<Logic.Amenity> am = await repo.ReadAmenityListByComplexIdAsync(cId1);

      Assert.Equal("Fridge", am[0].AmenityType);
      Assert.Equal("Test2", am[1].AmenityType);
    }

    [Fact]
    public async void ReadAmenityListByRoomIdTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListByRoomIdTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(am1);
      testContext.Add(am2);
      testContext.Add(arE1);
      testContext.Add(arE2);
      testContext.SaveChanges();

      List<Logic.Amenity> am = await repo.ReadAmenityListByRoomIdAsync(rId);

      Assert.Equal("Fridge", am[0].AmenityType);
      Assert.Equal("Test2", am[1].AmenityType);
    }

    [Fact]
    public async void ReadComplexByProviderIDTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadComplexByProviderIDTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      Entity.Complex complexE3 = new Entity.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = pId1,
        ComplexName = "XXX",
        ContactNumber = "1234567895"
      };

      testContext.Add(complexE1);
      testContext.Add(complexE2);
      testContext.Add(complexE3);
      testContext.SaveChanges();

      List<Logic.Complex> complices = await repo.ReadComplexByProviderIdAsync(pId1);

      Assert.Equal("Liv+", complices[0].ComplexName);
      Assert.Equal("XXX", complices[1].ComplexName);
    }

    [Fact]
    public async void UpdateAmenityAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("UpdateAmenityAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(am1);
      testContext.Add(am2);

      Logic.Amenity update1 = new Logic.Amenity
      {
        AmenityId = amId1,
        AmenityType = "Microwave",
        Description = "To heat foods"
      };

      await repo.UpdateAmenityAsync(update1);
      Entity.Amenity check1 = testContext.Amenity.Find(amId1);

      Assert.Equal("Microwave", check1.AmenityType);
    }

    [Fact]
    public async void DeleteAmenityAsyncTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("DeleteAmenityAsyncTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      Guid amId3 = Guid.NewGuid();
      Entity.Amenity am3 = new Entity.Amenity
      {
        AmenityId = amId3,
        AmenityType = "Pool",
        Description = "swimming"
      };

      testContext.Add(am1);
      testContext.Add(am2);
      testContext.Add(am3);

      Logic.Amenity delete1 = new Logic.Amenity
      {
        AmenityId = amId1
      };

      await repo.DeleteAmenityAsync(delete1);

      Entity.Amenity check = testContext.Amenity.First();

      Assert.Equal(amId2, check.AmenityId);
    }
  } 
}
