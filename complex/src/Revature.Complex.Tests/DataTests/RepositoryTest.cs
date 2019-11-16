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

      Guid cId = Guid.NewGuid();
      Guid aId = Guid.NewGuid();
      Guid pId = Guid.NewGuid();

      Logic.Complex complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = aId,
        ProviderId = pId,
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };

      string result = await repo.CreateComplexAsync(complex);
      Guid checker = testContext.Complex.Last().ComplexId;

      Assert.Equal(checker, cId);
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

      Logic.Complex c1 = new Logic.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "Liv+",
        ContactNumber = "1234567890"
      };

      Logic.Complex c2 = new Logic.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "SampleComplex",
        ContactNumber = "9876543215"
      };
      string r1 = await repo.CreateComplexAsync(c1);
      string r2 = await repo.CreateComplexAsync(c2);

      List<Logic.Complex> list = repo.ReadComplexList().ToList();

      Assert.Equal("Liv+", list[0].ComplexName);
      Assert.Equal("9876543215", list[1].ContactNumber);
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

      Guid cId = Guid.NewGuid();

      Logic.Complex complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };

      string result = await repo.CreateComplexAsync(complex);

      Logic.Complex read = await repo.ReadComplexAsync(cId);

      Assert.Equal(cId, read.ComplexId);
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

      Guid cId = Guid.NewGuid();
      Logic.Complex complex = new Logic.Complex
      {
        ComplexId = cId,
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "Liv+",
        ContactNumber = "(123)456-7890"
      };
      await repo.CreateComplexAsync(complex);

      Guid cId2 = Guid.NewGuid();
      Logic.Complex update = new Logic.Complex
      {
        ComplexId = cId2,
        ComplexName = "Liv++",
        ContactNumber = "7894561231"
      };

      string result = await repo.UpdateComplexAsync(update);
      result = testContext.Complex.Find(cId2).ComplexName;
      string phone = testContext.Complex.Find(cId2).ContactNumber;

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

      Guid cId1 = Guid.NewGuid();

      Logic.Complex c1 = new Logic.Complex
      {
        ComplexId = cId1,
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "Liv+",
        ContactNumber = "1234567890"
      };

      Logic.Complex c2 = new Logic.Complex
      {
        ComplexId = Guid.NewGuid(),
        AddressId = Guid.NewGuid(),
        ProviderId = Guid.NewGuid(),
        ComplexName = "SampleComplex",
        ContactNumber = "9876543215"
      };
      string r1 = await repo.CreateComplexAsync(c1);
      string r2 = await repo.CreateComplexAsync(c2);

      string result = repo.DeleteComplex(cId1);

      result = testContext.Complex.First().ComplexName;
      string phone = testContext.Complex.First().ContactNumber;

      Assert.Equal("SampleComplex", result);
      Assert.Equal("9876543215", phone);
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

      Guid rId = Guid.NewGuid();

      Logic.AmenityRoom ar = new Logic.AmenityRoom
      {
        AmenityRoomId = 4,
        RoomId = rId,
        AmenityId = 1
      };

      string result = await repo.CreateAmenityRoomAsync(ar);
      int check = testContext.AmenityRoom.Last().AmenityRoomId;

      Assert.Equal(4, check);
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

      Guid cId = Guid.NewGuid();

      Logic.AmenityComplex ac = new Logic.AmenityComplex
      {
        AmenityComplexId = 4,
        ComplexId = cId,
        AmenityId = 1
      };

      string result = await repo.CreateAmenityComplexAsync(ac);
      int check = testContext.AmenityComplex.Last().AmenityComplexId;

      Assert.Equal(4, check);
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

      Logic.Amenity amenity = new Logic.Amenity
      {
        AmenityId = 4,
        AmenityType = "Fridge",
        Description = "frozen"
      };

      string result = await repo.CreateAmenityAsync(amenity);

      int check = testContext.Amenity.Last().AmenityId;

      Assert.Equal(4, check);
    }

    [Fact]
    public void ReadAmenityListTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      List<Logic.Amenity> am = repo.ReadAmenityList().ToList();

      Assert.Equal(1, am[0].AmenityId);
      Assert.Equal("Test2", am[1].AmenityType);
    }

    [Fact]
    public void ReadAmenityListByComplexIdTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListByComplexIdTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      Guid cId = testContext.Complex.First().ComplexId;

      List<Logic.Amenity> am = repo.ReadAmenityListByComplexId(cId).ToList();

      Assert.Equal("Test1", am[0].AmenityType);
      Assert.Equal("Test2", am[1].AmenityType);
    }

    [Fact]
    public void ReadAmenityListByRoomIdTest()
    {
      Mapper mapper = new Mapper();
      DbContextOptions<Entity.ComplexDbContext> options
          = new DbContextOptionsBuilder<Entity.ComplexDbContext>()
              .UseInMemoryDatabase("ReadAmenityListByComplexIdTest")
              .Options;
      using Entity.ComplexDbContext testContext = new Entity.ComplexDbContext(options);
      Repository repo = new Repository(testContext, mapper);

      Guid rId = testContext.AmenityRoom.First().RoomId;

      List<Logic.Amenity> am = repo.ReadAmenityListByRoomId(rId).ToList();

      Assert.Equal("Test3", am[0].AmenityType);
      Assert.Equal("Test2", am[1].AmenityType);
    }
  }
}
