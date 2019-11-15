using System;
using Xunit;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class TypeTests
  {
    /// <summary>
    /// tests RoomType constructor will assign values to feilds.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange
      string typeName = "Generic";
      int id = 1;

      // Act
      var type = new RoomType
      {
        TypeId = id,
        Type = typeName
      };

      // Assert (ensure properties are set properly)
      Assert.NotNull(type);
      Assert.Equal(typeName, type.Type);
      Assert.Equal(id, type.TypeId);
    }

    /// <summary>
    /// RoomType Type string should not be null, empty,
    /// or whitespace if so 
    /// </summary>
    /// <param name="room"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void RoomTypeShouldNotBeNullOrWhiteSpace(string room)
    {
      // Arrange
      var test = new RoomType();
      void Act() => test.Type = room;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(Act);
    }
    // success determines whether a variable should pass or not
    /// <summary>
    /// This method ideally will be split into valid values
    /// and invalid values. Valid will assert the assigned value is equal.
    /// invalid asserts execption being thrown.
    /// </summary>
    /// <param name="room"></param>
    /// <param name="success"></param>
    [Theory]
    [InlineData("Valid", true)]
    [InlineData("!nvalid", false)]
    [InlineData("So this isn't valid", false)]
    [InlineData("IRValid", true)]
    [InlineData("-----yes-----", true)]
    public void RoomTypeValidationTesting(string room, bool success)
    {
      // Arrange
      var test = new RoomType();
      void SetType() => test.Type = room; // try to set the room type

      // Act
      if (success)
      {
        SetType();
        // assert: test pass if no exception thrown
      }
      else
      {
        // assert
        Assert.ThrowsAny<ArgumentException>(SetType);
      }
    }
  }
}
