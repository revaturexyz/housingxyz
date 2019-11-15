using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Xunit;
using Revature.Room.DataAccess;
using System.Threading.Tasks;
using Revature.Room.DataAccess.Entities;
using Lib = Revature.Room.Lib;
using System.Linq;

namespace Revature.Room.Tests
{
  public class UnitTest
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

    //[Fact]
    //public async Task RepoReadTest()
    //{
    //  DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>().UseInMemoryDatabase("ReadRoom").Options;

    //  using RoomServiceContext testContext = new RoomServiceContext(options);

    //  var newRoom = new Lib.Room
    //  {

    //  };

    //  testContext.Add();
    //  //Repository repo = new Repository(testContext);

    //  await repo.CreateRoom(null);

    //  Revature.Room.DataAccess.Entities.Room room = testContext.Room.Select(r => r).First();



    //  Assert.NotNull(room);
    //}

    [Fact]
    public async Task RepoUpdateTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>().UseInMemoryDatabase("UpdateRoom").Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);
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


      Repository repo = new Repository(testContext, mapper);

      
      await repo.UpdateRoom(updatedRoom);

       var assertRoom = testContext.Room.Find(newRoom.RoomID);


      Assert.Equal("Male", assertRoom.Gender.Type);

    }

  }
}
