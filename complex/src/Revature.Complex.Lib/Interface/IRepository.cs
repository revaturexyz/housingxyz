using System;
using System.Collections.Generic;
using Logic = Revature.Complex.Lib.Models;
using System.Threading.Tasks;

namespace Revature.Complex.Lib.Interface
{
  public interface IRepository
  {
    /// <summary>
    /// Create new single complex in the database by logic complex object
    /// </summary>
    /// <param name="lComplex"></param>
    /// <returns></returns>
    public Task<string> CreateComplexAsync(Logic.Complex lComplex);

    /// <summary>
    /// Read all existed complices in the database
    /// </summary>
    /// <returns></returns>
    public Task<List<Logic.Complex>> ReadComplexListAsync();

    /// <summary>
    /// Read single Logic complex object from complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public Task<Logic.Complex> ReadComplexByIdAsync(Guid complexId);

    /// <summary>
    /// Read single logic complex object from complex name and complex contact number
    /// </summary>
    /// <param name="name"></param>
    /// <param name="phone"></param>
    /// <returns></returns>
    public Task<Logic.Complex> ReadComplexByNameAndNumberAsync(string name, string phone);

    /// <summary>
    /// Update existed single complex by passing logic complex object
    /// </summary>
    /// <param name="update"></param>
    /// <returns></returns>
    public Task<string> UpdateComplexAsync(Logic.Complex update);

    /// <summary>
    /// Delete existed single complex from database by specific complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public Task<string> DeleteComplexAsync(Guid complexId);

    /// <summary>
    /// Create new single Amenities of Room in database by amenityroom object
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar);

    /// <summary>
    /// Create new single Amenities of Room in database by logic amenitycomplex object
    /// </summary>
    /// <param name="ac"></param>
    /// <returns></returns>
    public Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac);


    /// <summary>
    /// Create new single Amenity in database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public Task<string> CreateAmenityAsync(Logic.Amenity amenity);

    /// <summary>
    /// Read all existed amenities from the database
    /// </summary>
    /// <returns></returns>
    public Task<List<Logic.Amenity>> ReadAmenityListAsync();

    /// <summary>
    /// Read amenity list for specific complex from database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public Task<List<Logic.Amenity>> ReadAmenityListByComplexIdAsync(Guid complexId);

    /// <summary>
    /// Read amenity list for specific room from database by room Id 
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public Task<List<Logic.Amenity>> ReadAmenityListByRoomIdAsync(Guid roomId);

    /// <summary>
    /// Read complex list for specific provider from database by provider Id
    /// </summary>
    /// <param name="providerId"></param>
    /// <returns></returns>
    public Task<List<Logic.Complex>> ReadComplexByProviderIdAsync(Guid providerId);

    /// <summary>
    /// Update existed single amenity info in the database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public Task<string> UpdateAmenityAsync(Logic.Amenity amenity);

    /// <summary>
    /// Delete existed single amenity info in the database by logic amenity object
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public Task<string> DeleteAmenityAsync(Logic.Amenity amenity);

    /// <summary>
    /// Delete ALL amenity record from Amenity of room in database by room Id
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    public Task<string> DeleteAmenityRoomAsync(Guid roomId);

    /// <summary>
    /// Delete ALL amenity record from Amenity of complex in database by complex Id
    /// </summary>
    /// <param name="complexId"></param>
    /// <returns></returns>
    public Task<string> DeleteAmenityComplexAsync(Guid complexId);
  }
}
