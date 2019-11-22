using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using Xunit;

namespace Revature.Account.Tests.Repository_Tests
{
  /// <summary>
  /// Tests for the Coordinator's data-access-layer and it's supporting database-negotiation methods.
  /// </summary>
  public class CoordinatorRepositoryTest
  {
    [Fact]
    public async void GetCoordinatorByIdTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetCoordinatorByIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var testCoordinator = helper.Coordinators[0];
      var testId = testCoordinator.CoordinatorId;
      arrangeContext.CoordinatorAccount.Add(mapper.MapCoordinator(testCoordinator));
      arrangeContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);

      // Act
      var result = await repo.GetCoordinatorAccountByIdAsync(testId);

      // Assert
      Assert.Equal(testId, result.CoordinatorId);
    }


  }
}
