using System;
using Xyz.Address.Api.Models;
using Xyz.Address.Dbx.Models;

namespace Xyz.Address.Tests
{
  public static class AddressTestData
  {
    public static AddressEntity ValidAddress1()
    {
      var ValidAddress1 = new AddressEntity
      {
        Key = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Active = true,
        Street = "1100 S W St",
        City = "Arlington",
        StateProvince = "Texas", 
        Country = "US",
        PostalCode = "76010"
      };
      return ValidAddress1;
    }

    public static AddressEntity UpdateValidAddress1()
    {
      var UpdateValidAddress1 = new AddressEntity
      {
        Key = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Active = true,
        Street = "1100 N E St",
        City = "Arlington",
        StateProvince = "Texas", 
        Country = "US",
        PostalCode = "76010"
      };
      return UpdateValidAddress1;
    }

    public static AddressEntity UpdateInvalidAddress1()
    {
      var UpdateInValidAddress1 = new AddressEntity
      {
        Key = new Guid("69f21a8b-ac72-4e4d-86e8-8d605b69828b"),
        Active = true,
        Street = "1100 N E St",
        City = "Arlington",
        StateProvince = "Texas", 
        Country = "US",
        PostalCode = "76010"
      };
      return UpdateInValidAddress1;
    }

    public static AddressEntity ValidAddress2()
    {
      var ValidAddress2 = new AddressEntity
      {
        Key  = new Guid("2d9fe0e6-8029-483d-b5e9-c06cf2fad0d6"), 
        Active = true,
        Street = "1560 Broadway Street", 
        City  = "Boulder", 
        StateProvince = "Colorado",
        Country = "US",
        PostalCode = "80112"
      };
      return ValidAddress2;
    }

    public static AddressApiModel InvalidCityState()
    {
      var CityState = new AddressApiModel
      {
        Key = Guid.NewGuid(),
        Street = "1560 Broadway St.",
        City = "Colorado",
        StateProvince = "Colorado", 
        PostalCode = "80112",
        Country = "US",    
      };
      return CityState;
    }
  }
}