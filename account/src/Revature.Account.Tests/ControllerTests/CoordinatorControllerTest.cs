using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Revature.Account.Tests.ControllerTests
{
  /// <summary>
  /// Tests the API's Coordinator Controller.
  /// </summary>
  public class CoordinatorControllerTest
  {
    /// <summary>
    /// Test for coordinator retrieval based on their Guid-Id.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetCoordinatorByIdAsync()
    {
      TestHelper helper = new TestHelper();
      Guid coordinatorId = helper.Coordinators[0].CoordinatorId;

      helper.Repository
        .Setup(x => x.GetCoordinatorAccountByIdAsync(It.IsAny<Guid>()))
        .Returns(Task.Run(() => helper.Coordinators.Where(c => c.CoordinatorId == coordinatorId).FirstOrDefault()));

      Assert.NotNull(await helper.CoordinatorAccountController.Get(coordinatorId) as OkObjectResult);
    }
  }
}
