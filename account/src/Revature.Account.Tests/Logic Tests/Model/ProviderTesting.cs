using Revature.Account.Lib.Model;
using System;
using Xunit;

namespace Revature.Account.Tests.Logic_Tests.Model
{
  public class ProviderTesting
  {
    private ProviderAccount provider = new ProviderAccount();

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
  }
}
