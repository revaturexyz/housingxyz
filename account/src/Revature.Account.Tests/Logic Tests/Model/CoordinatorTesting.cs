using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Revature.Account.Lib.Model;

namespace Revature.Account.Test.Logic_Tests.Model
{
  public class CoordinatorTesting
  {
    CoordinatorAccount coordinator = new CoordinatorAccount();

    [Fact]
    public void CoordinatorNameNullException()
    {
      string nullString = null;

      Assert.Throws<ArgumentNullException>(() => coordinator.Name = nullString);
    }

    [Fact]
    public void CoordinatorNameEmptyException()
    {
      string emptyString = "";

      Assert.Throws<ArgumentException>(() => coordinator.Name = emptyString);
    }

    [Fact]
    public void CoordinatorEmailException()
    {
      string invalidEmail = "abcgmail.com";

      Assert.Throws<FormatException>(() => coordinator.Email = invalidEmail);
    }
  }
}
