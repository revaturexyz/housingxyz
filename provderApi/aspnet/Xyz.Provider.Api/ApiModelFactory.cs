using System.Collections.Generic;
using System.Linq;
using Xyz.Provider.Api.Models;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Api
{
  /// <summary>
  /// MAPPER, for testing
  /// methods that return an API model of a parameterized Library model
  /// </summary>
  public static class ApiModelFactory
  {
    /// <summary>
    /// Takes Library Address model and makes an API Address model
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public static ApiAddress MakeApiAddress(Address address)
    {
      if (address is null) { return null; }
      return new ApiAddress
      {
        AddressId = (address?.AddressId).GetValueOrDefault(),
        StreetAddress = address?.StreetAddress,
        City = address?.City,
        State = address?.State,
        ZipCode = address?.Zip
      };
    }

    /// <summary>
    /// Takes Library Amenity model and makes an API Amenity model
    /// </summary>
    /// <param name="amenity"></param>
    /// <returns></returns>
    public static ApiAmenity MakeApiAmenity(Amenity amenity)
    {
      return new ApiAmenity
      {
        AmenityId = amenity.AmenityId,
        Amenity = amenity.AmenityType
      };
    }

    /// <summary>
    /// Takes Library Complex model and makes an API Complex model
    /// </summary>
    /// <param name="complex"></param>
    /// <param name="includeRooms"></param>
    /// <returns></returns>
    public static ApiComplex MakeApiComplex(Complex complex, bool includeRooms = false)
    {
      if (complex is null) { return null; }
      return new ApiComplex
      {
        ComplexId = complex.ComplexId,
        ComplexName = complex.ComplexName,
        ContactNumber = complex.ContactNumber,
        ApiAddress = MakeApiAddress(complex.Address),
        ApiProvider = MakeApiProvider(complex.Provider),
        ApiTrainingCenter = MakeApiTrainingCenter(complex.Center),
        ApiRooms = !includeRooms ? new List<ApiRoom>()
                                 : complex.Rooms.Select(MakeApiRoom).ToList()
      };
    }

    /// <summary>
    /// Takes Library Gender model and makes an API Gender model
    /// </summary>
    /// <param name="gender"></param>
    /// <returns></returns>
    public static ApiGender MakeApiGender(Gender gender)
    {
      return new ApiGender
      {
        GenderId = gender.GenderId,
        GenderType = gender.GenderType
      };
    }

    /// <summary>
    /// Takes Library Notification model and makes an API Notification model
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Takes Library Provider model and makes an API Provider model
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static ApiProvider MakeApiProvider(Lib.Models.Provider provider)
    {
      if (provider is null) { return null; }
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

    /// <summary>
    /// Takes Library Room model and makes an API Room model
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public static ApiRoom MakeApiRoom(Room room)
    {
      if (room is null) { return null; }
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

    /// <summary>
    /// Takes Library RoomType model and makes an API RoomType model
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static ApiRoomType MakeApiRoomType(RoomType type)
    {
      return new ApiRoomType
      {
        TypeId = type.TypeId,
        RoomType = type.Type
      };
    }

    /// <summary>
    /// Takes Library TrainingCenter model and makes an API TrainingCenter model
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    public static ApiTrainingCenter MakeApiTrainingCenter(TrainingCenter center)
    {
      if (center is null) { return null; }
      return new ApiTrainingCenter
      {
        CenterId = center.CenterId,
        CenterName = center.CenterName,
        ContactNumber = center.ContactNumber,
        ApiAddress = MakeApiAddress(center.Address),
        ApiProvider = center.Providers?.Select(MakeApiProvider)?.ToList()
      };
    }
  }
}
