using System;
using System.Collections.Generic;
using System.Text;
using Entity = Revature.Complex.DataAccess.Models;
using Logic = Revature.Complex.Lib.Models;

namespace Revature.Complex.Lib
{
  public interface IMapper
  {
    public abstract Entity.Amenity MapAmenitytoE(Logic.Amenity amenity);
    public Logic.Amenity MapEtoAmenity(Entity.Amenity amenity);
    public Entity.AmenityComplex MapAmenityComplextoE(Logic.AmenityComplex ac);
    public Logic.AmenityComplex MapEtoAmenityComplex(Entity.AmenityComplex ac);
    public Entity.AmenityRoom MapAmenityRoomtoE(Logic.AmenityRoom ar);
    public Logic.AmenityRoom MapEtoAmenityRoom(Entity.AmenityRoom ar);
    public Entity.Complex MapComplextoE(Logic.Complex c);
    public Logic.Complex MapEtoComplex(Entity.Complex c);
  }
}
