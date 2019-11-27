using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Revature.Complex.Lib.Models;
using Revature.Complex.Lib.Interface;
using Revature.Complex.Api.Models;
using Xunit;
using Revature.Complex.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

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
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      List<Amenity> res = new List<Amenity>();
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
      Guid rId = Guid.NewGuid();
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      List<Amenity> res = new List<Amenity>();
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
      Guid cId = Guid.NewGuid();
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      List<Amenity> res = new List<Amenity>();
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
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      Amenity amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      ApiAmenity apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      bool res = true;
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
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      Amenity amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      ApiAmenity apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      bool res = true;
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
      Mock<IRepository> _complexRepo = new Mock<IRepository>();
      Mock<ILogger<AmenityController>> _logger = new Mock<ILogger<AmenityController>>();
      Amenity amenity = new Amenity
      {
        AmenityId = Guid.NewGuid(),
        AmenityType = "test1",
        Description = "describe"
      };
      ApiAmenity apiAmenity = new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = "test1",
        Description = "describe"
      };
      bool res = true;
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
