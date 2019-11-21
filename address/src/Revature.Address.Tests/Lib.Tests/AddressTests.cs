using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
    public class AddressTests
    {
    /// <summary>
    /// tests Address constructor will assign values to fields.
    /// </summary>
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // act 
      var address = new Address.Lib.Address
      {
        Id = new Guid(),
        Street = "123 Fake St",
        City = "Las Vegas",
        State = "NV",
        Country = "USA",
        ZipCode = "42069"
      };
      // Assert
      Assert.Equal(address.Id, address.Id);
      Assert.Equal("123 Fake St", address.Street);
      Assert.Equal("Las Vegas", address.City);
      Assert.Equal("NV", address.State);
      Assert.Equal("USA", address.Country);
      Assert.Equal("42069", address.ZipCode);
    }

    /// <summary>
    /// test to check street property is not null or
    /// whitespace if so throw Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="street"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void StreetAddressShouldNotBeNullOrWhitespace(string street)
    {
      // Arrange
      var address = new Address.Lib.Address();
      void Acted() => address.Street = street;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(Acted);
    }

    /// <summary>
    /// test to check city string is null or whitespace
    /// will throw Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="city"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CityShouldNotBeNullOrWhitespace(string city)
    {
      // Arrange
      var address = new Address.Lib.Address(); // default constructor
      void Acted() => address.City = city;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(Acted);
    }
    /// <summary>
    /// test to check if state string is null or whitespace
    /// if null or whitespace assert that Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="state"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void StateNamesShouldNotBeNullOrWhitespace(string state)
    {
      // Arrange
      var address = new Address.Lib.Address();
      void Acted() => address.State = state;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(Acted);
    }
    /// <summary>
    /// test to check that zip string is a number
    /// if not assert that Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="zip"></param>

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Zip")]
    [InlineData(" 00000 ")]
    [InlineData("12345-6789")]
    public void ZipShouldBeNumeric(string zip)
    {
      // Arrange
      var address = new Address.Lib.Address();
      void BadZip() => address.ZipCode = zip;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(BadZip);
    }
    /// <summary>
    /// test to check that zip string is a 5 char long.
    /// if not assert that Argument or ArgumentNull Exceptions are thrown in Action
    /// </summary>
    /// <param name="zip"></param>
    [Theory]
    [InlineData("1")]
    [InlineData("1234")]
    [InlineData("123456789")]
    public void ZipShouldBeFiveCharacters(string zip)
    {
      // Arrange
      var address = new Address.Lib.Address();
      void BadZip() => address.ZipCode = zip;
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(BadZip);
    }
  }
}
