using Xunit;

using Xyz.Provider.Api.Models;

namespace Xyz.Provider.Tests.ApiTests.ModelTests
{
  public class ApiAddressTests
  {
    [Fact]
    public void ConstructorShouldConstructWithSetters()
    {
      // Arrange (set up variables)
      var addressId = 1;
      var streetAddress = "123 Fake St.";
      var city = "San Luis Obispo";
      var state = "CA";
      var zipcode = "93401";
      // Act (set properties to variables through constructor)
      var apiAddress = new ApiAddress
      {
        AddressId = addressId,
        StreetAddress = streetAddress,
        City = city,
        State = state,
        ZipCode = zipcode
      };
      // Assert (assure the values are set)
      Assert.Equal(addressId, apiAddress.AddressId);
      Assert.Equal(streetAddress, apiAddress.StreetAddress);
      Assert.Equal(city, apiAddress.City);
      Assert.Equal(state, apiAddress.State);
      Assert.Equal(zipcode, apiAddress.ZipCode);
    }

    [Fact]
    public void MapperShouldMap()
    {
      // Arrange (set up variables and library model)
      var addressId = 1;
      var streetAddress = "123 Fake St";
      var city = "San Luis Obispo";
      var state = "CA";
      var zipcode = "93401";
      var address = new Lib.Models.Address
      {
        AddressId = addressId,
        StreetAddress = streetAddress,
        City = city,
        State = state,
        Zip = zipcode
      };
      // Act (map to an ApiModel)
      var apiAddress = Api.Mapper.Map(address);
      // Assert (assure the values are set properly)
      Assert.Equal(addressId, apiAddress.AddressId);
      Assert.Equal(streetAddress, apiAddress.StreetAddress);
      Assert.Equal(city, apiAddress.City);
      Assert.Equal(state, apiAddress.State);
      Assert.Equal(zipcode, apiAddress.ZipCode);
    }
  }
}
