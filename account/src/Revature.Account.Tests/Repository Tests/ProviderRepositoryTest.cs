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
    [Fact]
    public void AddNewProviderAccountTest()
    {
      // Arrange
      TestHelper helper = new TestHelper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("AddNewProviderAccountTest")
          .Options;
      using var actContext = new AccountDbContext(options);
      var newProvider = helper.Providers[0];
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
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("UpdateProviderAccountTestAsync")
          .Options;
      using var arrangeContext = new AccountDbContext(options);
      var arrangeProvider = helper.Providers[0];
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(arrangeProvider));
      arrangeProvider.Name = "Robby";

      // Act
      var repo = new GenericRepository(arrangeContext);
      await repo.UpdateProviderAccountAsync(arrangeProvider);
      arrangeContext.SaveChanges();

      // Assert
      var assertContext = new AccountDbContext(options);
      var assertProvider = assertContext.ProviderAccount.First(p => p.ProviderId == arrangeProvider.ProviderId);
      Assert.Equal(arrangeProvider.Name, assertProvider.Name);
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

      arrangeContext.CoordinatorAccount.Add(mapper.MapCoordinator(helper.Coordinators[0]));
      arrangeContext.ProviderAccount.Add(mapper.MapProvider(helper.Providers[0]));
      arrangeContext.Status.Add(mapper.MapStatus(helper.Statuses[0]));
      arrangeContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);

      // Act
      var result = await repo.GetProviderAccountByIdAsync(helper.Providers[0].ProviderId);

      // Assert
      Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteProviderTestAsync()
    {
      //Assemble
      TestHelper helper = new TestHelper();
      Mapper mapper = new Mapper();
      var options = new DbContextOptionsBuilder<AccountDbContext>()
          .UseInMemoryDatabase("DeleteProviderTestAsync")
          .Options;
      using var assembleContext = new AccountDbContext(options);
      var deleteProvider = mapper.MapProvider(helper.Providers[2]);
      assembleContext.Add(deleteProvider);
      assembleContext.SaveChanges();
      using var actContext = new AccountDbContext(options);
      var repo = new GenericRepository(actContext);
      // Act
      await repo.DeleteProviderAccountAsync(deleteProvider.ProviderId);
      // Assert
      var provider = actContext.ProviderAccount.ToList();
      Assert.DoesNotContain(deleteProvider, provider);
    }
  }
}
