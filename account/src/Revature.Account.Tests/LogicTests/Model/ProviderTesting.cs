using Revature.Account.Lib.Model;
using System;
using Xunit;

namespace Revature.Account.Tests.Logic_Tests.Model
{
  /// <summary>
  /// Tests for the business-logic-layer representation for the Provider.
  /// </summary>
  public class ProviderTesting
  {
    //instantiate a test-object
    private ProviderAccount provider = new ProviderAccount();

    /// <summary>
    /// Test if the Provider's name is null.
    /// </summary>
    [Fact]
    public void ProviderNameNullException()
    {
      string nullString = null;

      Assert.ThrowsAny<ArgumentNullException>(() => provider.Name = nullString);
    }

    /// <summary>
    /// Test if the Provider's name is a blank-string.
    /// </summary>
    [Fact]
    public void ProviderNameEmptyException()
    {
      string emptyString = "";

      Assert.ThrowsAny<ArgumentException>(() => provider.Name = emptyString);
    }
  }
}
