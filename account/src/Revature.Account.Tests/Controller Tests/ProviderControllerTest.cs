using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Revature.Account.Tests.Controller_Tests
{
  public class ProviderControllerTest
  {
    [Fact]
    public async Task GetProviderByIdAsync()
    {
      TestHelper helper = new TestHelper();
      Guid providerId = helper.Providers[0].ProviderId;

      helper.Repository
       .Setup(x => x.GetProviderAccountByIdAsync(It.IsAny<Guid>()))
       .Returns(Task.Run(() => helper.Providers.Where(c => c.ProviderId == providerId).FirstOrDefault()));

      Assert.NotNull(await helper.ProviderAccountController.Get(providerId) as OkObjectResult);
    }
  }
}
