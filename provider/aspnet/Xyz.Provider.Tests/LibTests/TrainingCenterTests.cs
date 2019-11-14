using System;
using System.Collections.Generic;
using Xunit;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class TrainingCenterTests
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // arrange (nothing needed here)

      // act (set parameters in TrainingCenter object in constructor)
      var center = new TrainingCenter()
      {
        CenterId = 1,
        Address = new Address(),
        CenterName = "UTA",
        ContactNumber = "1234567890",
        Providers = new List<Lib.Models.Provider>()
      };

      // assert (ensure that variables were set properly)
      Assert.NotNull(center);
      Assert.Equal(1, center.CenterId);
      Assert.Equal("UTA", center.CenterName);
      Assert.Equal("1234567890", center.ContactNumber);
      Assert.NotNull(center.Address);
      Assert.NotNull(center.Providers);
      Assert.Empty(center.Providers);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CenterNameShouldNotBeNullOrWhiteSpace(string name)
    {
      // arrange
      var test = new TrainingCenter(); // default constructor
      void Act() => test.CenterName = name; // try to set the center name to a bad value

      // act, assert
      Assert.ThrowsAny<ArgumentException>(Act); // checks if Argument or ArgumentNull Exceptions are thrown in Action
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("8675309", true)]
    [InlineData("notANumber", false)]
    [InlineData("8421 8345", false)] // white space in the middle of it, this should fail
    [InlineData("!@#$%^&*()", false)]
    public void ContactNumbersShouldBeNumbers(string number, bool success) // success bool determines whether the variable should pass or fail
    {
      // arrange
      var test = new TrainingCenter(); // default constructor
      void SetNumber() => test.ContactNumber = number; // try to set the ContactNumber to the number parameter

      // act
      if (success)
      {
        SetNumber();
        // assert: test passes if no exception thrown
      }
      else
      {
        // assert
        Assert.ThrowsAny<ArgumentException>(SetNumber);
      }
    }
  }
}
