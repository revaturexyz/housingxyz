using Microsoft.Extensions.Configuration;
using Revature.Address.Lib.BusinessLogic;
using System;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;

namespace Revature.Address.Tests.Lib.Tests
{
  public class AddressLogicTests
  {
    [Fact]
    public void ConstructorShouldConstruct()
    {
      //create configuration
      var mockConfig = new Mock<IConfiguration>();

      //act create addresslogic object with config
      var mockAddressLogic = new Mock<AddressLogic>(mockConfig);
    }

    [Fact]
    public async Task CheckAddressFormatting()
    {
      Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
      AddressLogic validator = new AddressLogic(mockConfig.Object);

      Address.Lib.Address newAddy = new Address.Lib.Address
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
    public async Task CreateGoogleApiUrl()
    {
      Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
      AddressLogic validator = new AddressLogic(mockConfig.Object);

      Address.Lib.Address newAddy = new Address.Lib.Address
      {
        Id = new Guid("566e1a61-c283-4d33-9b9b-9a981393cf2b"),
        Street = "1100 N E St",
        City = "Arlington",
        State = "Texas",
        Country = "US",
        ZipCode = "76010"
      };
      var address = validator.FormatAddress(newAddy);
      var result = validator.GetGoogleApiUrl(address,address);
      Assert.Equal($"?units=imperial&origins={address}&destinations={address}&key=", result);

    }


    
  }
}




