using Microsoft.EntityFrameworkCore;
using Revature.Address.DataAccess.Entities;


namespace Revature.Address.Tests.DataAccess.Tests
{
  /// <summary>
  /// This class handles the creation of the
  /// dbcontext used for testing data access methods
  /// </summary>
  public static class TestDbContext
  {
    /// <summary>
    /// Initializeer for in-memory database for testing
    /// data access functionality
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DbContextOptions<AddressDbContext> TestDbInitalizer(string name)
    {
      var finalDb = new DbContextOptionsBuilder<AddressDbContext>()
      .UseInMemoryDatabase(databaseName: name)
      .Options;
      return finalDb;
    }

    /// <summary>
    /// Creates the testing database
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AddressDbContext CreateTestDb(DbContextOptions<AddressDbContext> options)
    {
      var context = new AddressDbContext(options);
      context.Database.EnsureCreated();
      return context;
    }
  }
}
