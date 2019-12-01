using System;
using System.Text.RegularExpressions;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Model for the messages being sent to the room service
  /// </summary>
  public class RoomMessage
  {
    private Guid _roomId;
    private string _gender;
    private int _operationType;
    /// <summary>
    /// RoomId of tenant
    /// </summary>
    public Guid RoomId
    {
      get => _roomId;
      set
      {
        if (value == Guid.Empty) throw new ArgumentException("Room Id must be valid");
        _roomId = value;
      }
    }

    /// <summary>
    /// Gender of tenant
    /// </summary>
    /// <remarks>Needed for when the room gender is null on the room service</remarks>
    public string Gender
    {
      get => _gender;
      set
      {
        if (value == null || Regex.IsMatch(value, @"\s+") || value == "") throw new ArgumentException("Gender should not be null or empty");
        _gender = value;
      }
    }

    /// <summary>
    /// Two types: 0 for assigning a tenant to a room, 1 for unassigning a tenant to a room
    /// </summary>
    public int OperationType
    {
      get => _operationType;
      set {
        if (value < 0 || value > 1) throw new ArgumentException("Operation type unknown");
        _operationType = value;
      }
    }
  }
}
