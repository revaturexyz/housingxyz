using System.Collections.Generic;

namespace Revature.Room.DataAccess
{
  /// <summary>
  /// Interface for a mapper that maps between DB Entity objects and Business Logic Objects and vice versa
  /// </summary>
  public interface IMapper
  {
    /// <summary>
    /// Method that converts a Entities Room Object to a Business Logic Room Object
    /// </summary>
    /// <param name="roomEntity"></param>
    /// <returns></returns>
    public Lib.Room ParseRoom(Entities.Room roomEntity);

    /// <summary>
    /// Method that converts a Business Logic Room Object to a Entities Room Object
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public Entities.Room ParseRoom(Lib.Room room);

    /// <summary>
    /// Method that converts a list of Entities Room Objects to Business Logic Room Object
    /// </summary>
    /// <param name="roomsFromDB"></param>
    /// <returns></returns>
    IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB);
  }
}
