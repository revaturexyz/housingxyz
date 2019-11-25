using Xunit;
using Revature.Tenant.DataAccess.Repository;
using Revature.Tenant.DataAccess;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Revature.Tenant.Tests.DataTests
{
  /// <summary>
  /// Unit tests for data access methods in TenantRoomRepository class
  /// </summary>
  public class TenantRoomRepositoryTest
  {
    /// <summary>
    /// GetTenantsByRoomId SHould Return a List of Tenants
    /// </summary>
    [Fact]
    public async Task GetTenantsByRoomIdShouldReturnList()
    {
      var options = TestDbInitializer.InitializeDbOptions("GetTenantsByRoomIdShouldReturnList");
      using var _context = TestDbInitializer.CreateTestDb(options);
      var mapper = new Mapper();
      var repo = new TenantRoomRepository(_context, mapper);

      var roomId = Guid.NewGuid();

      var expected = typeof(List<Lib.Models.Tenant>);
      var actual = await repo.GetTenantsByRoomId(roomId);

      Assert.IsType(expected, actual);
    }
  }
}
