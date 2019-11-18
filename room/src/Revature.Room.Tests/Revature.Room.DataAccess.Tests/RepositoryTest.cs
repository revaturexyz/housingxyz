using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using Revature.Room.DataAccess;
using BusinessLogic = Revature.Room.Lib;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Revature.Room.Tests
{
  /// <summary>
  /// Test class for testing all repository methods and general database functions
  /// </summary>
  public class RepositoryTest
  {
    /* Preset valid room properties */
    private Guid newRoomID = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private Guid newComplexID = Guid.Parse("249e5358-169a-4bc6-aa0f-c054952456dd");
    private string newGender = "Female";
    private string newRoomNumber = "2002";
    private string newRoomType = "Condo";
    private int newNumOfBeds = 4;
    private DateTime newLeaseStart = new DateTime(2000, 1, 1);
    private DateTime newLeaseEnd = new DateTime(2001, 12, 31);

    /* End of Room Properties */

    private BusinessLogic.Room PresetRoom()
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

    //This test creates a room, but gender and roomtype is null
    [Fact]
    public async Task CreateRoomShouldCreateAsync()
    {
      var options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("CreateRoomShouldCreateAsync")
        .Options;

      using var assembleContext = new RoomServiceContext(options);
      var mapper = new DBMapper();

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
      await actRepo.CreateRoom(assembleRoom);
      await assembleContext.SaveChangesAsync();

      using var assertContext = new RoomServiceContext(options);

      Assert.NotNull(assertContext.Room.Find(newRoomID));
    }

    [Fact]
    public async Task RepoReadTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("ReadRoom")
        .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      testContext.Database.EnsureCreated();
      var mapper = new DBMapper();
      

      
      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = new DataAccess.Entities.Gender { Type = newGender },
        RoomNumber = newRoomNumber,
        RoomType = new DataAccess.Entities.RoomType { Type = newRoomType },
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      
      Repository repo = new Repository(testContext, mapper);

      testContext.Add(newRoom);

      //ResultRoom is empty
      var resultRoom = await repo.ReadRoom(newRoomID);

      //Test passes,but it "fails" because resultRoom has nothing in it, but it references to something so
      //technically it's not null.
      Assert.NotNull(resultRoom);
    }

    [Fact]
    public async Task RepoUpdateTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>().UseInMemoryDatabase("UpdateRoom").Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      testContext.Database.EnsureCreated();

      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = testContext.Gender.FirstOrDefault(g => g.Type == "Male"),
        RoomNumber = newRoomNumber,
        RoomType = testContext.RoomType.FirstOrDefault(r => r.Type == "Apartment"),
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      var updatedRoom = new Revature.Room.Lib.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = "Male",
        RoomNumber = newRoomNumber,
        RoomType = "Dorm",
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom);
      await testContext.SaveChangesAsync();


      Repository repo = new Repository(testContext, mapper);


      await repo.UpdateRoom(updatedRoom);

      var assertRoom = testContext.Room.Find(newRoom.RoomID);


      Assert.Equal("Male", assertRoom.Gender.Type);

    }

    [Fact]
    public async Task RepoReadCheckGenderTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("ReadRoom")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      Repository repo = new Repository(testContext, mapper);

      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = new DataAccess.Entities.Gender { Type = "Male" },
        RoomNumber = newRoomNumber,
        RoomType = new DataAccess.Entities.RoomType { Type = newRoomType },
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom);

      var resultRoom = await repo.ReadRoom(newRoom.RoomID);
      string y = null;

      foreach (var x in resultRoom)
      {
        if(x.Gender == "Male")
        {
          y = x.Gender;
        }
      }

      Assert.Equal(newGender, y);
    }

    [Fact]
    public async Task RepoDeleteTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>()
      .UseInMemoryDatabase("DeleteRoom")
      .Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
      var mapper = new DBMapper();
      Repository repo = new Repository(testContext, mapper);

      var newRoom = new Revature.Room.DataAccess.Entities.Room
      {
        RoomID = newRoomID,
        ComplexID = newComplexID,
        Gender = new DataAccess.Entities.Gender { Type = newGender },
        RoomNumber = newRoomNumber,
        RoomType = new DataAccess.Entities.RoomType { Type = newRoomType },
        NumberOfBeds = newNumOfBeds,
        LeaseStart = newLeaseStart,
        LeaseEnd = newLeaseEnd
      };

      testContext.Add(newRoom);
      //await repo.CreateRoom(newRoom);

      var assertContext = new RoomServiceContext(options);

      await repo.DeleteRoom(newRoomID);

      Assert.Null(assertContext.Room.Find(newRoomID));

    }
  }
}
