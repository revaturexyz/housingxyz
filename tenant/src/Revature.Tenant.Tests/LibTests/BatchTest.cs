using System;
using Xunit;

namespace Revature.Tenant.Tests.LibTests
{
  public class BatchTest
  {
    /// <summary>
    /// Tests that Empty ID throw exception
    /// </summary>
    [Fact]
    public void Batch_Id_Test()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Batch { Id = -1 });
    }

    /// <summary>
    /// Tests that Empty Curriculum throw exception
    /// </summary>
    [Fact]
    public void Batch_Curriculum_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Batch { BatchCurriculum = "" });
    }

    /// <summary>
    /// Tests that Empty train center ID throw exception
    /// </summary>
    [Fact]
    public void Batch_Training_Center_Empty()
    {
      Assert.ThrowsAny<ArgumentException>(() => new Lib.Models.Batch { TrainingCenter = Guid.Empty });
    }

  }
}
