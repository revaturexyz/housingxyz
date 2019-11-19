using Microsoft.EntityFrameworkCore;
using Revature.Tenant.DataAccess.Entities;

namespace Revature.Tenant.Tests.DataTests
{
  public static class TestDbInitializer
  {
    /// <summary>
    /// This creates the In-Memory Database to be used for testing
    /// </summary>
    /// <param name="name">In-Memory Database's name</param>
    /// <returns>DbContextOptions for testing</returns>
    public static DbContextOptions<TenantsContext> InitializeDbOptions(string name)
    {
      return new DbContextOptionsBuilder<TenantsContext>()
        .UseInMemoryDatabase(databaseName: name)
        .Options;
    }

    /// <summary>
    /// Creates an in-memory database from the given DbContextOptions
    /// </summary>
    /// <param name="options">Information for constructing the database</param>
    /// <returns>RevatureHousingDbContext configured to use EF's in-memory DB</returns>
    public static TenantsContext CreateTestDb(DbContextOptions<TenantsContext> options)
    {
      var context = new TenantsContext(options);
      context.Database.EnsureCreated();
      return context;
    }
  }
}
