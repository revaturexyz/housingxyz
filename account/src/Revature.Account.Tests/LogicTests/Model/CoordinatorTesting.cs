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
  }
}
