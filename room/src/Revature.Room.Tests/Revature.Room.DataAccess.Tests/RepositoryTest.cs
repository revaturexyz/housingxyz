using Microsoft.EntityFrameworkCore;
using Revature.Room.DataAccess;
using Revature.Room.DataAccess.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Room.Tests.Revature.Room.DataAccess.Tests
{
  public class RepositoryTest
  {
    public Lib.Room MockRoom = new Lib.Room()
    {
      RoomID = 1,
      ComplexID = 1,
      Gender = Lib.Gender.Female,
      LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
      LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(756),
      NumberOfBeds = 4,
      RoomNumber = "2428B",
      RoomType = Lib.RoomType.Apartment
    };
    public Lib.Room MockRoom2 = new Lib.Room()
    {
      RoomID = 2,
      ComplexID = 1,
      Gender = Lib.Gender.Male,
      LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
      LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(1503),
      NumberOfBeds = 4,
      RoomNumber = "221B",
      RoomType = Lib.RoomType.Apartment
    };
    public Lib.Room MockRoom3 = new Lib.Room()
    {
      RoomID = 3,
      ComplexID = 2,
      Gender = Lib.Gender.Female,
      LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
      LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(756),
      NumberOfBeds = 4,
      RoomNumber = "2428B",
      RoomType = Lib.RoomType.Apartment
    };
    public Lib.Room MockRoom4 = new Lib.Room()
    {
      RoomID = 4,
      ComplexID = 2,
      Gender = Lib.Gender.Male,
      LeaseEnd = new DateTime(2020, 1, 14, 0, 0, 0, 0, DateTimeKind.Local),
      LeaseStart = new DateTime(2019, 11, 14, 16, 59, 33, 250, DateTimeKind.Local).AddTicks(1503),
      NumberOfBeds = 4,
      RoomNumber = "221B",
      RoomType = Lib.RoomType.Apartment
    };
    [Fact]
    public async Task FilterRoomsShouldFilterByComplexAsync()
    {
      //arrange
      var options = new DbContextOptionsBuilder<RoomServiceContext>()
              .UseInMemoryDatabase("FilterRoomsShouldFilterByComplex")
              .Options;

      using var context = new RoomServiceContext(options);
      var mapper = new DBMapper();
      var repo = new Repository(context, mapper);
      //act
      var result = await repo.GetFilteredRooms(1, null, null, null, null, null);
      //assert
      using var AssertContext = new RoomServiceContext(options);
      mapper = new DBMapper();
      repo = new Repository(context, mapper);
      result = result.ToList();
      Assert.NotNull(result);

    }
  }
}
