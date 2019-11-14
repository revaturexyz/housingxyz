using System;
using System.Collections.Generic;
using Xunit;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class ComplexTests
  {
    /// <summary>
    /// tests Complex constructor will assign values to feilds.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange/Act (use a default constructor, without setting properties)
      var notNull = new Complex();
      // assert (ensure the item isn't null)
      Assert.NotNull(notNull);
      // Arrange (create variables for the model properties)
      var name = "Generic";
      var number = "8675309";
      var id = 1;
      // Act (create model while setting properties)
      var parameters = new Complex()
      {
        ComplexId = id,
        ComplexName = name,
        ContactNumber = number,
        Address = new Address(),
        Center = new TrainingCenter(),
        Provider = new Lib.Models.Provider(),
        Rooms = new List<Room>()
      };

      // Assert (ensure parameters are equal to what is set to the complex)
      Assert.NotNull(parameters);
      Assert.NotNull(parameters.Address);
      Assert.NotNull(parameters.Center);
      Assert.NotNull(parameters.Provider);
      Assert.NotNull(parameters.Rooms);
      Assert.Empty(parameters.Rooms);
      Assert.Equal(id, parameters.ComplexId);
      Assert.Equal(name, parameters.ComplexName);
      Assert.Equal(number, parameters.ContactNumber);
    }

    /// <summary>
    /// test to check that ComplexName string variable is not
    /// null, empty, or whitespace if so throw
    /// Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="name"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ComplexNameShouldNotBeNullOrWhiteSpace(string name)
    {
      // Arrange (create a complex)
      var complex = new Complex();
      void Acted() => complex.ComplexName = name;
      // Act (try to set the complex name to a bad value),
      // Assert (ensure an ArugmentException of some type is thrown)
      Assert.ThrowsAny<ArgumentException>(Acted);
    }

    /// <summary>
    /// success bool determines whether something should work or fail
    /// </summary>
    /// <param name="number"></param>
    /// <param name="success"></param>
    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("8675309", true)]
    [InlineData("notANumber", false)]
    [InlineData("8421 8345", false)]
    [InlineData("!@#$%^&*()", false)]
    public void ContactNumbersShouldBeNumbers(string number, bool success)
    {
      // Arrange (create a complex)
      var comp = new Complex();
      void SetNumber() => comp.ContactNumber = number;

      // Act (try to set the number by the parameter)
      if (success)
      {
        SetNumber();
        // Assert: test passes if no exception thrown
      }
      else
      {
        // Assert
        Assert.ThrowsAny<ArgumentException>(SetNumber);
      }
    }
  }
}
