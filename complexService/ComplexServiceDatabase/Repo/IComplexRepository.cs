using System;
using System.Collections.Generic;
using Logic = ComplexServiceLogic.Model;
using System.Threading.Tasks;

namespace ComplexServiceDatabase.Repo
{
    public interface IComplexRepository
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
        public int ReadAmenittiesbyString(string amentityType);



    }
}
