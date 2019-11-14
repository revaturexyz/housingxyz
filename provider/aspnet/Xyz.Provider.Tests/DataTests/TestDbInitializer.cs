using Microsoft.EntityFrameworkCore;
using Xyz.Provider.DataAccess.Entities;

namespace Xyz.Provider.Tests.DataTests
{
  public static class TestDbInitializer
  {
    /// <summary>
    /// Creates an InMemoryDatabase Context Options to use for testing
    /// </summary>
    /// <param name="name">Name for the InMemoryDatabase</param>
    /// <returns>DbContextOptions to be used for testing</returns>
    public static DbContextOptions<RevatureHousingDbContext> InitializeDbOptions(string name)
    {
      return new DbContextOptionsBuilder<RevatureHousingDbContext>()
        .UseInMemoryDatabase(databaseName: name)
        .Options;
    }

    /// <summary>
    /// Creates an in-memory database from the given DbContextOptions
    /// </summary>
    /// <param name="options">Information for constructing the database</param>
    /// <returns>RevatureHousingDbContext configured to use EF's in-memory DB</returns>
    public static RevatureHousingDbContext CreateTestDb(DbContextOptions<RevatureHousingDbContext> options)
    {
      var context = new RevatureHousingDbContext(options);
      context.Database.EnsureCreated();
      return context;
    }
  }
}
