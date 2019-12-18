using System;
using Revature.Account.Lib.Model;
using Xunit;

namespace Revature.Account.Tests.Logic_Tests.Model
{
  /// <summary>
  /// Tests for the business logic layer representation for the Provider.
  /// </summary>
  public class ProviderTesting
  {
    //instantiate a test object
    private readonly ProviderAccount _provider = new ProviderAccount();

    /// <summary>
    /// Test if the Provider's name is null.
    /// </summary>
    [Fact]
    public void ProviderNameNullException()
    {
      string nullString = null;

      Assert.ThrowsAny<ArgumentNullException>(() => _provider.Name = nullString);
    }

    /// <summary>
    /// Test if the Provider's name is a blank string.
    /// </summary>
    [Fact]
    public void ProviderNameEmptyException()
    {
      var emptyString = "";

      Assert.ThrowsAny<ArgumentException>(() => _provider.Name = emptyString);
    }
  }
}
