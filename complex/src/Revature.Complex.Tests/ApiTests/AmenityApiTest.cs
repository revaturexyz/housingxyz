using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revature.Complex.Api.Controllers;
using Revature.Complex.Api.Models;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Lib.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Revature.Complex.Tests.ApiTests
{
  public class AmenityApiTest
  {
    /// <summary>
    /// this test is to test GetAmenitiesAsync in Amenity Api
    /// </summary>
    [Fact]
    public async void GetAmenitiesAsyncTest()
    {
      //setup
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var res = new List<Amenity>();
      _complexRepo.Setup(r => r.ReadAmenityListAsync())
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(await controller.GetAmenitiesAsync());

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(model);
    }

    /// <summary>
    /// This test is to test GetRoomAmenitiesAsync in Amenity Api
    /// </summary>
    [Fact]
    public async void GetRoomAmenitiesAsyncTest()
    {
      var rId = Guid.NewGuid();
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var res = new List<Amenity>();
      _complexRepo.Setup(r => r.ReadAmenityListByRoomIdAsync(rId))
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(await controller.GetRoomAmenitiesAsync(rId));

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(model);
    }

    /// <summary>
    /// This test is to test GetComplexAmenities in Amenity Api
    /// </summary>
    [Fact]
    public async void GetComplexAmenitiesAsyncTest()
    {
      var cId = Guid.NewGuid();
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var res = new List<Amenity>();
      _complexRepo.Setup(r => r.ReadAmenityListByComplexIdAsync(cId))
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(await controller.GetComplexAmenitiesAsync(cId));

      //assert
      Assert.IsAssignableFrom<ActionResult<IEnumerable<Amenity>>>(model);
    }

    /// <summary>
    /// This test is to test PostAmeityAsyncTest in Amenity Api
    /// </summary>
    [Fact]
    public async void PostAmenityAsyncTest()
    {
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      var apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      var res = true;
      _complexRepo.Setup(r => r.CreateAmenityAsync(amenity))
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult>(await controller.PostAmenityAsync(apiAmenity));

      //assert
      Assert.IsAssignableFrom<ActionResult>(model);
    }

    /// <summary>
    /// This test is to test PutAmenityAsync in Amenity Api
    /// </summary>
    [Fact]
    public async void PutAmenityAsyncTest()
    {
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      var apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      var res = true;
      _complexRepo.Setup(r => r.UpdateAmenityAsync(amenity))
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult>(await controller.PutAmenityAsync(apiAmenity));

      //assert
      Assert.IsAssignableFrom<ActionResult>(model);
    }

    /// <summary>
    /// This test is to test DeleteAmenityAsync in Amenity Api
    /// </summary>
    [Fact]
    public async void DeleteAmenityAsyncTest()
    {
      var _complexRepo = new Mock<IRepository>();
      var _logger = new Mock<ILogger<AmenityController>>();
      var amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      var apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      var res = true;
      _complexRepo.Setup(r => r.DeleteAmenityAsync(amenity))
          .Returns(Task.FromResult(res));

      //act
      var controller = new AmenityController(_complexRepo.Object, _logger.Object);
      var model = Assert.IsAssignableFrom<ActionResult>(await controller.DeleteAmenityAsync(apiAmenity));

      //assert
      Assert.IsAssignableFrom<ActionResult>(model);
    }
  }
}
