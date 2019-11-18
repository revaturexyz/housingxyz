using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess;
using Revature.Room.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BusinessLogic = Revature.Room.Lib;

namespace Revature.Room.Tests
{
  /// <summary>
  /// Test class for testing all repository methods and general database functions
  /// </summary>
  public class RepositoryTest
  {
    /* Preset valid room properties */
    private Guid newRoomID = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");

    private Guid newRoomID2 = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456de");

    private Guid newComplexID = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Female";
    private string newRoomNumber = "2002";
    private string newRoomType = "Dormitory";
    private int newNumOfBeds = 4;
    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);

    /* End of Room Properties */

    // Use to set up a valid business logic room
    private BusinessLogic.Room PresetBLRoom()
    {
      return new BusinessLogic.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    // Use to set up a valid entity room
    private DataAccess.Entities.Room PresetEntityRoom(RoomServiceContext context)
    {
      return new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = context.Gender.FirstOrDefault(g => g.Type == newGender),
        RoomNumber = newRoomNumber,
        RoomType = context.RoomType.FirstOrDefault(g => g.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    //This test creates a room, but gender and roomtype is null
    //Should be able be able to fix by setting roomType and Gender to
    //their respective Data entities class objects
    [Fact]
    public async Task CreateRoomShouldCreateAsync()
    {
      var options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("CreateRoomShouldCreateAsync")
        .Options;

      using var assembleContext = new RoomServiceContext(options);
      var mapper = new DBMapper(assembleContext);
      assembleContext.Database.EnsureCreated();

      var assembleRoom = new BusinessLogic.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      var actRepo = new Repository(assembleContext, mapper);
      await actRepo.CreateRoomAsync(assembleRoom);
      await assembleContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);

      Assert.NotNull(assertContext.Room.Find(newRoomID));
    }

    [Fact]
    public async Task ReadRoomShouldReturnRoom()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("ReadRoomShouldReturnRoom")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper(testContext);
      testContext.Database.EnsureCreated();

      var newRoomEntity = PresetEntityRoom(testContext);

      testContext.Add(newRoomEntity);
      await testContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);
      Repository repo = new Repository(assertContext, mapper);

      var resultRoomList = await repo.ReadRoomAsync(newRoomID);
      //Test passes,but it "fails" because resultRoom has nothing in it, but it references to something so
      //technically it's not null.
      //Fixed
      Assert.NotNull(resultRoomList);
      Assert.Equal(newRoomID, resultRoomList.FirstOrDefault().RoomID);
    }


    [Fact]
    public async Task UpdateRoomUpdatesGenderAndRoomType()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("UpdateRoomUpdatesRoomType")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper(testContext);
      testContext.Database.EnsureCreated();

      var oldRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = testContext.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = testContext.RoomType.FirstOrDefault(r => r.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      var updatedRoom = PresetBLRoom();

      testContext.Add(oldRoom);
      await testContext.SaveChangesAsync();

      using var actContext = new RoomServiceContext(options);

      Repository repo = new Repository(actContext, mapper);

      // Act
      await repo.UpdateRoomAsync(updatedRoom);
      await actContext.SaveChangesAsync();

      var assertRoom = actContext.Room.Find(oldRoom.RoomID);

      Assert.Equal("Female", assertRoom.Gender.Type);

    }

    [Fact]
    public async Task RepoReadCheckGenderTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoReadCheckGenderTest")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();
      var mapper = new DBMapper(testContext);

      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = testContext.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = testContext.RoomType.FirstOrDefault(g => g.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom);
      testContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      Repository repo = new Repository(actContext, mapper);

      var resultRoom = await repo.ReadRoomAsync(newRoom.RoomID);

      Assert.Equal("Male", resultRoom.FirstOrDefault().Gender);
    }

    [Fact]
    public async Task RepoReadCheckRoomID()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("RepoReadCheckRoomID")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      var mapper = new DBMapper(testContext);

      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID2,
        ComplexID = newComplexID,
        Gender = testContext.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = testContext.RoomType.FirstOrDefault(g => g.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom);
      testContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      Repository repo = new Repository(actContext, mapper);

      var resultRoom = await repo.ReadRoomAsync(newRoom.RoomID);

      Assert.Equal(newRoomID2.ToString(), resultRoom.FirstOrDefault().RoomID.ToString());
    }

    [Fact]
    public async Task RepoDeleteTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoDeleteTest")
      .Options;

      using RoomServiceContext assembleContext = new RoomServiceContext(options);
      var mapper = new DBMapper(assembleContext);

      var newRoom = PresetEntityRoom(assembleContext);

      assembleContext.Add(newRoom);
      assembleContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      var repo = new Repository(actContext, mapper);
      await repo.DeleteRoomAsync(newRoomID);
      actContext.SaveChanges();

      var assertContext = new RoomServiceContext(options);

      Assert.Null(assertContext.Room.Find(newRoomID));

    }

    //Test where we add two rooms, but delete one to see if it's actually in there
    [Fact]
    public async Task RepoDeleteOneOfTwo()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoDeleteOneOfTwo")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper(testContext);

      var newRoom1 = PresetEntityRoom(testContext);

      var newRoom2 = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID2,
        ComplexID = newComplexID,
        Gender = testContext.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = testContext.RoomType.FirstOrDefault(g => g.Type == "Dormitory"),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom1);
      testContext.Add(newRoom2);
      testContext.SaveChanges();
      //await repo.CreateRoom(newRoom);

      using var assertContext = new RoomServiceContext(options);
      var repo = new Repository(assertContext, mapper); 
      //testContext = new RoomServiceContext(options);

      await repo.DeleteRoomAsync(newRoomID2);
      assertContext.SaveChanges();

      Assert.Null(assertContext.Room.Find(newRoomID2));

      //Assert.
    }

  }
}
