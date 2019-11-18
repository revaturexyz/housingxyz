using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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
        Password = "Abcxzy123",
        TrainingAddress = "123 Main st, Arlington, TX 12345",
        TrainingName = "Liv+",
        Email = "abc@gmail.com"
      };
      arrangeContext.CoordinatorAccount.Add(testCoordinatorEntity);
      arrangeContext.SaveChanges();

      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);

      // Act
      var result = await repo.GetCoordinatorAccountById(testId);
      // Assert

      Assert.Equal(testId, result.CoordinatorId);

    }
  }
}
