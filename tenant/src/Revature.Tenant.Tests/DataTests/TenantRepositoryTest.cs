using Xunit;
using Revature.Tenant.DataAccess.Repository;
using Revature.Tenant.DataAccess;

namespace Revature.Tenant.Tests.DataTests
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
        // Arrange 
        var options = TestDbInitializer.InitializeDbOptions("TestConstructor");
        using var db = TestDbInitializer.CreateTestDb(options);
        var mapper = new Mapper();
        // act 
        var repo = new TenantRepository(db, mapper);
        // assert 
      }

    }
}
