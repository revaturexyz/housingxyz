using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xyz.Provider.Api.Controllers;
using Xyz.Provider.Api.Models;
using Xyz.Provider.DataAccess;
using Xyz.Provider.DataAccess.Repository;
using Xyz.Provider.Tests.DataTests;

namespace Xyz.Provider.Tests.ApiTests.ControllerTests
{
  public class ComplexControllerTests
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (create database)
      var options = TestDbInitializer.InitializeDbOptions("TestComplexControllerConstructor");
      using var db = TestDbInitializer.CreateTestDb(options);

      // act (use the ComplexRepository with a database to create controller)
      var test = new ComplexController(new ComplexRepository(db));

      // assert (test passes if no exception thrown)
    }

    [Fact]
    public async Task GetShouldGetProvider()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetComplexByProvider");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new ComplexController(new ComplexRepository(db));

      // act (get a list of complexes by a provider id)
      var result = await controller.GetByProviderIdAsync(1);

      // assert (ensure the list is received and isn't empty)
      var test = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      Assert.NotEmpty(Assert.IsAssignableFrom<IEnumerable<ApiComplex>>(test.Value));
    }

    [Fact]
    public async Task GetShouldGetAmenities()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetAmenitiesForComplex");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new ComplexController(new ComplexRepository(db));

      // act (get the amenities a complex provides)
      var result = await controller.GetAmenitiesByComplexIdAsync(1);

      // assert (ensure the list is received)
      var test = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
      Assert.NotNull(test.Value);
    }

    [Fact]
    public async Task BadIdShouldBeNotFound()
    {
      // arrange (create controller)
      var options = TestDbInitializer.InitializeDbOptions("TestGetWithBadIDs");
      using var db = TestDbInitializer.CreateTestDb(options);
      var controller = new ComplexController(new ComplexRepository(db));

      // act (try to get all complexes from a bad provider id)
      var result = await controller.GetByProviderIdAsync(-1);

      // assert (assure the controller returns the NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result.Result);

      // act (try to get all amenities from a bad complex id)
      var result2 = await controller.GetAmenitiesByComplexIdAsync(-1);

      // assert (assure the controller returns the NotFoundResult)
      Assert.IsAssignableFrom<NotFoundResult>(result2.Result);
    }

    [Fact]
    public async Task PostShouldPost()
    {
      // arrange
      var options = TestDbInitializer.InitializeDbOptions("TestPostComplex");
      ApiComplex apiComplex;
      Lib.Models.Complex newComplex;
      using (var db = TestDbInitializer.CreateTestDb(options))
      {
        var newAddress = new Lib.Models.Address
        {
          AddressId = db.Address.Max(a => a.AddressId) + 1,
          StreetAddress = "123 Fake St.",
          City = "Berkeley",
          State = "CA",
          Zip = "94704"
        };
        db.Address.Add(Mapper.Map(newAddress));
        db.SaveChanges();
        newComplex = new Lib.Models.Complex
        {
          ComplexId = db.Complex.Max(c => c.ComplexId) + 1,
          Address = newAddress,
          ComplexName = "Liv++",
          ContactNumber = "1234567890",
          Center = Mapper.Map(db.TrainingCenter.Find(1)),
          Provider = Mapper.Map(db.Provider.Find(1)),
          Rooms = new List<Lib.Models.Room>()
        };

        apiComplex = ApiModelFactory.MakeApiComplex(newComplex);
      }

      // act
      CreatedResult result;
      using (var db = TestDbInitializer.CreateTestDb(options))
      {
        var controller = new ComplexController(new ComplexRepository(db));
        var actionResult = await controller.PostAsync(1, apiComplex);
        result = Assert.IsAssignableFrom<CreatedResult>(actionResult.Result);
      }
      Assert.EndsWith(apiComplex.ComplexId.ToString(), result.Location);
      var createdComplex = Assert.IsAssignableFrom<ApiComplex>(result.Value);
      Assert.Equal(apiComplex.ComplexId, createdComplex.ComplexId);
    }

    [Fact]
    public async Task PostWithDuplicateAddressShouldReturnConflict()
    {
      // Arrange
      var options = TestDbInitializer.InitializeDbOptions("TestPostComplexWithWrongProviderID");
      ApiComplex apiComplex;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var newComplex = new Lib.Models.Complex
        {
          ComplexId = context.Complex.Max(c => c.ComplexId) + 1,
          Address = Mapper.Map(context.Address.Find(context.Complex.Find(1).AddressId)),
          ComplexName = "Liv++",
          ContactNumber = "1234567890",
          Center = Mapper.Map(context.TrainingCenter.Find(1)),
          Provider = Mapper.Map(context.Provider.Find(1)),
          Rooms = new List<Lib.Models.Room>()
        };

        apiComplex = ApiModelFactory.MakeApiComplex(newComplex);
      }
      // Act
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var controller = new ComplexController(new ComplexRepository(context));
        var result = await controller.PostAsync(2, apiComplex);
        // Assert
        Assert.IsAssignableFrom<ConflictObjectResult>(result.Result);
      }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(65536)]
    public async Task PostWithInvalidProviderIdShouldReturnClientError(int providerId)
    {
      // Arrange
      var options = TestDbInitializer.InitializeDbOptions("TestPostComplexWithWrongProviderID");
      ApiComplex apiComplex;
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var newAddress = new Lib.Models.Address
        {
          AddressId = context.Address.Max(a => a.AddressId) + 1,
          StreetAddress = "123 Fake St.",
          City = "Berkeley",
          State = "CA",
          Zip = "94704"
        };
        context.Address.Add(Mapper.Map(newAddress));
        context.SaveChanges();
        var newComplex = new Lib.Models.Complex
        {
          ComplexId = context.Complex.Max(c => c.ComplexId) + 1,
          Address = newAddress,
          ComplexName = "Liv++",
          ContactNumber = "1234567890",
          Center = Mapper.Map(context.TrainingCenter.Find(1)),
          Provider = Mapper.Map(context.Provider.Find(1)),
          Rooms = new List<Lib.Models.Room>()
        };

        apiComplex = ApiModelFactory.MakeApiComplex(newComplex);
      }
      // Act
      using (var context = TestDbInitializer.CreateTestDb(options))
      {
        var controller = new ComplexController(new ComplexRepository(context));
        var result = await controller.PostAsync(providerId, apiComplex);
        // Assert
        Assert.IsAssignableFrom<NotFoundResult>(result.Result);
      }
    }
  }
}
