namespace Revature.Room.Lib.Models
{
  /// <summary>
  /// The operation type is what CRUD operation you are sending to the Room service
  /// We are only receiving Create, Update, and Delete.
  /// For the OperationType it goes as follows
  /// Create: 0, Update: 1, Delete: 2
  /// </summary>
  public enum OperationType
  {
    Create, Delete, Update
  }
}
