using Microsoft.EntityFrameworkCore;
using Xyz.Address.Dbx;

namespace Xyz.Address.Tests
{
    public static class TestDbContext
    {
      public static DbContextOptions<AddressDbContext> TestDbInitalizer(string name)
      {
        var finalDb = new DbContextOptionsBuilder<AddressDbContext>()
        .UseInMemoryDatabase(databaseName:name)
        .Options;
        return finalDb;
      }

      public static AddressDbContext CreateTestDb(DbContextOptions<AddressDbContext> options)
      {
        var context = new AddressDbContext(options);
        context.Database.EnsureCreated();
        return context;
      }
    }
}