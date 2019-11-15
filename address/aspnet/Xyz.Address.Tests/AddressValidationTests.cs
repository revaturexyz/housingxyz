using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xyz.Address.Api.Models;
using Xyz.Address.Dbx.Models;
using Xyz.Address.Lib.Models;
using Xyz.Address.Lib.Validators;

namespace Xyz.Address.Tests
{
  ///<summary>Perform tests on validation attributes of Address Model</summary>
  public class AddressValidation
  {
    [Fact]
    public void ValidAddressEntityModel()
    {
      var sut = AddressTestData.ValidAddress1();
      var validationResult = new List<ValidationResult>();
      var actual = Validator.TryValidateObject(sut, new ValidationContext(sut), validationResult, true );
      var ErrorCount = validationResult.Count;
      Assert.True(actual);
      Assert.Equal(0, ErrorCount);
    }

    [Fact]
    public void ValidateCityState()
    {
      var validAddress = AddressTestData.InvalidCityState();
      var validationResult = new List<ValidationResult>();
      var actual  = Validator.TryValidateObject(validAddress, new ValidationContext(validAddress), validationResult, true);
      int ErrorCount = validationResult.Count;
      Assert.False(actual);
      Assert.Equal(1, ErrorCount); 
    }

    [Theory] 
    [InlineData("1560")]
    [InlineData("South")]
    [InlineData("South 1560")]
    public void StreetAtrribute(string Street)
    {
      //var Street = "1560";  
      var StreetValidator = new StreetValidatorAttribute();
      var StreetResult = StreetValidator.IsValid(Street);
      Assert.False(StreetResult);
    }
  }
}