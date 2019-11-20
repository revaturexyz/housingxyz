using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using Xunit;

namespace Revature.Account.Tests.Repository_Tests
{
  public class CoordinatorRepositoryTest
  {
    public Guid coordinatorId = Guid.NewGuid();
    public Guid providerId = Guid.NewGuid();
    public Guid notificationId = Guid.NewGuid();

    [Fact]
    public async void GetCoordinatorByIdTest()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetCoordinatorByIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var testId = coordinatorId;
      var testCoordinatorEntity = new DataAccess.Entities.CoordinatorAccount
      {
        CoordinatorId = coordinatorId,
        ProviderId = providerId,
        Name = "Fred",
        TrainingCenterAddress = "123 Main st, Arlington, TX 12345",
        TrainingCenterName = "Liv+",
        Email = "abc@gmail.com"
      };
      arrangeContext.CoordinatorAccount.Add(testCoordinatorEntity);
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
