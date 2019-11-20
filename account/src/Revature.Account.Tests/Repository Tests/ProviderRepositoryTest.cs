using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess;
using Revature.Account.DataAccess.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Revature.Account.Tests;

namespace Revature.Account.Test.Repository_Tests
{
  public class ProviderRepositoryTest
  {
    public Guid coordinatorId = Guid.NewGuid();
    public Guid providerId = Guid.NewGuid();
    public Guid notificationId = Guid.NewGuid();

    [Fact]
    public void AddNewProviderAccountTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("AddNewProviderAccountTest")
          .Options;
      using var actContext = new AccountDbContext(options);
      var newProvider = new Lib.Model.ProviderAccount
      {
        ProviderId = providerId,
        Coordinator = helper.ICoordinators[0],
        Name = "Testing",
        Status = "Pending",
        AccountCreatedAt = DateTime.Now,
        AccountExpiresAt = DateTime.Now.AddDays(7)
      };
      var actRepo = new GenericRepository(actContext);
      // Act
      actRepo.AddProviderAccountAsync(newProvider);
      actContext.SaveChanges();
      // Assert
      using var assertContext = new AccountDbContext(options);
      var assertProvider = assertContext.ProviderAccount.FirstOrDefault(p => p.ProviderId == newProvider.ProviderId);
      Assert.NotNull(assertProvider);
    }

    [Fact]
    public async Task UpdateProviderAccountTestAsync()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var updatedName = "Robby";
      var updatedStatus = "Under Review";
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("UpdateProviderAccountTestAsync")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var arrangeProvider = new Lib.Model.ProviderAccount
      {
        ProviderId = providerId,
        Coordinator = helper.ICoordinators[0],
        Name = "Testing",
        Status = "Pending",
        AccountCreatedAt = DateTime.Now,
        AccountExpiresAt = DateTime.Now.AddDays(7)
      };
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(arrangeProvider));
      var updatedProvider = new Lib.Model.ProviderAccount
      {
        ProviderId = providerId,
        Coordinator = helper.ICoordinators[0],
        Name = updatedName,
        Status = updatedStatus,
        AccountCreatedAt = DateTime.Now,
        AccountExpiresAt = DateTime.Now.AddDays(30)
      };
      // Act
      var repo = new GenericRepository(arrangeContext);
      await repo.UpdateProviderAccountAsync(updatedProvider);
      arrangeContext.SaveChanges();
      // Assert
      var assertContext = new AccountDbContext(options);
      var assertProvider = assertContext.ProviderAccount.First(p => p.ProviderId == providerId);
      Assert.Equal(updatedName, assertProvider.Name);
      Assert.Equal(updatedStatus, assertProvider.Status);
    }

    [Fact]
    public async void GetProviderByIdTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetProviderByIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);

      var testProvider = helper.IProviderAccountList[0];
      var testId = testProvider.ProviderId;

      arrangeContext.CoordinatorAccount.Add(mapper.MapCoordinator(helper.ICoordinators[0]));
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(testProvider));
      arrangeContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      var result = await repo.GetProviderAccountByIdAsync(testId);
      // Assert
      Assert.Equal(testId, result.ProviderId);
    }

    [Fact]
    public async Task DeleteProviderTestAsync()
    {
      //Assemble
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("DeleteProviderTestAsync")
          .Options;
      using var assembleContext = new AccountDbContext(options);
      var deleteProvider = new DataAccess.Entities.ProviderAccount
      {
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Name = "Testing",
        Status = "Pending",
        AccountCreatedAt = DateTime.Now,
        AccountExpiresAt = DateTime.Now.AddDays(7)
      };
      assembleContext.Add(deleteProvider);
      assembleContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      await repo.DeleteProviderAccountAsync(providerId);
      // Assert
      var provider = actContext.ProviderAccount.ToList();
      Assert.DoesNotContain(deleteProvider, provider);
    }
  }
}
