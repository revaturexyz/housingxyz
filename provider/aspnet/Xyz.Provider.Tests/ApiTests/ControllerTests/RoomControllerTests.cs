using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xyz.Provider.Api.Controllers;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.BusinessLogic;

namespace Xyz.Provider.Tests.ApiTests.ControllerTests
{
  public class RoomControllerTests
  {
    [Fact]
    public async Task GetShouldGetARoom()
    {
      // Arrange (create moq repos and use them for the controller)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      // Act (try to get a room by id)
      var result = await controller.GetAsync(1);
      // Assert (assure a room is returned and with the correct values)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var room = Assert.IsAssignableFrom<ApiRoom>(ok.Value);
      Assert.Equal(1, room.RoomId);
    }

    [Fact]
    public async Task GetByComplexIdShouldGetByComplexId()
    {
      // Arrange (create moq repos and use them for the controller)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      // Act (try to get all the rooms in a complex
      var result = await controller.GetByComplexIdAsync(2);
      // Assert (assure a list of rooms is returned)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var rooms = Assert.IsAssignableFrom<IEnumerable<ApiRoom>>(ok.Value);
      Assert.Equal(2, rooms.Count());
      Assert.True(rooms.All(r => r.RoomId > 2));
    }

    [Fact]
    public async Task GetByProviderIdShouldGetByProviderId()
    {
      // Arrange (create moq repos and use them for the controller)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      // Act (try to get all the rooms by a provider id)
      var result = await controller.GetByProviderIdAsync(2);
      // Assert (assure a list or IEnumerable is returned)
      var ok = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      var rooms = Assert.IsAssignableFrom<IEnumerable<ApiRoom>>(ok.Value).ToList();
      Assert.Equal(2, rooms.Count);
      Assert.True(rooms.All(r => r.RoomId > 2));
    }

    [Fact]
    public async Task PostShouldAddARoom()
    {
      // Arrange (create moq repos and use them for the controller, then create a room to add)
      var testList = ApiTestData.Rooms.ToList();
      var mockRooms = ApiTestData.MockRoomRepo(testList);
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      var newRoom = new ApiRoom
      {
        NumberOfBeds = 4,
        IsOccupied = true,
        RoomNumber = "101",
        StartDate = DateTime.Now,
        EndDate = null,
        HasAmenity = false,
        ApiAddress = ApiModelFactory.MakeApiAddress(ApiTestData.Addresses[0]),
        ApiGender = ApiModelFactory.MakeApiGender(ApiTestData.Male),
        ApiRoomType = ApiModelFactory.MakeApiRoomType(ApiTestData.Apartment),
        ApiComplex = ApiModelFactory.MakeApiComplex(ApiTestData.Complices[0]),
        ApiAmenity = new List<ApiAmenity>()
      };
      var addressLogic = new AddressLogic(); // should be mocked - this test hits the real Google API!
      // Act (add the room to the moq repo)
      var result = await controller.PostAsync(newRoom, 1, addressLogic);
      // Assert (assure the room was added with the correct values)
      var created = Assert.IsAssignableFrom<CreatedResult>(result.Result);
      Assert.EndsWith("5", created.Location);
      Assert.IsAssignableFrom<ApiRoom>(created.Value);
      Assert.Equal(5, testList.Count);
      Assert.Contains(testList, r => r.RoomId == 5);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task GetWithInvalidIdShouldReturnNotFound(int id)
    {
      // Arrange (create moq repos and use them for the controller)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      // Act (try to get a room by a bad id)
      var result = await controller.GetAsync(id);
      // Assert (assure a NotFoundResult is returned)
      Assert.IsAssignableFrom<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PutShouldUpdateARoom()
    {
      // Arrange (create moq repos and use them for the controller, then update an existing room)
      var testList = ApiTestData.Rooms.ToList();
      var mockRooms = ApiTestData.MockRoomRepo(testList);
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      var newEndDate = DateTime.Now.AddYears(1);
      var apiRoom = ApiModelFactory.MakeApiRoom(testList[0]);
      apiRoom.EndDate = newEndDate;
      apiRoom.NumberOfBeds = 4;
      apiRoom.IsOccupied = true;
      // Act (update the room through the controller call)
      var result = await controller.PutAsync(apiRoom, 1);
      // Assert (assure a NoContentResult is returned, and the room is updated)
      Assert.IsAssignableFrom<NoContentResult>(result);
      var editedRoom = testList[0];
      Assert.Equal(newEndDate, editedRoom.EndDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task PutWithInvalidIdShouldReturnBadRequest(int id)
    {
      // Arrange (create moq repos and use them for the controller, then make a room's id a bad value)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      var apiRoom = ApiModelFactory.MakeApiRoom(ApiTestData.Rooms[0]);
      apiRoom.RoomId = id;
      // Act (try to update that room)
      var result = await controller.PutAsync(apiRoom, 1);
      // Assert (assure an NotFoundResult is returned)
      Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task PutWithInvalidProviderIdShouldReturnNotFound(int providerId)
    {
      // Arrange (create moq repos and use them for the controller, and get a room to 'update')
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      var apiRoom = ApiModelFactory.MakeApiRoom(ApiTestData.Rooms[0]);
      // Act (try to update a room with an invalid providerId)
      var result = await controller.PutAsync(apiRoom, providerId);
      // Assert (assure an NotFoundResult is returned)
      Assert.IsAssignableFrom<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutWithIncorrectProviderShouldReturnBadRequest()
    {
      // Arrange (create moq repos and use them for the controller, then get a room)
      var mockRooms = ApiTestData.MockRoomRepo(ApiTestData.Rooms.ToList());
      var mockProviders = ApiTestData.MockProviderRepo(ApiTestData.Providers.ToList());
      var mockComplices = ApiTestData.MockComplexRepo(ApiTestData.Complices.ToList());
      var mockAddresses = ApiTestData.MockAddressRepo(ApiTestData.Addresses.ToList());
      var controller = new RoomController(mockRooms.Object, mockProviders.Object, mockAddresses.Object, mockComplices.Object);
      var apiRoom = ApiModelFactory.MakeApiRoom(ApiTestData.Rooms[0]);
      // Act (try to update a room with a different provider id)
      var result = await controller.PutAsync(apiRoom, 2);
      // Assert (assure a BadRequestObjectResult is returned)
      Assert.IsAssignableFrom<BadRequestObjectResult>(result);
    }
  }
}
