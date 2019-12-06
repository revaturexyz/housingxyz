using System;
using Revature.Account.Lib.Model;
using Xunit;

namespace Revature.Account.Test.Logic_Tests.Model
{
  /// <summary>
  /// Tests for the business logic layer representation for the Coordinator.
  /// </summary>
  public class CoordinatorTesting
  {
    //declare an instance for testing
    private readonly CoordinatorAccount _coordinator = new CoordinatorAccount();

    /// <summary>
    /// Test if the Coordinator's name is null.
    /// </summary>

    [Fact]
    public void CoordinatorNameNullException()
    {
      string nullString = null;

      Assert.ThrowsAny<ArgumentNullException>(() => _coordinator.Name = nullString);
    }

    /// <summary>
    /// Test if a Coordinator's name is an empty-string.
    /// </summary>
    [Fact]
    public void CoordinatorNameEmptyException()
    {
      var emptyString = "";

      Assert.ThrowsAny<ArgumentException>(() => _coordinator.Name = emptyString);
    }

    /// <summary>
    /// Test if the given Coordinator's email is of a valid format.
    /// </summary>
    [Fact]
    public void CoordinatorEmailException()
    {
      var invalidEmail = "abcgmail.com";

      Assert.ThrowsAny<FormatException>(() => _coordinator.Email = invalidEmail);
    }

    /// <summary>
    /// Test if the Coordinator's training center name is null.
    /// </summary>
    [Fact]
    public void CoordinatorTCNameNullException()
    {
      string invalidName = null;

      Assert.ThrowsAny<ArgumentNullException>(() => _coordinator.TrainingCenterName = invalidName);
    }

    /// <summary>
    /// Test if the Coordinator's training center is a blank-string.
    /// </summary>
    [Fact]
    public void CoordinatorTCNameBlankException()
    {
      var invalidName = "";

      Assert.ThrowsAny<ArgumentException>(() => _coordinator.TrainingCenterName = invalidName);
    }

    /// <summary>
    /// Test if the coordinator's training center's address is null.
    /// </summary>
    [Fact]
    public void CoordinatorTCAddrNullException()
    {
      string invalidAddr = null;

      Assert.ThrowsAny<ArgumentNullException>(() => _coordinator.TrainingCenterAddress = invalidAddr);
    }

    /// <summary>
    /// Test if the coordinator's training center's address is a blank-string.
    /// </summary>
    [Fact]
    public void CoordinatorTCAddrBlankException()
    {
      var invalidAddr = "";

      Assert.ThrowsAny<ArgumentException>(() => _coordinator.TrainingCenterName = invalidAddr);
    }
  }
}
