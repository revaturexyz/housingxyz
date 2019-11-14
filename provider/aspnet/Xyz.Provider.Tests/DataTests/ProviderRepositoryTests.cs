using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xyz.Provider.DataAccess;
using Xyz.Provider.DataAccess.Repository;

namespace Xyz.Provider.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in ProviderRepository class
  /// </summary>
  public class ProviderRepositoryTests
  {
    /// <summary>
    /// Checks that the constructor constructs a new ProviderRepository
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (initialize DbContextOptions)
      var options = TestDbInitializer.InitializeDbOptions("ConstructorTestDB");
      // act (create constuctor)
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);
      // assert (test passes if no exception thrown)
    }

    /// <summary>
    /// Checks that GetAsync returns a Provider with a valid ID
    /// </summary>
    [Fact]
    public async Task ValidIdShouldGet()
    {
      // Arrange (initialize Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("ValidGetTestDB");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);

      // Act (get a provider)
      var prvdr = await repo.GetAsync(1);

      // Assert (ensure the function got the provider)
      Assert.NotNull(prvdr);
    }

    /// <summary>
    /// Checks that GetAsync throws ArgumentException w/ invalid ID
    /// </summary>
    /// <param name="providerId">Invalid ID to test</param>
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(366359)]
    public async Task InvalidIdShouldNotGet(int providerId)
    {
      // Arrange (create Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions($"InvalidGetTestDB{providerId}");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);
      async Task InvalidProvIdAsync() => await repo.GetAsync(providerId);

      // Act (try to get a provider with a bad Id),
      // Assert (catches any ArgumentException, including exceptions inheriting it)
      await Assert.ThrowsAnyAsync<ArgumentException>(InvalidProvIdAsync);
    }

    /// <summary>
    /// Checks that GetAllAsync returns all Provider records from db
    /// </summary>
    [Fact]
    public async Task GetAllShouldGetAll()
    {
      // Arrange (create Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("GetAllTestDB");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);

      // Act (get all providers in the Db)
      var providers = await repo.GetAllAsync();

      // Assert (ensure the list isn't null)
      Assert.NotNull(providers);
    }

    /// <summary>
    /// Checks that AddAsync inserts a new Provider record to the DB
    /// </summary>
    [Fact]
    public async Task AddShouldAddProvider()
    {
      // Arrange (create DbOptions and initialize and set variables
      var options = TestDbInitializer.InitializeDbOptions("AddTestDB");
      int oldCount, oldMaxId;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldCount = context.Provider.Count();
        oldMaxId = context.Provider.Max(p => p.ProviderId);
      }

      // Act (create and add a new provider)
      Lib.Models.Provider newProvider;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newProvider = new Lib.Models.Provider
        {
          ProviderId = oldMaxId + 1,
          CompanyName = "Liv+ 2",
          ContactNumber = "1234567890",
          Username = "GeneralLiv",
          Password = "Irb@b00n",
          Center = Mapper.Map(context.TrainingCenter.Include(c => c.Address).FirstOrDefault(c => c.CenterId == 1)),
          Address = Mapper.Map(context.Address.FirstOrDefault(i => i.AddressId == 2))
        };
        newProvider.Address.AddressId = context.Address.Max(a => a.AddressId) + 1;

        var repo = new ProviderRepository(context);
        newProvider = await repo.AddAsync(newProvider, 2);
      }

      // Assert (ensure the provider was added)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        Assert.Equal(oldCount + 1, context.Provider.Count());
        var provider2 = context.Provider.FirstOrDefault(p => p.ProviderId == oldMaxId + 1);
        Assert.NotNull(provider2);
        Assert.Equal("Liv+ 2", provider2.CompanyName);
      }
    }

    /// <summary>
    /// Checks that GetAllAsync result includes Provider added to DB at runtime
    /// </summary>
    [Fact]
    public async Task GetAllShouldReturnAllAfterProviderHasBeenAdded()
    {
      // Arrange (create DbOptions and initialize and set variables)
      var options = TestDbInitializer.InitializeDbOptions("GetAllWhenPAddedTestDB");
      int oldCount, oldMaxId;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldCount = context.Provider.Count();
        oldMaxId = context.Provider.Max(p => p.ProviderId);
      }

      // Act (create and add a new provider)
      Lib.Models.Provider newProvider;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newProvider = new Lib.Models.Provider
        {
          ProviderId = oldMaxId + 1,
          CompanyName = "Liv+ 2",
          ContactNumber = "1234567890",
          Username = "GeneralLiv",
          Password = "Irb@b00n",
          Center = Mapper.Map(context.TrainingCenter.Include(c => c.Address).FirstOrDefault(c => c.CenterId == 1)),
          Address = new Lib.Models.Address
          {
            AddressId = context.Address.Max(a => a.AddressId) + 1,
            StreetAddress = "123 Fake St.",
            City = "Berkeley",
            State = "CA",
            Zip = "94704"
          }
        };

        var repo = new ProviderRepository(context);
        newProvider = await repo.AddAsync(newProvider, newProvider.ProviderId);
      }

      // Assert (get all and ensure the list grew by one)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ProviderRepository(context);
        var providers = await repo.GetAllAsync();
        Assert.Equal(oldCount + 1, providers.Count());
      }
    }

    /// <summary>
    /// Checks that UpdateAsync changes the relevant values in Provider DB record
    /// </summary>
    [Fact]
    public async Task UpdateShouldUpdateProvider()
    {
      // Arrange (create and add new provider, while getting old max id to increment into new id)
      var options = TestDbInitializer.InitializeDbOptions("UpdateTestDB");
      Lib.Models.Provider newProvider;
      int oldMaxId;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        oldMaxId = context.Provider.Max(p => p.ProviderId);
        newProvider = new Lib.Models.Provider
        {
          ProviderId = oldMaxId + 1,
          CompanyName = "Liv+ 2",
          ContactNumber = "1234567890",
          Username = "GeneralLiv",
          Password = "Irb@b00n",
          Center = Mapper.Map(context.TrainingCenter.Include(c => c.Address).FirstOrDefault(c => c.CenterId == 1)),
          Address = new Lib.Models.Address
          {
            AddressId = context.Address.Max(a => a.AddressId) + 1,
            StreetAddress = "123 Fake St.",
            City = "Berkeley",
            State = "CA",
            Zip = "94704"
          }
        };
        var repo = new ProviderRepository(context);
        await repo.AddAsync(newProvider, newProvider.ProviderId);
      }
      // Act (update provider we just added after changing some values)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        newProvider.CompanyName = "Liv-";
        newProvider.ContactNumber = "1234567899";
        var repo = new ProviderRepository(context);
        await repo.UpdateAsync(newProvider, newProvider.ProviderId);
      }
      // Assert (ensure that values persist after update)
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var repo = new ProviderRepository(context);
        var provider = await repo.GetAsync(newProvider.ProviderId);
        Assert.NotNull(provider);
        Assert.Equal("Liv-", provider.CompanyName);
        Assert.Equal("1234567899", provider.ContactNumber);
      }
    }

    /// <summary>
    /// Checks that GetTrainingCenterByProviderIdAsync returns
    /// the TrainingCenter associated with the relevant Provider
    /// </summary>
    [Fact]
    public async Task ValidGetProviderByTrainingCenterIdShouldGetProvider()
    {
      // Arrange (create Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("GetProviderByTrainingCenterIdTestDB");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);

      // Act (get all providers in the Db)
      var trainingCenter = await repo.GetTrainingCenterByProviderIdAsync(1);

      // Assert (ensure the list isn't null)
      Assert.NotNull(trainingCenter);
    }

    /// <summary>
    /// Checks that GetTrainingCenterByProviderId throws ArgumentException
    /// when given invalid providerId
    /// </summary>
    /// <param name="val">Invalid providerId to test</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task InvalidGetProviderByTrainingCenterIdShouldGetProvider(int val)
    {
      // Arrange (create Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("GetProviderByTrainingCenterIdTestDB");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);
      async Task InvalidProvIdAsync() => await repo.GetTrainingCenterByProviderIdAsync(val);

      // Act (get all providers in the Db),
      // Assert (catches any ArgumentException, including exceptions inheriting it)
      await Assert.ThrowsAnyAsync<ArgumentException>(InvalidProvIdAsync);
    }

    /// <summary>
    /// Checks that GetAllTrainingCentersAsync returns all TrainingCenter records from DB
    /// </summary>
    [Fact]
    public async Task GetAllTrainingCentersShouldReturnAllTrainingCenters()
    {
      // Arrange (create Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("GetAllTestDB");
      using var context = TestDbInitializer.CreateTestDb(options);
      var repo = new ProviderRepository(context);

      // Act (get all providers in the Db)
      var trainingCenters = await repo.GetAllTrainingCentersAsync();

      // Assert (ensure the list isn't null)
      Assert.NotNull(trainingCenters);
    }
  }
}
