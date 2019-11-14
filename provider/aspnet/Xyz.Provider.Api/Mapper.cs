using Xyz.Provider.Api.Models;

namespace Xyz.Provider.Api
{
  public static class Mapper
  {
    // Mapping Address
    #region Address Mapper Methods

    public static Lib.Models.Address Map(ApiAddress addr)
    {
      if (addr is null)
      {
        return null;
      }
      else
      {
        var result = new Lib.Models.Address
        {
          AddressId = addr.AddressId,
        };
        if (!string.IsNullOrEmpty(addr.StreetAddress))
        {
          result.StreetAddress = addr.StreetAddress;
        }
        if (!string.IsNullOrEmpty(addr.City))
        {
          result.City = addr.City;
        }
        if (!string.IsNullOrEmpty(addr.State))
        {
          result.State = addr.State;
        }
        if (!string.IsNullOrEmpty(addr.ZipCode))
        {
          result.Zip = addr.ZipCode;
        }
        return result;
      }
    }

    public static ApiAddress Map(Lib.Models.Address addr) => new ApiAddress
    {
      AddressId = addr.AddressId,
      StreetAddress = addr.StreetAddress,
      City = addr.City,
      State = addr.State,
      ZipCode = addr.Zip
    };

    #endregion

    // Mapping Gender
    #region Gender Mapper Methods

    public static Lib.Models.Gender Map(ApiGender gen) =>
      gen is null ? null : new Lib.Models.Gender
      {
        GenderId = gen.GenderId,
        GenderType = gen.GenderType
      };

    public static ApiGender Map(Lib.Models.Gender gen) => new ApiGender
    {
      GenderId = gen.GenderId,
      GenderType = gen.GenderType
    };

    #endregion

    // Mapping RoomType
    #region RoomType Mapper Methods

    public static Lib.Models.RoomType Map(ApiRoomType rType) =>
      rType is null ? null : new Lib.Models.RoomType
      {
        Type = rType.RoomType,
        TypeId = rType.TypeId
      };

    public static ApiRoomType Map(Lib.Models.RoomType rType) => new ApiRoomType
    {
      RoomType = rType.Type,
      TypeId = rType.TypeId
    };

    #endregion

    // Mapping Amenity
    #region Amenity Mapper Methods

    public static Lib.Models.Amenity Map(ApiAmenity amen) =>
      amen is null ? null : new Lib.Models.Amenity
      {
        AmenityId = amen.AmenityId,
        AmenityType = amen.Amenity
      };

    public static ApiAmenity Map(Lib.Models.Amenity amen) => new ApiAmenity
    {
      AmenityId = amen.AmenityId,
      Amenity = amen.AmenityType
    };

    #endregion
  }
}
