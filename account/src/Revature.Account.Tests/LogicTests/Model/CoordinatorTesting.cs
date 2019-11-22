using Revature.Account.Lib.Model;
using System;
using Xunit;

namespace Revature.Account.Test.Logic_Tests.Model
{
  public class CoordinatorTesting
  {
    private CoordinatorAccount coordinator = new CoordinatorAccount();

    [Fact]
    public void CoordinatorNameNullException()
    {
      string nullString = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.Name = nullString);
    }

    [Fact]
    public void CoordinatorNameEmptyException()
    {
      string emptyString = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.Name = emptyString);
    }

    [Fact]
    public void CoordinatorEmailException()
    {
      string invalidEmail = "abcgmail.com";

      Assert.ThrowsAny<FormatException>(() => coordinator.Email = invalidEmail);
    }

    
    [Fact]
    public void CoordinatorTCNameNullException()
    {
      string invalidName = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.TrainingCenterName = invalidName);
    }

    [Fact]
    public void CoordinatorTCNameBlankException()
    {
      string invalidName = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.TrainingCenterName = invalidName);
    }

    [Fact]
    public void CoordinatorTCAddrNullException()
    {
      string invalidAddr = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.TrainingCenterAddress = invalidAddr);
    }

    [Fact]
    public void CoordinatorTCAddrBlankException()
    {
      string invalidAddr = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.TrainingCenterName = invalidAddr);
    }


  }
}
