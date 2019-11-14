using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xyz.Provider.DataAccess.Repository;

namespace Xyz.Provider.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in RoomRepository class
  /// </summary>
  public class AddressRepositoryTests
  {
    /// <summary>
    /// Checks that the constructor can construct a new AddressRepository object
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (initialize Db Context Options)
      var options = TestDbInitializer.InitializeDbOptions("TestConstructor");
      using var db = TestDbInitializer.CreateTestDb(options);
      // act (test the constructor to see if it works)
      var repo = new AddressRepository(db);
      // assert (test passes if no exception thrown)
    }

    /// <summary>
    /// Checks that GetAsync returns Address entity w/ given ID
    /// </summary>
    [Fact]
    public async Task GetShouldGetId()
    {
      // arrange (Initialize Db and repo)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAddressById");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);
      // act (Get the first address in the Db)
      var addr = await repo.GetAsync(1);
      // assert (ensure the address isn't null)
      Assert.NotNull(addr);
    }

    /// <summary>
    /// Checks that GetAllAsync() returns all Address records from DB
    /// </summary>
    [Fact]
    public async Task GetShouldGetAll()
    {
      // arrange (initialize Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAllAddresses");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);
      // act (Get all addresses in the Db)
      var addr = await repo.GetAllAsync();
      // assert (ensure the list isn't null or empty)
      Assert.NotNull(addr);
      Assert.NotEmpty(addr);
    }

    /// <summary>
    /// Checks that AddAsync creates a new Address record in DB
    /// </summary>
    [Fact]
    public async Task AddShouldAdd()
    {
      // arrange (initialize Db, repo and create an address)
      var options = TestDbInitializer.InitializeDbOptions("TestAdd");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);
      var addr = new Lib.Models.Address
      {
        AddressId = 5,
        StreetAddress = "Abc 123 Ln.",
        State = "AL",
        City = "Somewhere",
        Zip = "92816"
      };

      // act (add address to the Db)
      await repo.AddAsync(addr, 1);

      // assert (ensure that the address was added successfully)
      var check = await repo.GetAsync(5);
      Assert.NotNull(check);
      Assert.Equal(addr.AddressId, check.AddressId);
      Assert.Equal(addr.City, check.City);
      Assert.Equal(addr.State, check.State);
      Assert.Equal(addr.StreetAddress, check.StreetAddress);
      Assert.Equal(addr.Zip, check.Zip);
      var list = await repo.GetAllAsync();
      Assert.NotNull(list);
      Assert.NotEmpty(list);
      Assert.Equal(5, list.Count());
    }

    /// <summary>
    /// Checks that GetAddressesByComplexIdAsync returns all/only addresses
    /// associated w/ given complexId
    /// </summary>
    [Fact]
    public async Task GetByComplexShouldGet()
    {
      // arrange (initialize Db and repo)
      var options = TestDbInitializer.InitializeDbOptions("TestGetByComplex");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);

      // act (get the addresses associated by a complex Id)
      var check = await repo.GetAddressesByComplexIdAsync(1);
      // assert (ensure the list isn't null
      Assert.NotNull(check);
    }

    /// <summary>
    /// Checks that GetAddressesByProviderIdAsync returns all/only addresses
    /// associated w/ given providerId
    /// </summary>
    [Fact]
    public async Task GetByProviderIdShouldGet()
    {
      // arrange (initialize Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAddressByProviderId");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);

      // act (get all addresses associated with a provider)
      var check = await repo.GetAddressesByProviderIdAsync(1);
      // assert (ensure the list isn't null)
      Assert.NotNull(check);
    }

    /// <summary>
    /// Checks that RemoveAsync removes Address record w/ given ID from DB
    /// </summary>
    [Fact]
    public async Task RemoveShouldRemove()
    {
      // arrange (initialize Db and Repo)
      var options = TestDbInitializer.InitializeDbOptions("TestRemoveAddress");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);

      // act (attempt to remove an address from the Db)
      await repo.RemoveAsync(2, 1);

      // assert (attempt to get the removed address.  This should throw an ArgumentException)
      async Task GetRemovedAsync() => await repo.GetAsync(2);
      await Assert.ThrowsAnyAsync<ArgumentException>(GetRemovedAsync);
    }

    /// <summary>
    /// Checks that UpdateAsync changes relevant fields of DB record
    /// </summary>
    [Fact]
    public async Task UpdateShouldUpdate()
    {
      // arrange (initialize Db, repo, and an address)
      var options = TestDbInitializer.InitializeDbOptions("TestUpdateAddress");
      using var db = TestDbInitializer.CreateTestDb(options);
      var repo = new AddressRepository(db);
      var addr = new Lib.Models.Address
      {
        AddressId = 1,
        StreetAddress = "Abc 123 Ln.",
        State = "AL",
        City = "Somewhere",
        Zip = "92816"
      };

      // act (update address at id 1)
      await repo.UpdateAsync(addr, 1);

      // assert (get the address from the Db and compare it)
      var check = await repo.GetAsync(1);
      Assert.NotNull(check);
      Assert.Equal(addr.AddressId, check.AddressId);
      Assert.Equal(addr.City, check.City);
      Assert.Equal(addr.State, check.State);
      Assert.Equal(addr.StreetAddress, check.StreetAddress);
      Assert.Equal(addr.Zip, check.Zip);
    }
  }
}
