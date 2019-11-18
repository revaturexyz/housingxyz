using Revature.Account.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Revature.Account.Tests.Logic_Tests.Model
{
  public class ProviderTesting
  {
    ProviderAccount provider = new ProviderAccount();

    [Fact]
    public void ProviderNameNullException()
    {
      string nullString = null;

      Assert.Throws<ArgumentNullException>(() => provider.Name = nullString);
    }

    [Fact]
    public void ProviderNameEmptyException()
    {
      string emptyString = "";

      Assert.Throws<ArgumentException>(() => provider.Name = emptyString);
    }

    [Fact]
    public void ProviderPasswordEmptyException()
    {
      string emptyString = "";

      Assert.Throws<ArgumentException>(() => provider.Password = emptyString);
    }

  }
}
