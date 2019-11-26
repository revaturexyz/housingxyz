using System;
using System.Collections.Generic;

namespace Revature.Tenant.Lib.Models
{
  /// <summary>
  /// Class that contains necessary information about the room and its residents
  /// </summary>
  /// <exception cref="ArgumentException">Thrown when number of beds is not positive</exception>
  public class RoomInfo
  {
    /// <summary>
    /// Full capacity of room
    /// </summary>
    private int _numberOfBeds;

    /// <summary>
    /// RoomId from Room Service
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Full capacity of room, checks if the full capacity is positive
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the number of beds is not positive</exception>
    public int NumberOfBeds
    {
      get => _numberOfBeds;
      set
      {
        if (value < 1) throw new ArgumentException("Number of beds must be greater than 0");
        _numberOfBeds = value;
      }
    }

    /// <summary>
    /// List of tenants and associated information of room occupants
    /// </summary>
    public List<Tenant> Tenants { get; set; }
  }
}
