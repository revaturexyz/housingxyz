using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Revature.Account.Lib.Model;

namespace Revature.Account.Tests.ControllerTests
{
  /// <summary>
  /// Tests for the Provider Controller.
  /// </summary>
  public class ProviderControllerTest
  {
    /// <summary>
    /// Test for Provider retrieval based on their Guid-Id. 
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetProviderByIdAsync()
    {
      TestHelper helper = new TestHelper();
      Guid providerId = helper.Providers[0].ProviderId;

      helper.Repository
       .Setup(x => x.GetProviderAccountByIdAsync(It.IsAny<Guid>()))
       .Returns(Task.FromResult(helper.Providers[0]));

      Assert.NotNull(await helper.ProviderAccountController.Get(providerId) as OkObjectResult);
    }

    /// <summary>
    /// Test for the successful creation of a Provider account.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNewProviderAccountSuccessfullyVerifyTestAsync()
    {
      TestHelper helper = new TestHelper();
      ProviderAccount newProvider = new ProviderAccount();
      newProvider.AccountCreatedAt = DateTime.Now;
      newProvider.Name = "Testing";

      helper.Repository
        .Setup(x => x.AddProviderAccountAsync(It.IsAny<ProviderAccount>()))
        .Verifiable();
      helper.Repository
        .Setup(x => x.GetStatusByStatusTextAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(helper.Statuses[0]));
     
      var newProviderAccount = await helper.ProviderAccountController.Post(newProvider);

      helper.Repository
         .Verify();
    }

    /// <summary>
    /// Test for a sucessful provider-account-update.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task UpdateProviderAccountAsyncSuccessfulVerificationAsync()
    {
      TestHelper helper = new TestHelper();
      Guid providerId = helper.Notifications[0].ProviderId;
      var updatedProvider = helper.Providers[0];
      updatedProvider.Name = "New Name";

      helper.Repository
          .Setup(x => x.GetProviderAccountByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(helper.Providers[0]))
              .Verifiable();
      helper.Repository
          .Setup(x => x.UpdateProviderAccountAsync(It.IsAny<ProviderAccount>()))
              .Returns(Task.FromResult(true))
              .Verifiable();

      var updatedResult = await helper.ProviderAccountController.Put(providerId, updatedProvider);

      helper.Repository
          .Verify();
    }

    /// <summary>
    /// Test for a successful provider account deletion.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeletProviderAccountAsyncSuccessfulVerificationAsync()
    {
      TestHelper helper = new TestHelper();
      Guid providerId = helper.Providers[0].ProviderId;

      helper.Repository
          .Setup(x => x.GetProviderAccountByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(helper.Providers[0]))
              .Verifiable();
      helper.Repository
          .Setup(x => x.DeleteProviderAccountAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(true))
              .Verifiable();

      var deleted = await helper.ProviderAccountController.Delete(providerId);

      helper.Repository
          .Verify();
    }
  }
}
