using System.Collections.Generic;
using System.Threading.Tasks;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Lib.Interface
{
  public interface IRoomRepository : IRepository<Room>
  {
    Task<IEnumerable<Room>> GetAllRoomsByComplexIdAsync(int complexId);
    Task<IEnumerable<Room>> GetAllRoomsByProviderIdAsync(int providerId);
    Task<Room> AddRoomWithAddressAsync(Address newAddress, Room newRoom, int providerId);
    Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();
    Task<IEnumerable<Amenity>> GetAllRoomAmenitiesAsync();
  }
}
