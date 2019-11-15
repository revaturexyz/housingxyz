using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Xunit;
using Revature.Room.DataAccess;
using System.Threading.Tasks;
using Revature.Room.DataAccess.Entities;
using Lib = Revature.Room.Lib;
using System.Linq;

namespace Revature.Room.Tests
{
  public class UnitTest
  {

    [Fact]
    public async Task RepoReadTest()
    {
      DbContextOptions<RoomServiceContext> options = new DbContextOptionsBuilder<RoomServiceContext>().UseInMemoryDatabase("ReadRoom").Options;

      using RoomServiceContext testContext = new RoomServiceContext(options);

      var newRoom = new 

      testContext.Add();
      Repository repo = new Repository(testContext);

      await repo.CreateRoom(null);

      Revature.Room.DataAccess.Entities.Room room = testContext.Room.Select(r => r).First();



      Assert.NotNull(room);
    }

  }
}
