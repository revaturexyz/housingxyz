using System;

namespace Revature.Room.Lib.Models
{
  public class TenantMessage
  {
    /// <summary>
    /// The message we will receive from the tenant, which will be the room Id and the gender in the form
    /// of a tuple
    /// </summary>
    public Guid RoomId { get; set; }

    public string Gender { get; set; }

    /// <summary>
    /// Based on the operation type ( 0 = Create, 1 = Delete ), we will react accordinly in the ServiceBusConsumer
    /// If 0 then we will add a occupant to the room, if 1 then we will remove a occupant from a room
    /// </summary>
    public OperationType OperationType { get; set; }
  }
}
