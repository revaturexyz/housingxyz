using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xyz.Provider.DataAccess;
using Xyz.Provider.DataAccess.Repository;

namespace Xyz.Provider.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in room repository
  /// </summary>
  public class RoomRepositoryTests
  {
    /// <summary>
    /// Checks that AddAsync increases the number of rooms in the db by 1
    /// </summary>
    [Fact]
    public async Task AddShouldAddRoom()
    {
      // Arrange (db context options and variable declaration)
      var options = TestDbInitializer.InitializeDbOptions("TestAddRoom");
      int oldCount, maxId;
      Lib.Models.Room newRoom;

      // Arrange (create DbContext and set variable values)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldCount = context.Room.Count();
        maxId = context.Room.Max(r => r.RoomId);
        var provider = context.Provider.Include(p => p.Complex).FirstOrDefault(p => p.ProviderId == 1);
        var complex = provider.Complex.First();
        newRoom = new Lib.Models.Room
        {
          RoomId = maxId + 1,
          Address = Mapper.Map(context.Address.Find(2)),
          Complex = Mapper.Map(complex),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 3,
          NumberOfOccupants = 2,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }
      // Act (Perform function with repository)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        newRoom = await repo.AddAsync(newRoom, newRoom.Complex.Provider.ProviderId);
      }
      // Assert (ensure that the function succeeded)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        Assert.Equal(maxId + 1, newRoom.RoomId);
        Assert.Equal(oldCount + 1, context.Room.Count());
        Assert.Contains(context.Room, r => r.RoomId == maxId + 1);
      }
    }

    /// <summary>
    /// Checks that AddRoomWithAddressAsync adds both room and address to db
    /// </summary>
    [Fact]
    public async Task AddWithAddressShouldAddNewAddress()
    {
      // Arrange (create DbContext and set variable values)
      int maxRoomId, maxAddressId, oldRoomCount, oldAddressCount;
      Lib.Models.Room newRoom;
      Lib.Models.Address newAddress;
      var options = TestDbInitializer.InitializeDbOptions("TestAddWithAddress");
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        maxRoomId = context.Room.Max(r => r.RoomId);
        maxAddressId = context.Address.Max(a => a.AddressId);
        oldRoomCount = context.Room.Count();
        oldAddressCount = context.Address.Count();
        newAddress = new Lib.Models.Address
        {
          AddressId = maxAddressId + 1,
          StreetAddress = "420 High St.",
          City = "Arlington",
          State = "TX",
          Zip = "12345"
        };
        newRoom = new Lib.Models.Room
        {
          RoomId = maxRoomId + 1,
          Address = newAddress,
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Address)
              .Include(c => c.Provider)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 3,
          NumberOfOccupants = 2,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }
      // Act (run the repository function)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        newRoom = await repo.AddRoomWithAddressAsync(newAddress, newRoom, 1);
      }
      // Assert (ensure the function succeeded)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        var rooms = context.Room.Where(r => r.AddressId == newAddress.AddressId);
        Assert.Single(rooms);
      }
    }

    /// <summary>
    /// Checks that GetAsync with an ID returns a room from the db with the correct ID
    /// </summary>
    [Fact]
    public async Task GetByIdShouldGetARoom()
    {
      // Arrange (nothing now)
      var options = TestDbInitializer.InitializeDbOptions("TestGetByRoomId");

      // Act (create DbContext and run Repository function)
      Lib.Models.Room room;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        room = await repo.GetAsync(1);
      }

      // Assert (ensure function succeeded)
      Assert.NotNull(room);
      Assert.Equal(1, room.RoomId);
    }

    /// <summary>
    /// Checks that RemoveAsync removes the given room from db
    /// </summary>
    [Fact]
    public async Task RemoveShouldRemoveARoom()
    {
      // Arrange (add a room to be removed in DbContext)
      var options = TestDbInitializer.InitializeDbOptions("TestRemoveRoom");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = context.Room.Max(r => r.RoomId) + 1,
          Address = Mapper.Map(context.Address.Find(4)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 3,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
        var repo = new RoomRepository(context);
        await repo.AddAsync(newRoom, 1);
      }
      // Act (remove the room we just added)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        await repo.RemoveAsync(newRoom.RoomId, 1);
      }
      // Assert (ensure that the room doesn't exist without doing anything stupid to the DbContext)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        var room2 = await repo.GetAsync(1);
        Assert.NotNull(room2);
        await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.GetAsync(newRoom.RoomId));
      }
    }

    /// <summary>
    /// Checks that UpdateAsync changes the relevant fields of a room
    /// </summary>
    [Fact]
    public async Task UpdateShouldUpdateRoom()
    {
      // Arrange (add a room to be updated)
      var options = TestDbInitializer.InitializeDbOptions("TestUpdateRoom");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = context.Room.Max(r => r.RoomId) + 1,
          Address = Mapper.Map(context.Address.Find(4)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = DateTime.Now.AddYears(1),
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
        var repo = new RoomRepository(context);
        await repo.AddAsync(newRoom, 1);
      }
      // Act (change the values of the room and update it)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom.EndDate = null;
        var repo = new RoomRepository(context);
        await repo.UpdateAsync(newRoom, 1);
      }
      // Assert (ensure changes from update persist)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        var room = await repo.GetAsync(newRoom.RoomId);
        Assert.Null(room.EndDate);
      }
    }

    /// <summary>
    /// Checks that GetAllRoomsByComplexIdAsync returns all/only the rooms with a given ComplexId
    /// </summary>
    [Fact]
    public async Task GetByComplexIdShouldGetByComplexId()
    {
      // Arrange (set variables and add a room)
      var options = TestDbInitializer.InitializeDbOptions("TestGetRoomByComplexId");
      int oldCount, maxId;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldCount = context.Room.Where(r => r.ComplexId == 1).Count();
        maxId = context.Room.Max(r => r.RoomId);
        var newRoom = new Lib.Models.Room
        {
          RoomId = maxId + 1,
          Address = Mapper.Map(context.Address.Find(2)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 3,
          NumberOfOccupants = 2,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
        var repo = new RoomRepository(context);
        await repo.AddAsync(newRoom, 1);
      }
      // Act (get all rooms connected to a complex)
      IEnumerable<Lib.Models.Room> rooms;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        rooms = await repo.GetAllRoomsByComplexIdAsync(1);
      }
      // Assert (make sure that room was added, while still getting the list of rooms)
      Assert.Equal(oldCount + 1, rooms.Count());
      Assert.True(rooms.All(r => r.Complex.ComplexId == 1));
    }

    /// <summary>
    /// Checks that calling UpdateAsync w/ an occupied room throws InvalidOperationException
    /// </summary>
    [Fact]
    public async Task UpdateShouldNotUpdateOccupiedRoom()
    {
      // Arrange (add an occupied room to the DbContext)
      var options = TestDbInitializer.InitializeDbOptions("TestUpdateOccupied");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = context.Room.Max(r => r.RoomId) + 1,
          Address = Mapper.Map(context.Address.Find(4)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(2)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 1,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
        var repo = new RoomRepository(context);
        newRoom = await repo.AddAsync(newRoom, 1);
      }
      // Act (try to update an occupied room)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Assert (attempting to update an occupied room should throw an exception)
        await Assert.ThrowsAsync<InvalidOperationException>(() => repo.UpdateAsync(newRoom, newRoom.Complex.Provider.ProviderId));
      }
    }

    /// <summary>
    /// Checks that GetAsync with an invalid RoomId throws ArgumentOutOfRangeException
    /// </summary>
    /// <param name="id">Invalid ID to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task InvalidIdShouldThrowArgumentException(int id)
    {
      // Arrange
      var options = TestDbInitializer.InitializeDbOptions($"TestInvalidRoomId{id}");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new RoomRepository(context);

      // Act (try to get an invalid room by id),
      // Assert (throws Argument Exception if the id cannot be used)
      await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.GetAsync(id));
    }

    /// <summary>
    /// Checks that AddAsync throws ArgumentException
    /// when adding a room with an ID already in the db
    /// </summary>
    [Fact]
    public async Task DuplicateIdShouldThrowException()
    {
      // Arrange (create and add a room)
      var options = TestDbInitializer.InitializeDbOptions("TestDuplicateId");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = 1,
          Address = Mapper.Map(context.Address.Find(2)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }

      // Act (try to add the same room again)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Assert (should throw InvalidOperationException upon failure)
        await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.AddAsync(newRoom, 1));
      }
    }

    /// <summary>
    /// Checks that AddAsync throws ArgumentException
    /// if RoomNumber and AddressId are not unique
    /// </summary>
    [Fact]
    public async Task DuplicateAddressAndRoomNumberShouldThrowException()
    {
      // Arrange (create a room with values already in Db)
      var options = TestDbInitializer.InitializeDbOptions("TestDuplicateAddressAndNumber");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = 1,
          Address = Mapper.Map(context.Address.Find(2)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "1234",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }
      // Act (try to add the room with the duplicate values)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Assert (should throw InvalidOperationException upon failure)
        await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.AddAsync(newRoom, 1));
      }
    }

    /// <summary>
    /// Checks that AddAsync throws ArgumentException
    /// when a room's address does not already exist in db
    /// </summary>
    [Fact]
    public async Task AddShouldRequireExistingAddress()
    {
      // Arrange (create a room with a fake address (one that isn't in the Db))
      var options = TestDbInitializer.InitializeDbOptions("TestAddWithoutAddress");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = 1,
          Address = new Lib.Models.Address
          {
            AddressId = 5,
            StreetAddress = "123 Fake St.",
            City = "Arlington",
            State = "TX",
            Zip = "12345"
          },
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }
      // Act (try to add the room with the fake address to the Db)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Assert (should throw an ArgumentOutOfRange exception upon failure)
        await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.AddAsync(newRoom, 1));
      }
    }

    /// <summary>
    /// Checks that AddAsync throws InvalidOperationException
    /// when the providerId is not recognized
    /// </summary>
    [Fact]
    public async Task AddShouldRejectUnknownProvider()
    {
      // Arrange (create a room with a bad provider)
      var options = TestDbInitializer.InitializeDbOptions("TestInvalidProvider");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = 1,
          Address = Mapper.Map(context.Address.Find(2)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Provider)
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
      }
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Act (try to add room with bad provider into the Db),
        // Assert (should throw an InvalidOperationException upon failure)
        await Assert.ThrowsAsync<InvalidOperationException>(() => repo.AddAsync(newRoom, 2));
      }
    }

    /// <summary>
    /// Checks that AddAsync throws InvalidOperationException
    /// when provider adds to complex they do not own
    /// </summary>
    [Fact]
    public async Task ProviderCannotAddToOtherComplex()
    {
      // Arrange (create a room and change the complex it is using)
      var options = TestDbInitializer.InitializeDbOptions("TestUnauthorizedProvider");
      Lib.Models.Room newRoom;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newRoom = new Lib.Models.Room
        {
          RoomId = context.Room.Max(r => r.RoomId) + 1,
          Address = Mapper.Map(context.Address.Find(4)),
          Complex = Mapper.Map(
            context.Complex
              .Include(c => c.Address)
              .FirstOrDefault(c => c.ComplexId == 1)),
          Gender = Mapper.Map(context.Gender.Find(1)),
          RoomType = Mapper.Map(context.RoomType.Find(1)),
          StartDate = DateTime.Now,
          EndDate = null,
          HasAmenity = false,
          NumberOfBeds = 4,
          NumberOfOccupants = 0,
          RoomNumber = "101",
          Amenities = new List<Lib.Models.Amenity>()
        };
        newRoom.Complex.ComplexId = context.Complex.Max(c => c.ComplexId) + 1;
      }
      // Act (try to add the room to the Db)
      // the provider is not responsible for the complex, this should fail
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new RoomRepository(context);
        // Assert (should throw an InvalidOperationException upon failure)
        await Assert.ThrowsAsync<InvalidOperationException>(() => repo.AddAsync(newRoom, 1));
      }
    }
  }
}
