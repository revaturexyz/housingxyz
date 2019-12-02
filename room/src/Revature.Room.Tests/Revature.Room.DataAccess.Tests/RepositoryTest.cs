using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess;
using Revature.Room.DataAccess.Entities;
using System;
using System.Collections.Generic;
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
    /* Preset valid Room properties */
    private Guid newRoomId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");

    private Guid newRoomId2 = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456de");

    private Guid newRoomId3 = Guid.Parse("449e5358-169a-4bc6-aa0f-c054952456df");

    private Guid newComplexId = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Female";
    private string newRoomNumber = "2002";
    private string newRoomType = "Dormitory";
    private int newNumOfBeds = 4;

    private int newNumOfOccupants = 2;
    private int newNumOfOccupants2 = 3;
    private int newNumOfOccupants3 = 4;

    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);

    /* End of Room Properties */

    // Use to set up a valid business logic Room
    private BusinessLogic.Room PresetBLRoom()
    {
      var room = new BusinessLogic.Room
      {
        RoomId = newRoomId,
        ComplexId = newComplexId,
        Gender = newGender,
        RoomNumber = newRoomNumber,
        RoomType = newRoomType,
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants,
      };
      room.SetLease(newLeaseStart, newLeaseEnd);
      return room;
    }

    // Use to set up a valid entity Room
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
        RoomNumber = "2003",
        RoomType = context.RoomType.FirstOrDefault(g => g.Type == "Apartment"),
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants2,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    private DataAccess.Entities.Room PresetEntityRoom3(RoomServiceContext context)
    {
      return new DataAccess.Entities.Room
      {
        RoomId = newRoomId3,
        ComplexId = newComplexId,
        Gender = context.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = "2004",
        RoomType = context.RoomType.FirstOrDefault(g => g.Type == "TownHouse"),
        NumberOfBeds = newNumOfBeds,
        NumberOfOccupants = newNumOfOccupants3,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };
    }

    //This test creates a Room, but gender and roomtype is null
    //Should be able be able to fix by setting roomType and Gender to
    //their respective Data entities class objects
    [Fact]
    public async Task CreateRoomShouldCreateAsync()
    {
      var options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("CreateRoomShouldCreateAsync")
        .Options;

      using var assembleContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      assembleContext.Database.EnsureCreated();

      var assembleRoom = PresetBLRoom();

      var actRepo = new Repository(assembleContext, mapper);
      await actRepo.CreateRoomAsync(assembleRoom);
      await assembleContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);

      Assert.NotNull(assertContext.Room.Find(newRoomId));
    }

    [Fact]
    public async Task ReadRoomShouldReturnRoom()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("ReadRoomShouldReturnRoom")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      testContext.Database.EnsureCreated();

      var newRoomEntity = PresetEntityRoom(testContext);

      testContext.Add(newRoomEntity);
      await testContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);
      Repository repo = new Repository(assertContext, mapper);

      var resultRoom = await repo.ReadRoomAsync(newRoomId);

      Assert.NotNull(resultRoom);
      Assert.Equal(newRoomId, resultRoom.RoomId);
    }

    [Fact]
    public async Task UpdateRoomShouldUpdateLeaseAsync()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
       .UseInMemoryDatabase("UpdateRoomShouldUpdateLease")
       .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      var repo = new Repository(testContext, mapper);
      testContext.Database.EnsureCreated();

      testContext.Room.Add(PresetEntityRoom(testContext));
      await testContext.SaveChangesAsync();
      var room = PresetEntityRoom(testContext);
      var updateRoom = PresetBLRoom();
      updateRoom.SetLease(newLeaseStart.AddDays(3), newLeaseEnd.AddYears(2));
      updateRoom.RoomId = room.RoomId;

      await repo.UpdateRoomAsync(updateRoom);
      await testContext.SaveChangesAsync();

      var result = await testContext.Room.FirstAsync(r => r.RoomId == newRoomId);
      Assert.True(result.LeaseStart == newLeaseStart.AddDays(3));
    }

    [Fact]
    public async Task RepoReadCheckGenderTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoReadCheckGenderTest")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();
      var mapper = new DBMapper();

      var newRoom = PresetEntityRoom2(testContext);

      testContext.Add(newRoom);
      testContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      Repository repo = new Repository(actContext, mapper);

      var resultRoom = await repo.ReadRoomAsync(newRoom.RoomId);

      Assert.Equal("Male", resultRoom.Gender);
    }

    [Fact]
    public async Task RepoReadCheckRoomId()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("RepoReadCheckRoomId")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      var mapper = new DBMapper();

      var newRoom = PresetEntityRoom2(testContext);

      testContext.Add(newRoom);
      testContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      Repository repo = new Repository(actContext, mapper);

      var resultRoom = await repo.ReadRoomAsync(newRoom.RoomId);

      Assert.Equal(newRoomId2.ToString(), resultRoom.RoomId.ToString());
    }

    [Fact]
    public async Task RepoDeleteTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoDeleteTest")
      .Options;

      using RoomServiceContext assembleContext = new RoomServiceContext(options);
      var mapper = new DBMapper();

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
      var mapper = new DBMapper();

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
      var mapper = new DBMapper();

      var newRoom = PresetEntityRoom(assembleContext);
      var newRoom2 = PresetEntityRoom2(assembleContext);

      assembleContext.Add(newRoom);
      assembleContext.Add(newRoom2);
      assembleContext.SaveChanges();

      DateTime endDate = new DateTime(2001, 3, 15);

      using var actContext = new RoomServiceContext(options);
      var repo = new Repository(actContext, mapper);

      var filterRoom = await repo.GetVacantFilteredRoomsByGenderandEndDateAsync("Female", endDate);

      var assertContext = new RoomServiceContext(options);

      Assert.NotNull(filterRoom);

      Assert.Equal(newRoomId.ToString(), filterRoom.FirstOrDefault(r => r.Item1 == newRoomId).Item1.ToString());
    }

    [Fact]
    public async Task RepoGetFilterRoomShouldFilterBasedOnWhatYouGiveIt()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoGetFilterRoomShouldFilterBasedOnWhatYouGiveIt")
      .Options;

      using RoomServiceContext assembleContext = new RoomServiceContext(options);
      assembleContext.Database.EnsureCreated();
      var mapper = new DBMapper();

      var newRoom = PresetEntityRoom(assembleContext);
      var newRoom2 = PresetEntityRoom2(assembleContext);
      var newRoom3 = PresetEntityRoom3(assembleContext);

      assembleContext.Add(newRoom);
      assembleContext.Add(newRoom2);
      assembleContext.Add(newRoom3);
      assembleContext.SaveChanges();

      DateTime endDate = new DateTime(2001, 3, 15);

      using var actContext = new RoomServiceContext(options);
      var repo = new Repository(actContext, mapper);

      //Returns 1 room because there is only 1 room that matches that room number
      var filterRoom1 = await repo.GetFilteredRoomsAsync(newComplexId, newRoomNumber, null, null, null, null, null);
      //Returns 2 rooms because there are 2 rooms that are labeld as "Male" gender
      var filterRoom2 = await repo.GetFilteredRoomsAsync(newComplexId, null, newNumOfBeds, null, "Male", null, null);

      //Returns 1 room because there is only one room labeled as room type TownHouse
      var filterRoom3 = await repo.GetFilteredRoomsAsync(newComplexId, null, newNumOfBeds, "TownHouse", null, null, null);

      //Returns 3 rooms because that is all the rooms witht that complex Id
      var filterRoom4 = await repo.GetFilteredRoomsAsync(newComplexId, null, null, null, null, null, null);

      //Returns 3 rooms because there are 3 rooms with that many number of beds
      var filterRoom5 = await repo.GetFilteredRoomsAsync(newComplexId, null, newNumOfBeds, null, null, null, null);

      //Returns 0 rooms because there are no rooms whose lease are greater than the end date
      var filterRoom6 = await repo.GetFilteredRoomsAsync(newComplexId, null, null, null, null, DateTime.Now, null);

      //Returns 1 room
      var filterRoom7 = await repo.GetFilteredRoomsAsync(newComplexId, null, null, null, null, null, newRoomId);

      Assert.Equal(newRoomNumber, filterRoom1.FirstOrDefault(r => r.RoomNumber == newRoomNumber).RoomNumber);

      Assert.Equal(2, filterRoom2.Count(r => r.Gender == "Male"));

      //Assert.Equal("TownHouse", filterRoom3.FirstOrDefault(r => r.RoomType == "TownHouse").RoomType);

      Assert.Equal(1, filterRoom3.Count(r => r.RoomType == "TownHouse"));

      //Assert.Equal(newComplexId, filterRoom4.FirstOrDefault(r => r.ComplexId == newComplexId).ComplexId);

      Assert.Equal(3, filterRoom4.Count(r => r.ComplexId == newComplexId));

      Assert.Equal(3, filterRoom5.Count(r => r.NumberOfBeds == newNumOfBeds));

      Assert.True(filterRoom6.Count() == 0);

      Assert.True(filterRoom7.Count() == 1);
    }
    [Fact]
    public async Task RepoDeleteComplexRoomShouldDeleteAllRoomsInComplex()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("RepoDeleteComplexRoomShouldDeleteAllRoomsInComplex")
      .Options;

      using RoomServiceContext assembleContext = new RoomServiceContext(options);
      assembleContext.Database.EnsureCreated();
      //var mapper = new DBMapper(assembleContext);

      var mapper = new DBMapper();

      var newRoom = PresetEntityRoom(assembleContext);
      var newRoom2 = PresetEntityRoom2(assembleContext);
      var newRoom3 = PresetEntityRoom3(assembleContext);

      assembleContext.Add(newRoom);
      assembleContext.Add(newRoom2);
      assembleContext.Add(newRoom3);
      assembleContext.SaveChanges();

      using var actContext = new RoomServiceContext(options);
      var repo = new Repository(actContext, mapper);

      var deleteComplexRooms = await repo.DeleteComplexRoomAsync(newComplexId);
      await actContext.SaveChangesAsync();

      var assertContext = new RoomServiceContext(options);

      //The DeleteComplexRoomAsync method deletes all rooms based on the complex, it works
      //But we have 3 seeded room in our database that aren't in the same complex
      //so even after delete all the created rooms
      //we still have our seeded data.
      Assert.Equal(3, assertContext.Room.Count());

    }

    [Fact]
    public async Task AddRoomOccupantsShouldUpdateAsync()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("AddRoomOccupantsShouldUpdate")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      testContext.Room.Add(PresetEntityRoom(testContext));
      await testContext.SaveChangesAsync();
      var mapper = new DBMapper();
      var repo = new Repository(testContext, mapper);

      await repo.AddRoomOccupantsAsync(newRoomId, newGender);

      var result = await testContext.Room.FirstAsync(r => r.RoomId == newRoomId);
      Assert.True(result.NumberOfOccupants == newNumOfOccupants + 1);
    }

    [Fact]
    public async Task AddRoomOccupantsShouldSetGenderAsync()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("AddRoomOccupantsShouldSetGender")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      var room = PresetEntityRoom(testContext);
      room.Gender = null;
      room.NumberOfOccupants = 0;
      testContext.Room.Add(room);
      await testContext.SaveChangesAsync();
      var mapper = new DBMapper();
      var repo = new Repository(testContext, mapper);

      await repo.AddRoomOccupantsAsync(newRoomId, newGender);

      var result = await testContext.Room.Where(r => r.RoomId == newRoomId).Include(r => r.Gender).FirstAsync();
      Assert.True(result.Gender.Type == newGender);
    }

    [Fact]
    public async Task SubtractRoomOccupantsShouldUpdateAsync()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("SubtractRoomOccupantsShouldUpdate")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      testContext.Room.Add(PresetEntityRoom(testContext));
      await testContext.SaveChangesAsync();
      var mapper = new DBMapper();
      var repo = new Repository(testContext, mapper);

      await repo.SubtractRoomOccupantsAsync(newRoomId);

      var result = await testContext.Room.FirstAsync(r => r.RoomId == newRoomId);
      Assert.True(result.NumberOfOccupants == newNumOfOccupants - 1);
    }
    [Fact]
    public async Task SubtractRoomOccupantsShouldSetGenderAsync()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("SubtractRoomOccupantsShouldSetGenderAsync")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();

      var room = PresetEntityRoom(testContext);
      room.Gender = null;
      room.NumberOfOccupants = 1;
      testContext.Room.Add(room);
      await testContext.SaveChangesAsync();
      var mapper = new DBMapper();
      var repo = new Repository(testContext, mapper);

      await repo.SubtractRoomOccupantsAsync(newRoomId);

      var result = await testContext.Room.Where(r => r.RoomId == newRoomId).Include(r => r.Gender).FirstAsync();
      Assert.True(result.Gender == null);
    }
  }
}
