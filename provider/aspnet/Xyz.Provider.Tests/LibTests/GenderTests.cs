using System;
using Xunit;
using Xyz.Provider.Lib.Models;
namespace Xyz.Provider.Tests.LibTests
{
  public class GenderTests
  {
    /// <summary>
    /// tests to check that GenderType is not null, empty or whitespace.
    /// if so throw Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="name"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GenderTypeShouldNotBeEmptyOrNull(string name)
    {
      // Arrange (create default gender)
      var gender = new Gender();
      void Acted() => gender.GenderType = name;
      // Act (try to set the gender to a bad type),
      // Assert (ensure an ArgumentException is thrown)
      Assert.ThrowsAny<ArgumentException>(Acted);
    }
  }
}
