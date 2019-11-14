using System.Collections.Generic;
using System.Linq;

namespace Xyz.Provider.DataAccess
{
  public static class Mapper
  {
    // Have nullable values for direct API input (for development)

    public static string Password { get; } = "Pa$$@W0rd!";

    // Mapping Address
    public static Lib.Models.Address Map(Entities.Address addr) =>
      addr is null ? null : new Lib.Models.Address
      {
        AddressId = addr.AddressId,
        StreetAddress = addr.StreetAddress,
        City = addr.City,
        State = addr.State,
        Zip = addr.Zip
      };

    public static Entities.Address Map(Lib.Models.Address addr) => new Entities.Address
    {
      AddressId = addr.AddressId,
      StreetAddress = addr.StreetAddress,
      City = addr.City,
      State = addr.State,
      Zip = addr.Zip
    };

    // Mapping Amenity
    public static Lib.Models.Amenity Map(Entities.Amenity amen) =>
      amen is null ? null : new Lib.Models.Amenity
      {
        AmenityType = amen.AmenityType,
        AmenityId = amen.AmenityId
      };

    public static Entities.Amenity Map(Lib.Models.Amenity amen) => new Entities.Amenity
    {
      AmenityType = amen.AmenityType,
      AmenityId = amen.AmenityId
    };

    // Mapping Complex
    public static Lib.Models.Complex Map(Entities.Complex comp)
    {
      if (comp is null) { return null; }
      var result = new Lib.Models.Complex
      {
        ComplexName = comp.ComplexName,
        ContactNumber = comp.ContactNumber
      };
      if (comp.ComplexId != 0)
      {
        result.ComplexId = comp.ComplexId;
      }
      if (comp.Address != null)
      {
        result.Address = Map(comp.Address);
      }
      if (comp.Provider != null)
      {
        result.Provider = Map(comp.Provider);
      }
      return result;
    }

    public static Entities.Complex Map(Lib.Models.Complex comp) => new Entities.Complex
    {
      ComplexId = (comp.ComplexId != 0) ? comp.ComplexId : 0,
      AddressId = comp.Address?.AddressId ?? 0,
      ProviderId = comp.Provider?.ProviderId ?? 0,
      ComplexName = comp.ComplexName,
      ContactNumber = comp.ContactNumber,
    };

    // Mapping Gender
    public static Lib.Models.Gender Map(Entities.Gender gen) =>
      gen is null ? null : new Lib.Models.Gender
      {
        GenderId = gen.GenderId,
        GenderType = gen.GenderType
      };

    public static Entities.Gender Map(Lib.Models.Gender gen) => new Entities.Gender
    {
      GenderId = gen.GenderId,
      GenderType = gen.GenderType
    };

    // Mapping Notification
    public static Lib.Models.Notification Map(Entities.Notification notif)
    {
      if (notif is null) { return null; }
      var result = new Lib.Models.Notification
      {
        NotificationId = notif.NotificationId,
        Title = notif.Title,
        Reason = notif.Reason
      };
      if (notif.Provider != null)
      {
        result.Provider = Map(notif.Provider);
      }
      if (notif.Room != null)
      {
        result.Room = Map(notif.Room);
      }
      return result;
    }

    public static Entities.Notification Map(Lib.Models.Notification notif) => new Entities.Notification
    {
      NotificationId = notif.NotificationId,
      ProviderId = notif.Provider?.ProviderId ?? 0,
      RoomId = notif.Room?.RoomId ?? 0,
      Title = notif.Title,
      Reason = notif.Reason
    };

    // Provider
    public static Lib.Models.Provider Map(Entities.Provider pro)
    {
      if (pro is null) { return null; }
      var result = new Lib.Models.Provider
      {
        ProviderId = pro.ProviderId,
        CompanyName = pro.CompanyName,
        Username = pro.Username,
        Password = pro.Password,
        ContactNumber = pro.ContactNumber
      };
      if (pro.Center != null)
      {
        result.Center = Map(pro.Center);
      }
      if (pro.Address != null)
      {
        result.Address = Map(pro.Address);
      }
      return result;
    }

    public static Entities.Provider Map(Lib.Models.Provider pro) => new Entities.Provider
    {
      ProviderId = pro.ProviderId,
      CenterId = pro.Center?.CenterId ?? 0,
      AddressId = pro.Address?.AddressId ?? 0,
      CompanyName = pro.CompanyName,
      Username = pro.Username,
      Password = pro.Password,
      ContactNumber = pro.ContactNumber
    };

    // Room
    public static Lib.Models.Room Map(Entities.Room room)
    {
      if (room is null) { return null; }
      var result = new Lib.Models.Room
      {
        RoomId = room.RoomId,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        StartDate = room.StartDate,
        EndDate = room.EndDate,
        HasAmenity = room.RoomAmenity.Any()
      };
      if (room.Complex != null)
      {
        result.Complex = Map(room.Complex);
      }
      if (room.Address != null)
      {
        result.Address = Map(room.Address);
      }
      if (room.Type != null)
      {
        result.RoomType = Map(room.Type);
      }
      if (room.Gender != null)
      {
        result.Gender = Map(room.Gender);
      }
      result.NumberOfOccupants = room.NumberOfOccupants;
      return result;
    }

    public static Entities.Room Map(Lib.Models.Room room) => new Entities.Room
    {
      RoomId = (room.RoomId != 0) ? room.RoomId : 0,
      ComplexId = room.Complex?.ComplexId ?? 0,
      AddressId = room.Address?.AddressId ?? 0,
      TypeId = room.RoomType?.TypeId ?? 0,
      GenderId = room.Gender?.GenderId ?? 0,
      RoomNumber = room.RoomNumber,
      NumberOfBeds = room.NumberOfBeds,
      NumberOfOccupants = room.NumberOfOccupants,
      StartDate = room.StartDate,
      EndDate = room.EndDate,
    };

    // TrainingCenter
    public static Lib.Models.TrainingCenter Map(Entities.TrainingCenter train)
    {
      if (train is null) { return null; }
      var result = new Lib.Models.TrainingCenter
      {
        CenterId = train.CenterId,
        CenterName = train.CenterName,
        ContactNumber = train.ContactNumber
      };
      if (train.Address != null)
      {
        result.Address = Map(train.Address);
      }
      return result;
    }

    public static Entities.TrainingCenter Map(Lib.Models.TrainingCenter train) => new Entities.TrainingCenter
    {
      CenterId = train.CenterId,
      AddressId = train.Address?.AddressId ?? 0,
      CenterName = train.CenterName,
      ContactNumber = train.ContactNumber
    };

    // RoomType
    public static Lib.Models.RoomType Map(Entities.RoomType type) =>
      type is null ? null : new Lib.Models.RoomType
      {
        TypeId = type.TypeId,
        Type = type.Type
      };

    public static Entities.RoomType Map(Lib.Models.RoomType type) => new Entities.RoomType
    {
      TypeId = type.TypeId,
      Type = type.Type
    };

    // Mapping Lists
    public static IEnumerable<Lib.Models.Provider> Map(IEnumerable<Entities.Provider> pro) =>
      pro.Select(Map);
    public static IEnumerable<Entities.Provider> Map(IEnumerable<Lib.Models.Provider> pro) =>
      pro.Select(Map);

    public static IEnumerable<Lib.Models.Amenity> Map(IEnumerable<Entities.Amenity> pro) =>
      pro.Select(Map);
    public static IEnumerable<Entities.Amenity> Map(IEnumerable<Lib.Models.Amenity> pro) =>
      pro.Select(Map);

    public static IEnumerable<Lib.Models.TrainingCenter> Map(IEnumerable<Entities.TrainingCenter> tra) =>
      tra.Select(Map);
    public static IEnumerable<Entities.TrainingCenter> Map(IEnumerable<Lib.Models.TrainingCenter> tra) =>
      tra.Select(Map);
  }
}
