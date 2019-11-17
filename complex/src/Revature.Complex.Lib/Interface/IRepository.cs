using System;
using System.Collections.Generic;
using Logic = Revature.Complex.Lib.Models;
using System.Threading.Tasks;

namespace Revature.Complex.Lib.Interface
{
  public interface IRepository
  {
    public Task<string> CreateComplexAsync(Logic.Complex lComplex);
    public Task<List<Logic.Complex>> ReadComplexListAsync();
    public Task<Logic.Complex> ReadComplexAsync(Guid complexId);
    public Task<string> UpdateComplexAsync(Logic.Complex update);
    public Task<string> DeleteComplexAsync(Guid complexId);
    public Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar);
    public Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac);
    public Task<string> CreateAmenityAsync(Logic.Amenity amenity);
    public Task<List<Logic.Amenity>> ReadAmenityListAsync();
    public Task<List<Logic.Amenity>> ReadAmenityListByComplexIdAsync(Guid guid);
    public Task<List<Logic.Amenity>> ReadAmenityListByRoomIdAsync(Guid roomId);


  }
}
