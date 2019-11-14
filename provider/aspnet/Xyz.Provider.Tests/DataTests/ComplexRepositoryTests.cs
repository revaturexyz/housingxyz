using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xyz.Provider.DataAccess;
using Xyz.Provider.DataAccess.Entities;
using Xyz.Provider.DataAccess.Repository;

namespace Xyz.Provider.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in ComplexRepository class
  /// </summary>
  public class ComplexRepositoryTests
  {
    /// <summary>
    /// Checks that GetComplexesByProviderIdAsync returns all/only complices w/ given providerId
    /// </summary>
    [Fact]
    public async Task GetByProviderIdShouldGetByProviderId()
    {
      // Arrange (initialize db and add new complex)
      var options = TestDbInitializer.InitializeDbOptions("TestGetComplexByProviderId");
      int oldCount;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldCount = context.Complex.Where(c => c.ProviderId == 1).Count();
        var newAddress = new Address
        {
          AddressId = 5,
          StreetAddress = "123 Fake St.",
          City = "Berkeley",
          State = "CA",
          Zip = "94704"
        };
        context.Address.Add(newAddress);
        context.SaveChanges();
        var newComplex = new Complex
        {
          AddressId = 5,
          ComplexId = context.Complex.Max(c => c.ComplexId) + 1,
          ComplexName = "Liv+ 2",
          ContactNumber = "1234567890",
          ProviderId = 1
        };
        context.Complex.Add(newComplex);
        context.SaveChanges();
      }
      // Act (get the list of complexes with the same provider id)
      IEnumerable<Lib.Models.Complex> complexes;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ComplexRepository(context);
        complexes = await repo.GetComplexesByProviderIdAsync(1);
      }
      // Assert (ensure that our new complex was added successfully)
      Assert.Equal(oldCount + 1, complexes.Count());
      Assert.True(complexes.All(c => c.Provider.ProviderId == 1));
    }

    /// <summary>
    /// Checks that GetComplexesByProviderIdAsync throws ArgumentException w/ invalid providerId
    /// </summary>
    /// <param name="providerId">Invalid providerId to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task GetByInvalidProviderIdShouldThrowArgumentException(int providerId)
    {
      // Arrange (create db context options)
      var options = TestDbInitializer.InitializeDbOptions($"TestGetByInvalidProviderId{providerId}");

      // Act (try to get a list of complexes by an invalid provider id)
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ComplexRepository(context);
      async Task InvalidProviderIdAsync() => await repo.GetComplexesByProviderIdAsync(providerId);
      // Assert (it should throw any ArgumentException when it fails)
      await Assert.ThrowsAnyAsync<ArgumentException>(InvalidProviderIdAsync);
    }

    /// <summary>
    /// Checks that GetComplexesByProviderIdAsync returns empty sequence
    /// When given a valid providerId with no associated complices in DB
    /// </summary>
    [Fact]
    public async Task ProviderWithNoComplexesShouldReturnEmpty()
    {
      // Arrange (initialize Db and add a provider with no complexes)
      var options = TestDbInitializer.InitializeDbOptions("TestProviderWithoutComplexes");
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var newProvider = new DataAccess.Entities.Provider
        {
          ProviderId = context.Complex.Max(c => c.ProviderId) + 1,
          CenterId = 1,
        };
        context.Provider.Add(newProvider);
        context.SaveChanges();
      }
      // Act (Get the 'list' of complexes under the provider we just added)
      IEnumerable<Lib.Models.Complex> complexes;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ComplexRepository(context);
        complexes = await repo.GetComplexesByProviderIdAsync(context.Provider.Max(p => p.ProviderId));
      }
      // Assert (ensure that what we got is actually empty)
      Assert.NotNull(complexes);
      Assert.Empty(complexes);
    }

    /// <summary>
    /// Checks that AddAsync creates a new Complex record in DB
    /// </summary>
    [Fact]
    public async Task AddShouldAddAComplex()
    {
      // Arrange (initialize Db options context)
      var options = TestDbInitializer.InitializeDbOptions("TestAddComplex");
      // Act (create and add a complex completely)
      int oldCount, oldMaxId;
      Lib.Models.Complex newComplex;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldMaxId = context.Complex.Max(c => c.ComplexId);
        newComplex = new Lib.Models.Complex
        {
          Address = new Lib.Models.Address
          {
            AddressId = context.Address.Max(a => a.AddressId) + 1,
            StreetAddress = "123 Fake St.",
            City = "Berkeley",
            State = "CA",
            Zip = "94704"
          },
          ComplexId = oldMaxId + 1,
          ComplexName = "Liv+ 2",
          ContactNumber = "1234567890",
          Provider = Mapper.Map(context.Provider
                                        .Include(p => p.Center).ThenInclude(c => c.Address)
                                        .Include(p => p.Address)
                                        .FirstOrDefault(p => p.ProviderId == 1)),
          Center = Mapper.Map(context.TrainingCenter.FirstOrDefault(t => t.CenterId == 1)),
          Rooms = new List<Lib.Models.Room>()
        };
        oldCount = context.Complex.Count();
        var repo = new ComplexRepository(context);
        newComplex = await repo.AddAsync(newComplex, 1);
      }
      // Assert (ensure that complex is in the database)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        Assert.Equal(oldCount + 1, context.Complex.Count());
        var complex2 = context.Complex.FirstOrDefault(c => c.ComplexId == oldMaxId + 1);
        Assert.NotNull(complex2);
        Assert.Equal("Liv+ 2", complex2.ComplexName);
      }
    }

    /// <summary>
    /// Checks that AddAsync w/ invalid providerId throws ArgumentException
    /// </summary>
    /// <param name="providerId">Invalid providerId to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task AddWithInvalidProviderIdShouldThrowException(int providerId)
    {
      // Arrange (Create Db Options Context)
      var options = TestDbInitializer.InitializeDbOptions($"TestAddWithInvalidProvider{providerId}");
      // Act (Create a complex, but with a bad provider)
      using var context = TestDbInitializer.CreateTestDb(options);
      var newComplex = new Lib.Models.Complex
      {
        Address = Mapper.Map(context.Address.Find(2)),
        ComplexId = 2,
        ComplexName = "Liv+ 2",
        ContactNumber = "1234567890",
        Provider = Mapper.Map(
          context.Provider
            .Include(p => p.Center)
              .ThenInclude(c => c.Address)
            .Include(p => p.Address)
            .FirstOrDefault(p => p.ProviderId == 1)),
        Center = Mapper.Map(context.TrainingCenter.Find(1)),
        Rooms = new List<Lib.Models.Room>()
      };
      var repo = new ComplexRepository(context);
      // Assert (Function should throw an argument exception upon failure)
      await Assert.ThrowsAnyAsync<ArgumentException>(() => repo.AddAsync(newComplex, providerId));
    }

    /// <summary>
    /// Checks that AddAsync throws InvalidOperationException
    /// given Complex w/ AddressId of another Complex
    /// </summary>
    [Fact]
    public async Task AddShouldRejectComplexWithDuplicateAddress()
    {
      // Arrange (Initialize Db Context Options)
      var options = TestDbInitializer.InitializeDbOptions("TestAddWithDuplicateAddress");
      // Act (Create a new complex, with an address already in the database)
      using var context = TestDbInitializer.CreateTestDb(options);
      var newComplex = new Lib.Models.Complex
      {
        Address = Mapper.Map(context.Address.Find(1)),
        ComplexId = 2,
        ComplexName = "Liv+ 2",
        ContactNumber = "1234567890",
        Provider = Mapper.Map(
          context.Provider
            .Include(p => p.Center)
              .ThenInclude(c => c.Address)
            .Include(p => p.Address)
            .FirstOrDefault(p => p.ProviderId == 1)),
        Center = Mapper.Map(context.TrainingCenter.Find(1)),
        Rooms = new List<Lib.Models.Room>()
      };
      var repo = new ComplexRepository(context);
      // Assert (Should throw an InvalidOperationException upon failure)
      await Assert.ThrowsAsync<InvalidOperationException>(() => repo.AddAsync(newComplex, 1));
    }

    /// <summary>
    /// Checks that GetAsync gets a Complex record w/ given ID from DB
    /// </summary>
    [Fact]
    public async Task GetByIdShouldGetComplexById()
    {
      // Arrange (Create Db Context Options)
      var options = TestDbInitializer.InitializeDbOptions("TestGetComplexByComplexId");
      // Act (Get a complex by its id)
      Lib.Models.Complex complex;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ComplexRepository(context);
        complex = await repo.GetAsync(1);
      }
      // Assert (ensure we have a complex and not null)
      Assert.NotNull(complex);
      Assert.Equal(1, complex.ComplexId);
    }

    /// <summary>
    /// Checks that GetAsync throws ArgumentException given invalid complexId
    /// </summary>
    /// <param name="complexId">Invalid complexId to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task InvalidIdShouldThrowArgumentException(int complexId)
    {
      // Arrange (Initialize Db Context Options)
      var options = TestDbInitializer.InitializeDbOptions($"TestGetComplexByInvalidComplexId{complexId}");
      // Act (Try to get a complex with a bad id)
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ComplexRepository(context);
      async Task InvalidComplexIdAsync() => await repo.GetAsync(complexId);
      // Assert (Should throw an ArgumentException upon failure)
      await Assert.ThrowsAnyAsync<ArgumentException>(InvalidComplexIdAsync);
    }

    /// <summary>
    /// Checks that GetAmenitiesByComplexIdAsync throws ArgumentException
    /// given invalid complexId
    /// </summary>
    /// <param name="complexId">Invalid complexId to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task AmenitiesForInvalidIdShouldThrowArgumentException(int complexId)
    {
      // Arrange (Initialize Db Options Context)
      var options = TestDbInitializer.InitializeDbOptions($"TestGetAmenitiesByInvalidComplexId{complexId}");
      // Act (Attempt to get a list of amenities in a complex with a bad id)
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ComplexRepository(context);
      async Task InvalidComplexIdAsync() => await repo.GetAmenitiesByComplexIdAsync(complexId);
      // Assert (Should throw an ArgumentException upon failure)
      await Assert.ThrowsAnyAsync<ArgumentException>(InvalidComplexIdAsync);
    }

    /// <summary>
    /// Checks that GetAmenitiesByComplexId returns all/only amenities
    /// belonging to rooms w/ the given complexId
    /// </summary>
    [Fact]
    public async Task GetAmenitiesByComplexIdShouldGetAmenities()
    {
      // Arrange (create Db and complex with amenities.  Add the complex to the Db)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAmenitiesByComplexId");
      Lib.Models.Complex newComplex;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newComplex = new Lib.Models.Complex
        {
          Address = new Lib.Models.Address
          {
            AddressId = context.Address.Max(a => a.AddressId) + 1,
            StreetAddress = "123 Fake St.",
            City = "Berkeley",
            State = "CA",
            Zip = "94704"
          },
          ComplexId = context.Complex.Max(c => c.ComplexId) + 1,
          ComplexName = "Liv+ 2",
          ContactNumber = "1234567890",
          Provider = Mapper.Map(
            context.Provider
              .Include(p => p.Center)
                .ThenInclude(c => c.Address)
              .Include(p => p.Address)
              .FirstOrDefault(p => p.ProviderId == 1)),
          Center = Mapper.Map(context.TrainingCenter.Find(1)),
          Rooms = new List<Lib.Models.Room>()
        };
        context.Address.Add(Mapper.Map(newComplex.Address));
        context.Complex.Add(Mapper.Map(newComplex));
        // Add room to test same amenity in multiple rooms
        var newRoom = new Room
        {
          RoomId = context.Room.Max(r => r.RoomId) + 1,
          RoomNumber = "101",
          StartDate = DateTime.Now,
          EndDate = DateTime.Now.AddYears(1),
          TypeId = 1,
          NumberOfOccupants = 4,
          NumberOfBeds = 4,
          GenderId = 1,
          ComplexId = newComplex.ComplexId,
          AddressId = newComplex.Address.AddressId
        };
        context.Room.Add(newRoom);
        // Assign amenities to rooms in join table
        context.RoomAmenity.Add(new RoomAmenity
        {
          RoomAmenityId = context.RoomAmenity.Max(ra => ra.RoomAmenityId) + 1,
          AmenityId = 2,
          RoomId = newRoom.RoomId
        });
        context.RoomAmenity.Add(new RoomAmenity
        {
          RoomAmenityId = context.RoomAmenity.Max(ra => ra.RoomAmenityId) + 2,
          AmenityId = 1,
          RoomId = newRoom.RoomId
        });
        context.SaveChanges();
      }
      // Act (Get the amenities from the complex we just added)
      IEnumerable<Lib.Models.Amenity> amenities;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ComplexRepository(context);
        amenities = await repo.GetAmenitiesByComplexIdAsync(newComplex.ComplexId);
      }
      // Assert (Ensure the list is occupied with the values we inserted into it)
      Assert.NotNull(amenities);
      Assert.Equal(2, amenities.Count());
      Assert.DoesNotContain(amenities, a => a.AmenityId == 3);
    }

    /// <summary>
    /// Checks that GetAmenitiesByComplexIdAsync returns an empty list
    /// given the complexId of a Complex w/ no amenities
    /// </summary>
    [Fact]
    public async Task ProviderWithNoAmenitiesShouldReturnEmpty()
    {
      // Arrange (create Db and clear it of RoomAmenities)
      var options = TestDbInitializer.InitializeDbOptions("TestProviderWithNoAmenities");
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        context.RoomAmenity.RemoveRange(context.RoomAmenity);
        context.SaveChanges();
      }
      // Act (try to get the amenities a complex has)
      IEnumerable<Lib.Models.Amenity> amenities;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ComplexRepository(context);
        amenities = await repo.GetAmenitiesByComplexIdAsync(1);
      }
      // Assert (ensure that the list isn't null, but it is empty)
      Assert.NotNull(amenities);
      Assert.Empty(amenities);
    }
  }
}
