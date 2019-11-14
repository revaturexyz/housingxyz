using System;
using System.Collections.Generic;
using Xunit;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.LibTests
{
  public class ProviderTests
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      // Arrange

      // Act (create a Provider object)
      var provider = new Lib.Models.Provider
      {
        ProviderId = 1,
        Username = "user",
        Password = "P4$$w0rd",
        CompanyName = "Liv+",
        ContactNumber = "1234567890",
        Address = new Address(),
        Center = new TrainingCenter(),
        Complexes = new List<Complex>()
      };

      // Assert (ensure variables are set to the correct values)
      Assert.Equal(1, provider.ProviderId);
      Assert.Equal("user", provider.Username);
      Assert.Equal("P4$$w0rd", provider.Password);
      Assert.Equal("Liv+", provider.CompanyName);
      Assert.Equal("1234567890", provider.ContactNumber);
      Assert.NotNull(provider.Address);
      Assert.NotNull(provider.Center);
      Assert.NotNull(provider.Complexes);
      Assert.Empty(provider.Complexes);
    }

    [Theory]
    [InlineData("Pass@word1")]
    [InlineData("Password$0")]
    [InlineData("P4ssword!")]
    public void PasswordsShouldContainLettersNumbersAndSpecialCharacters(string pass)
    {
      // Arrange
      var provider = new Lib.Models.Provider
      {
        // Act
        Password = pass // set the password to a value
      }; // default constructor
      // Assert
      Assert.Equal(pass, provider.Password); // ensure that value is the same
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1234")]
    [InlineData("password")]
    [InlineData("PassWord")]
    [InlineData("Password1")]
    [InlineData("Password!")]
    [InlineData("Pa5$!")]
    public void WeakPasswordsShouldBeUnacceptable(string pass)
    {
      // Arrange
      var provider = new Lib.Models.Provider(); // default constructor
      void WeakPassword() => provider.Password = pass; // try to set password to a bad password
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(WeakPassword); // checks if Argument or ArgumentNull Exceptions are thrown in Action
    }

    [Theory]
    [InlineData("UserName")]
    [InlineData("User_Name")]
    [InlineData("UserName123")]
    [InlineData("U$erName")]
    public void UserNameCanContainNumbersAndUnderscores(string name)
    {
      // Arrange
      var provider = new Lib.Models.Provider
      {
        // Act
        Username = name // set Username to passed in parameter
      }; // default constructor
      // Assert
      Assert.Equal(name, provider.Username); // ensure that the value is set properly
    }

    [Theory]
    [InlineData("User>Name")]
    [InlineData("User-Name")]
    [InlineData("User~Name")]
    [InlineData("User/Name")]
    [InlineData("User.Name")]
    public void UserNameCannotContainSpecialCharacters(string name)
    {
      // Arrange
      var provider = new Lib.Models.Provider(); // default constructor
      void BadName() => provider.Username = name; // try to set Username to a bad value
      // Act, Assert
      Assert.ThrowsAny<ArgumentException>(BadName); // checks if Argument or ArgumentNull Exceptions is thrown in Action
    }
  }
}
