using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Revature.Address.Lib.BusinessLogic;
using Xunit;

namespace Revature.Address.Tests.Lib.Tests
{
  public class AddressLogicTests
  {
    [Fact]
    public void CheckAddressFormatting()
    {
      var mockConfig = new Mock<IConfiguration>();
      var validator = new AddressLogic(mockConfig.Object);

      var newAddy = new Address.Lib.Address
      {
        Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Street = "1100 N E St",
        City = "Arlington",
        State = "Texas",
        Country = "US",
        ZipCode = "76010"
      };

      var result = validator.FormatAddress(newAddy);
      Assert.Equal("1100+N+E+St+Arlington,Texas+76010", result);
    }

    [Fact]
    public void CreateGoogleApiUrl()
    {
      var mockConfig = new Mock<IConfiguration>();
      var validator = new AddressLogic(mockConfig.Object);

      var newAddy = new Address.Lib.Address
      {
        Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Street = "1100 N E St",
        City = "Arlington",
        State = "Texas",
        Country = "US",
        ZipCode = "76010"
      };
      var address = validator.FormatAddress(newAddy);
      var result = validator.GetGoogleApiUrl(address, address);
      Assert.Equal($"?units=imperial&origins={address}&destinations={address}&key=", result);
    }
  }
}




