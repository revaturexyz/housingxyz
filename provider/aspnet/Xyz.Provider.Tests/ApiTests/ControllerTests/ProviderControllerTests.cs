using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xyz.Provider.Api.Controllers;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Interface;

namespace Xyz.Provider.Tests.ApiTests.ControllerTests
{
  /// <summary>
  /// Unit tests for methods of Provider controller
  /// </summary>
  public class ProviderControllerTests
  {
    /// <summary>
    /// Checks that all providers in the repository are returned by Get method w/o parameters
    /// </summary>
    /// <returns>Task b/c async, does nothing</returns>
    [Fact]
    public async Task GetAllShouldGetAll()
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      // Act (get all providers)
      var result = await controller.GetAllAsync();
      // Assert (assure the function runs and returns a list of providers)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var providers = Assert.IsAssignableFrom<IEnumerable<ApiProvider>>(ok.Value);
      Assert.Equal(ApiTestData.Providers.Count, providers.Count());
    }

    [Fact]
    public async Task GetByIdShouldGetById()
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      // Act (get a provider with an id)
      var result = await controller.GetAsync(1);
      // Assert (ensure the provider is returned with the correct values)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var provider = Assert.IsAssignableFrom<ApiProvider>(ok.Value);
      Assert.Equal(1, provider.ProviderId);
    }

    [Fact]
    public async Task PutShouldUpdateProvider()
    {
      // Arrange (create a moq repo and use it for the controller, then update an existing provider)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      var provider = ApiModelFactory.MakeApiProvider(ApiTestData.Providers[0]);
      provider.CompanyName = "TestCo";
      provider.ContactNumber = "8058675309";
      provider.Username = "prov1";
      provider.Password = "Pa$$W0rd!";
      // Act (update the provider with the controller method)
      var result = await controller.PutAsync(provider.ProviderId, provider);
      // Assert (assure the put method returns a NoContentResult and that the provider was updated)
      Assert.IsAssignableFrom<NoContentResult>(result);
      var result2 = await controller.GetAsync(provider.ProviderId);
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result2.Result);
      var editedProvider = Assert.IsAssignableFrom<ApiProvider>(ok.Value);
      Assert.Equal(provider.CompanyName, editedProvider.CompanyName);
      Assert.Equal(provider.ContactNumber, editedProvider.ContactNumber);
      Assert.Equal(provider.Username, editedProvider.Username);
      Assert.Equal(provider.Password, editedProvider.Password);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task GetWithInvalidIdShouldReturnNotFound(int id)
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      // Act (try to get by an invalid id)
      var result = await controller.GetAsync(id);
      // Assert (assure the return type is a NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result.Result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task PutWithInvalidIdShouldReturnNotFound(int id)
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      // Act (try to update a provider with a bad id)
      var result = await controller.PutAsync(id, new ApiProvider { ProviderId = id });
      // Assert (assure the return is a NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutWithIncorrectIdShouldReturnConflict()
    {
      // Arrange (create a moq repo and use it for the controller)
      Mock<IProviderRepository> mockRepo = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var controller = new ProviderController(mockRepo.Object);
      // Act (try to update a provider with a different provider id)
      var result = await controller.PutAsync(1, new ApiProvider { ProviderId = 2 });
      // Assert (assure the return is a ConflictResult)
      Assert.IsAssignableFrom<ConflictResult>(result);
    }
  }
}
