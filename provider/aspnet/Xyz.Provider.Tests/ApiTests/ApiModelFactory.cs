using System.Collections.Generic;
using System.Linq;

using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.ApiTests
{
  /// <summary>
  /// ApiModelFactory takes business library models and
  /// converts them into ApiModels
  /// </summary>
  public static class ApiModelFactory
  {
    public static ApiAddress MakeApiAddress(Address address)
    {
      if (address == null) { return null; }
      return new ApiAddress
      {
        AddressId = (address?.AddressId).GetValueOrDefault(),
        StreetAddress = address?.StreetAddress,
        City = address?.City,
        State = address?.State,
        ZipCode = address?.Zip
      };
    }

    public static ApiAmenity MakeApiAmenity(Amenity amenity)
    {
      return new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        Amenity = amenity.AmenityType
      };
    }

    public static ApiComplex MakeApiComplex(Complex complex, bool includeRooms = false)
    {
      if (complex == null) { return null; }
      return new ApiComplex
      {
        ComplexId = complex.ComplexId,
        ComplexName = complex.ComplexName,
        ContactNumber = complex.ContactNumber,
        ApiAddress = MakeApiAddress(complex.Address),
        ApiProvider = MakeApiProvider(complex.Provider),
        ApiTrainingCenter = MakeApiTrainingCenter(complex.Center),
        ApiRooms = includeRooms ? complex.Rooms.Select(MakeApiRoom).ToList()
                                : new List<ApiRoom>()
      };
    }

    public static ApiGender MakeApiGender(Gender gender)
    {
      return new ApiGender
      {
        GenderId = gender.GenderId,
        GenderType = gender.GenderType
      };
    }

    public static ApiNotification MakeApiNotification(Notification notification)
    {
      return new ApiNotification
      {
        NotificationId = notification.NotificationId,
        Title = notification.Title,
        Reason = notification.Reason,
        ProviderId = notification.Provider.ProviderId,
        RoomId = notification.Room.RoomId
      };
    }

    public static ApiProvider MakeApiProvider(Lib.Models.Provider provider)
    {
      if (provider == null) { return null; }
      return new ApiProvider
      {
        ProviderId = provider.ProviderId,
        CompanyName = provider.CompanyName,
        ContactNumber = provider.ContactNumber,
        Username = provider.Username,
        Password = provider.Password,
        ApiAddress = MakeApiAddress(provider.Address),
      };
    }

    public static ApiRoom MakeApiRoom(Room room)
    {
      if (room == null) { return null; }
      return new ApiRoom
      {
        RoomId = room.RoomId,
        RoomNumber = room.RoomNumber,
        NumberOfBeds = room.NumberOfBeds,
        IsOccupied = room.NumberOfOccupants > 0,
        StartDate = room.StartDate,
        EndDate = room.EndDate,
        HasAmenity = room.HasAmenity,
        ApiComplex = MakeApiComplex(room.Complex),
        ApiAddress = MakeApiAddress(room.Address),
        ApiGender = MakeApiGender(room.Gender),
        ApiRoomType = MakeApiRoomType(room.RoomType),
        ApiAmenity = room.Amenities.Select(MakeApiAmenity).ToList()
      };
    }

    public static ApiRoomType MakeApiRoomType(RoomType type)
    {
      return new ApiRoomType
      {
        TypeId = type.TypeId,
        RoomType = type.Type
      };
    }

    public static ApiTrainingCenter MakeApiTrainingCenter(TrainingCenter center)
    {
      if (center == null) { return null; }
      return new ApiTrainingCenter
      {
        CenterId = center.CenterId,
        CenterName = center.CenterName,
        ContactNumber = center.ContactNumber,
        ApiAddress = MakeApiAddress(center.Address),
        ApiProvider = center.Providers?.Select(MakeApiProvider).ToList()
      };
    }
  }
}
