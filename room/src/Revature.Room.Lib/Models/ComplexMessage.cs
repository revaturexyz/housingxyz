namespace Revature.Room.Lib.Models
{
  public class ComplexMessage
  {
    /// <summary>
    /// The room you are going to send
    /// </summary>
    public Room room { get; set; }

    /// <summary>
    /// The operation type is what CRUD operation you are sending to the Room service
    /// We are only receiving Create, Update, and Delete.
    /// For the operationType it goes as follows
    /// Create: 0, Update: 1, Delete: 2
    /// </summary>
    public int operationType { get; set; }
  }
}
