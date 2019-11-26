using System;

namespace Revature.Address.Tests.DataAccess.Tests
{
  /// <summary>
  /// This class holds testing data for data
  /// access tests
  /// </summary>
  public static class AddressTestData
  {
    public static Address.DataAccess.Entities.Address ValidAddress1()
    {
      var ValidAddress1 = new Address.DataAccess.Entities.Address
      {
        Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Street = "1100 S W St",
        City = "Arlington",
        State = "Texas",
        Country = "US",
        ZipCode = "76010"
      };
      return ValidAddress1;
    }


    public static Address.DataAccess.Entities.Address ValidAddress2()
    {
      var ValidAddress2 = new Address.DataAccess.Entities.Address
      {
        Id = new Guid("2d9fe0e6-8029-483d-b5e9-c06cf2fad0d6"),
        Street = "1560 Broadway Street",
        City = "Boulder",
        State = "Colorado",
        Country = "US",
        ZipCode = "80112"
      };
      return ValidAddress2;
    }
  }
}
