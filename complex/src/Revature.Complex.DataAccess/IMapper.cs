
namespace Revature.Complex.DataAccess
{
  public interface IMapper
  {
    Entities.AmenityComplex MapAmenityComplextoE(Lib.Models.AmenityComplex ac);
    Entities.AmenityRoom MapAmenityRoomtoE(Lib.Models.AmenityRoom ar);
    Entities.Amenity MapAmenitytoE(Lib.Models.Amenity amenity);
    Entities.Complex MapComplextoE(Lib.Models.Complex c);
    Lib.Models.Amenity MapEtoAmenity(Entities.Amenity amenity);
    Lib.Models.AmenityComplex MapEtoAmenityComplex(Entities.AmenityComplex ac);
    Lib.Models.AmenityRoom MapEtoAmenityRoom(Entities.AmenityRoom ar);
    Lib.Models.Complex MapEtoComplex(Entities.Complex c);
  }
}