using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess.Entities;
using Revature.Room.DataAccess;
using BusinessLogic = Revature.Room.Lib;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using System;

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
    [Fact]
    public async Task CreateRoomShouldCreateAsync()
    {
      var options = new DbContextOptionsBuilder<RoomServiceContext>()
        .UseInMemoryDatabase("CreateRoomShouldCreateAsync")
        .Options;

      var assembleContext = new RoomServiceContext(options);
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

      var assertContext = new RoomServiceContext(options);

      Assert.NotNull(assertContext.Room.Find(newRoomID));
    }
  }
}
