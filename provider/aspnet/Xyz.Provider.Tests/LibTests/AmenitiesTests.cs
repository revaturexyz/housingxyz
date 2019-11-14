using System;
using Xunit;

using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class AmenitiesTests
  {
    /// <summary>
    /// tests Amenity constructor will assign values to feilds.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange/Act
      var amenity = new Amenity
      {
        AmenityId = 1,
        AmenityType = "Test"
      };
      // Assert
      Assert.Equal(1, amenity.AmenityId);
      Assert.Equal("Test", amenity.AmenityType);
    }

    /// <summary>
    /// test to check that AmenityType string variable is not
    /// null, empty, or whitespace. if so throw
    /// Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="name"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NullOrWhitespaceNameShouldThrowException(string name)
    {
      // Arrange
      var amenity = new Amenity();
      void EmptyName() => amenity.AmenityType = name;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(EmptyName);
    }
  }
}
