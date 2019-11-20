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
    private Guid newRoomId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");

    private Guid newRoomId2 = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456de");

    private Guid newComplexId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Female";
    private string newRoomNumber = "2002";
    private string newRoomType = "Dormitory";
    private int newNumOfBeds = 4;

    private int newNumOfOccupants = 2;
    private int newNumOfOccupants2 = 3;

    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);

    private DateTime endDate = new DateTime(2001, 3, 15);

    /* End of Room Properties */

    // Use to set up a valid business logic room
    private BusinessLogic.Room PresetBLRoom()
    {
      return new BusinessLogic.Room
      {
        RoomId = newRoomId,
        ComplexId = newComplexId,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    // Use to set up a valid entity room
    private DataAccess.Entities.Room PresetEntityRoom(RoomServiceContext context)
    {
      return new DataAccess.Entities.Room
      {
        RoomId = newRoomId,
        ComplexId = newComplexId,
        Gender = context.Gender.FirstOrDefault(g => g.Type == newGender),
        RoomNumber = newRoomNumber,
        RoomType = context.RoomType.FirstOrDefault(g => g.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    private DataAccess.Entities.Room PresetEntityRoom2(RoomServiceContext context)
    {
      return new DataAccess.Entities.Room
      {
        RoomId = newRoomId2,
        ComplexId = newComplexId,
        Gender = context.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = context.RoomType.FirstOrDefault(g => g.Type == newRoomType),
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants2,
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

      var assembleRoom = PresetBLRoom();

      var actRepo = new Repository(assembleContext, mapper);
      await actRepo.CreateRoomAsync(assembleRoom);
      await assembleContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);

      Assert.NotNull(assertContext.Room.Find(newRoomId));
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

      var oldRoom = PresetEntityRoom2(testContext);

      var updatedRoom = PresetBLRoom();
      updatedRoom.RoomId = newRoomId2;
      testContext.Add(oldRoom);
      await testContext.SaveChangesAsync();

      using var actContext = new RoomServiceContext(options);

      Repository repo = new Repository(actContext, mapper);

      // Act
      await repo.UpdateRoomAsync(updatedRoom);
      await actContext.SaveChangesAsync();

      var assertRoom = actContext.Room.Find(oldRoom.RoomId);

      Assert.Equal("Female", assertRoom.Gender.Type);
    }

    [Fact]
    public async Task UpdateRoomUpdatesOccupants()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("UpdateRoomUpdatesOccupants")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper(testContext);
      testContext.Database.EnsureCreated();

      var oldRoom = PresetEntityRoom2(testContext);

      var updatedRoom = PresetBLRoom();
      updatedRoom.RoomId = newRoomId2;
      testContext.Add(oldRoom);
      await testContext.SaveChangesAsync();

      using var actContext = new RoomServiceContext(options);

      Repository repo = new Repository(actContext, mapper);

      await repo.UpdateRoomAsync(updatedRoom);
      await actContext.SaveChangesAsync();

      var assertRoom = actContext.Room.Find(oldRoom.RoomId);

      Assert.Equal(newNumOfOccupants, assertRoom.NumberOfOccupants);
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
      await repo.DeleteRoomAsync(newRoomId);
      actContext.SaveChanges();

      var assertContext = new RoomServiceContext(options);

      Assert.Null(assertContext.Room.Find(newRoomId));
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

      var newRoom2 = PresetEntityRoom2(testContext);

      testContext.Add(newRoom1);
      testContext.Add(newRoom2);
      testContext.SaveChanges();

      using var assertContext = new RoomServiceContext(options);
      var repo = new Repository(assertContext, mapper);

      await repo.DeleteRoomAsync(newRoomId2);
      assertContext.SaveChanges();

      Assert.Null(assertContext.Room.Find(newRoomId2));
    }

    [Fact]
    public async Task RepoGetVacantShouldReturnAvailableRoomsBasedOnFilter()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoGetVacantShouldReturnAvailableRoomsBasedOnFilter")
      .Options;

      using RoomServiceContext assembleContext = new RoomServiceContext(options);
      assembleContext.Database.EnsureCreated();
      var mapper = new DBMapper(assembleContext);

      var newRoom = PresetEntityRoom(assembleContext);
      var newRoom2 = PresetEntityRoom2(assembleContext);

      assembleContext.Add(newRoom);
      assembleContext.Add(newRoom2);
      assembleContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      var repo = new Repository(actContext, mapper);

      var filterRoom = await repo.GetVacantFilteredRoomsByGenderandEndDateAsync("Female", endDate);

      var assertContext = new RoomServiceContext(options);

      Assert.NotNull(filterRoom);

      Assert.Equal(newRoomId.ToString(), filterRoom.FirstOrDefault(r => r == newRoomId).ToString());
    }
  }
}
