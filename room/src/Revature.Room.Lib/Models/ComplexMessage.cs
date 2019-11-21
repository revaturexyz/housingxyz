namespace Revature.Room.Lib.Models
{
  public class ComplexMessage
  {
    /// <summary>
    /// The Room you are going to send
    /// </summary>
    public Room Room { get; set; }

    /// <summary>
    /// The operation type is what CRUD operation you are sending to the Room service
    /// We are only receiving Create, Update, and Delete.
    /// For the OperationType it goes as follows
    /// Create: 0, Update: 1, Delete: 2
    /// </summary>
    public OperationType OperationType { get; set; }
  }
}
