using Revature.Account.Lib.Model;
using System;
using Xunit;

namespace Revature.Account.Test.Logic_Tests.Model
{
  /// <summary>
  /// Tests for the business-logic-layer representation for the Coordinator.
  /// </summary>
  public class CoordinatorTesting
  {
    //declare an instance for testing
    private CoordinatorAccount coordinator = new CoordinatorAccount();

    /// <summary>
    /// Test if the Coordinator's name is null.
    /// </summary>

    [Fact]
    public void CoordinatorNameNullException()
    {
      string nullString = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.Name = nullString);
    }

    /// <summary>
    /// Test if a Coordinator's name is an empty-string.
    /// </summary>

    [Fact]
    public void CoordinatorNameEmptyException()
    {
      string emptyString = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.Name = emptyString);
    }

    /// <summary>
    /// Test if the given Coordinator's email is of a valid format.
    /// </summary>
    [Fact]
    public void CoordinatorEmailException()
    {
      string invalidEmail = "abcgmail.com";

      Assert.ThrowsAny<FormatException>(() => coordinator.Email = invalidEmail);
    }

    /// <summary>
    /// Test if the Coordinator's training center name is null.
    /// </summary>
    [Fact]
    public void CoordinatorTCNameNullException()
    {
      string invalidName = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.TrainingCenterName = invalidName);
    }

    /// <summary>
    /// Test if the Coordinator's training center is a blank-string.
    /// </summary>
    [Fact]
    public void CoordinatorTCNameBlankException()
    {
      string invalidName = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.TrainingCenterName = invalidName);
    }

    /// <summary>
    /// Test if the coordinator's training-center's address is null.
    /// </summary>
    [Fact]
    public void CoordinatorTCAddrNullException()
    {
      string invalidAddr = null;

      Assert.ThrowsAny<ArgumentNullException>(() => coordinator.TrainingCenterAddress = invalidAddr);
    }

    /// <summary>
    /// Test if the coordinator's training-center's address is a blank-string.
    /// </summary>
    [Fact]
    public void CoordinatorTCAddrBlankException()
    {
      string invalidAddr = "";

      Assert.ThrowsAny<ArgumentException>(() => coordinator.TrainingCenterName = invalidAddr);
    }


  }
}
