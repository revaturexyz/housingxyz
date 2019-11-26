using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using BL = Revature.Room.Lib;

namespace Revature.Room.Tests.Revature.Room.Lib.Tests
{

  public class TenantMessageTest
  {
    public Guid newRoomId = Guid.Parse("349e5358-169a-4bc6-aa0f-c054952456dd");

    /// <summary>
    /// A test to ensure that creating a tenant message which includes a tuple of < Guid, string >
    /// and an operation type is successful in creating
    /// </summary>
    public void TenantMessageShouldCreate()
    {
      Tuple<Guid, string> tenantToInsert = new Tuple<Guid, string>(newRoomId, "Male");

      var newTenantMessage = new BL.Models.TenantMessage()
      {
        Tenant = tenantToInsert,

        OperationType = BL.Models.OperationType.Create
      };

    }
  }
}
