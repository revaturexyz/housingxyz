using System;
using System.Collections.Generic;
using Logic = Revature.Complex.Lib.Models;
using System.Threading.Tasks;

namespace Revature.Complex.Lib.Interface
{
  public interface IRepository
  {
    public Task<string> CreateComplexAsync(Logic.Complex lComplex);
    public IEnumerable<Logic.Complex> ReadComplexList();
    public Task<Logic.Complex> ReadComplexAsync(Guid complexId);
    public Task<string> UpdateComplexAsync(Logic.Complex update);
    public string DeleteComplex(Guid complexId);
    public Task<string> CreateAmenityRoomAsync(Logic.AmenityRoom ar);
    public Task<string> CreateAmenityComplexAsync(Logic.AmenityComplex ac);
    public Task<string> CreateAmenityAsync(Logic.Amenity amenity);
    public IEnumerable<Logic.Amenity> ReadAmenityList();
    public IEnumerable<Logic.Amenity> ReadAmenityListByComplexId(Guid guid);
    public IEnumerable<Logic.Amenity> ReadAmenityListByRoomId(Guid roomId);


  }
}
