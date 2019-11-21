using System.Collections.Generic;
using System.Threading.Tasks;

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
    /// <param name="Room"></param>
    /// <returns></returns>
    public Lib.Room ParseRoom(Entities.Room Room);

    /// <summary>
    /// Method that converts a Business Logic Room Object to a Entities Room Object
    /// </summary>
    /// <param name="Room"></param>
    /// <returns></returns>
    public Task<Entities.Room> ParseRoomAsync(Lib.Room Room);

    /// <summary>
    /// Method that converts a list of Entities Room Objects to Business Logic Room Object
    /// </summary>
    /// <param name="roomsFromDB"></param>
    /// <returns></returns>
    IEnumerable<Lib.Room> ParseRooms(IEnumerable<Entities.Room> roomsFromDB);
  }
}
