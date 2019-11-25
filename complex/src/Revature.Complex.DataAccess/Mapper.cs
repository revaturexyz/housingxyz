using System;
using System.Collections.Generic;
using System.Text;
using Revature.Complex.Lib.Interface;
using Logic = Revature.Complex.Lib.Models;
using Entity = Revature.Complex.DataAccess.Entities;

namespace Revature.Complex.DataAccess
{
  public class Mapper
  {
<<<<<<< Updated upstream
=======
    /// <summary>
    /// Logic.Amenity => Entity.Amenity
    /// All properties are mapped. Logic.amenity has no Lists
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
>>>>>>> Stashed changes
    public Entity.Amenity MapAmenitytoE(Logic.Amenity amenity)
    {
      return new Entity.Amenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = amenity.AmenityType,
        Description = amenity.Description
      };
    }

    /// <summary>
    /// Entity.Amenity => Logic.Amenity
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public Logic.Amenity MapEtoAmenity(Entity.Amenity amenity)
    {
      return new Logic.Amenity
      {
        AmenityId = amenity.AmenityId,
        AmenityType = amenity.AmenityType,
        Description = amenity.Description
      };
    }

    /// <summary>
    /// Logic.AmenityComplex => Entity.AmenityComplex
    /// </summary>
    /// <param name="ac"></param>
    /// <returns></returns>
    public Entity.AmenityComplex MapAmenityComplextoE(Logic.AmenityComplex ac)
    {
      return new Entity.AmenityComplex
      {
        AmenityComplexId = ac.AmenityComplexId,
        AmenityId = ac.AmenityId,
        ComplexId = ac.ComplexId
      };
    }

    /// <summary>
    /// Entity.AmenityComplex => Logic.AmenityComplex
    /// </summary>
    /// <param name="ac"></param>
    /// <returns></returns>
    public Logic.AmenityComplex MapEtoAmenityComplex(Entity.AmenityComplex ac)
    {
      return new Logic.AmenityComplex
      {
        AmenityComplexId = ac.AmenityComplexId,
        AmenityId = ac.AmenityId,
        ComplexId = ac.ComplexId
      };
    }

    /// <summary>
    /// Logic.AmenityRoom => Entity.AmenityRoom
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public Entity.AmenityRoom MapAmenityRoomtoE(Logic.AmenityRoom ar)
    {
      return new Entity.AmenityRoom
      {
        AmenityRoomId = ar.AmenityRoomId,
        AmenityId = ar.AmenityId,
        RoomId = ar.RoomId
      };
    }

    /// <summary>
    /// Entity.AmenityRoom => Logic.AmenityRoom
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public Logic.AmenityRoom MapEtoAmenityRoom(Entity.AmenityRoom ar)
    {
      return new Logic.AmenityRoom
      {
        AmenityRoomId = ar.AmenityRoomId,
        AmenityId = ar.AmenityId,
        RoomId = ar.RoomId
      };
    }

    /// <summary>
    /// Logic.Complex => Entity.Complex
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public Entity.Complex MapComplextoE(Logic.Complex c)
    {
      return new Entity.Complex
      {
        ComplexId = c.ComplexId,
        AddressId = c.AddressId,
        ProviderId = c.ProviderId,
        ComplexName = c.ComplexName,
        ContactNumber = c.ContactNumber
      };
    }

    /// <summary>
    /// Entity.Complex => Logic.Complex
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public Logic.Complex MapEtoComplex(Entity.Complex c)
    {
      return new Logic.Complex
      {
        ComplexId = c.ComplexId,
        AddressId = c.AddressId,
        ProviderId = c.ProviderId,
        ComplexName = c.ComplexName,
        ContactNumber = c.ContactNumber
      };

    }
  }
}
