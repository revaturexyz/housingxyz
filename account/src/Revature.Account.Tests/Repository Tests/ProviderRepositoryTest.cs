using System;
using System.Collections.Generic;
using System.Text;
using Revature.Account.DataAccess.Entities;
using Revature.Account.DataAccess;
using Revature.Account.Lib.Model;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Revature.Account.DataAccess.Repositories;
using Entity = Revature.Account.DataAccess;
using System.Threading.Tasks;

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
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("AddNewProviderAccountTest")
          .Options;
      using var actContext = new AccountDbContext(options);
      var newProvider = new Lib.Model.ProviderAccount
      {
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Name = "Testing",
        Password = "aBCXYZ123",
        Status = "Pending",
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(7)
      };
      var actRepo = new GenericRepository(actContext);
      // Act
      actRepo.AddNewProviderAccountAsync(newProvider);
      actContext.SaveChanges();
      // Assert
      using var assertContext = new AccountDbContext(options);
      var assertProvider = assertContext.ProviderAccount.First(p => p.ProviderId == newProvider.ProviderId);
      Assert.NotNull(assertProvider);
    }
    [Fact]
    public async Task UpdateProviderAccountTestAsync()
    {
      // Arrange
      var updatedName = "Robby";
      var updatedStatus = "Under Review";
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("UpdateProviderAccountTestAsync")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var arrangeProvider = new DataAccess.Entities.ProviderAccount
      {
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Name = "Testing",
        Password = "ABCxyz123",
        Status = "Pending",
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(7)
      };
      arrangeContext.ProviderAccount.Add(arrangeProvider);
      var updatedProvider = new Lib.Model.ProviderAccount
      {
        ProviderId = providerId,
        CoordinatorId = coordinatorId,
        Name = updatedName,
        Password = "ABCxyz123",
        Status = updatedStatus,
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(30)
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
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("GetProviderByIdTest")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var testId = providerId;
      var testProviderEntity = new DataAccess.Entities.ProviderAccount
      {
        CoordinatorId = coordinatorId,
        ProviderId = providerId,
        Name = "Joe Doe",
        Password = "Abcxzy123",
        Status = "Pending",
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(7),
      };
      arrangeContext.ProviderAccount.Add(testProviderEntity);
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
        Password = "aBCXYZ123",
        Status = "Pending",
        AccountCreated = DateTime.Now,
        Expire = DateTime.Now.AddDays(7)
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
