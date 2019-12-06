using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revature.Account.Lib.Model;
using Xunit;

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
