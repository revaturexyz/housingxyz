using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using Xyz.Provider.Lib.Interface;
using Xyz.Provider.Lib.Models;

namespace Xyz.Provider.Tests.ApiTests
{
  /// <summary>
  /// This is test data for the Api, and is used
  /// by our Moq repo tests
  /// </summary>
  internal static class ApiTestData
  {
    /// <summary>
    /// The following are methods that are used as dummy data.
    /// </summary>
    internal static List<Address> Addresses = new List<Address>
    {
      new Address
      {
        AddressId = 1,
        StreetAddress = "1001 Center St.",
        City = "Arlington",
        State = "TX",
        Zip = "76010"
      },
      new Address
      {
        AddressId = 2,
        StreetAddress = "2124 Parker St.",
        City = "Berkeley",
        State = "CA",
        Zip = "94704"
      },
      new Address
      {
        AddressId = 3,
        StreetAddress = "2399 Prospect St.",
        City = "Berkeley",
        State = "CA",
        Zip = "94704"
      },
      new Address
      {
        AddressId = 4,
        StreetAddress = "2132 Oxford St.",
        City = "Berkeley",
        State = "CA",
        Zip = "94704"
      },
    };

    internal static List<TrainingCenter> Centers = new List<TrainingCenter>
    {
      new TrainingCenter
      {
        CenterId = 1,
        Address = Addresses[0],
        Providers = new List<Lib.Models.Provider>(),
      },
      new TrainingCenter
      {
        CenterId = 2,
        Address = Addresses[3],
        Providers = new List<Lib.Models.Provider>()
      }
    };

    internal static List<Lib.Models.Provider> Providers = new List<Lib.Models.Provider>
    {
      new Lib.Models.Provider
      {
        ProviderId = 1,
        Address = Addresses[0],
        Center = Centers[0]
      },
      new Lib.Models.Provider
      {
        ProviderId = 2,
        Address = Addresses[2],
        Center = Centers[1]
      }
    };

    internal static List<Complex> Complices = new List<Complex>
    {
      new Complex
      {
        ComplexId = 1,
        Address = Addresses[0],
        ComplexName = "Arlington Complex",
        ContactNumber = "1234567890",
        Center = Centers[0],
        Provider = Providers[0],
        Rooms = new List<Room>()
      },
      new Complex
      {
        ComplexId = 2,
        Address = Addresses[1],
        ComplexName = "Berkeley Complex",
        ContactNumber = "4204206969",
        Center = Centers[1],
        Provider = Providers[1],
        Rooms = new List<Room>()
      }
    };

    internal static Gender Male = new Gender
    {
      GenderId = 1,
      GenderType = "Male",
    };

    internal static Gender Female = new Gender
    {
      GenderId = 2,
      GenderType = "Female",
    };

    internal static RoomType Apartment = new RoomType
    {
      TypeId = 1,
      Type = "Apartment"
    };

    internal static RoomType Dorm = new RoomType
    {
      TypeId = 2,
      Type = "Dorm"
    };

    internal static List<Amenity> Amenities = new List<Amenity>
    {
      new Amenity
      {
        AmenityId = 1,
        AmenityType = "Washer-Dryer"
      },
      new Amenity
      {
        AmenityId = 2,
        AmenityType = "Smart TV"
      },
      new Amenity
      {
        AmenityId = 3,
        AmenityType = "Air Conditioning"
      },
      new Amenity
      {
        AmenityId = 4,
        AmenityType = "Jacuzzi"
      }
    };

    internal static List<Room> Rooms = new List<Room>
    {
      new Room
      {
        RoomId = 1,
        Complex = Complices[0],
        Address = Complices[0].Address,
        Gender = Male,
        RoomNumber = "5",
        RoomType = Apartment,
        NumberOfBeds = 8,
        NumberOfOccupants = 0,
        HasAmenity = true,
        StartDate = DateTime.Now,
        EndDate = null,
        Amenities = Amenities.Take(2).ToList()
      },
      new Room
      {
        RoomId = 2,
        Complex = Complices[0],
        Address = Complices[0].Address,
        Gender = Female,
        RoomNumber = "55",
        RoomType = Apartment,
        NumberOfBeds = 8,
        NumberOfOccupants = 7,
        HasAmenity = true,
        StartDate = DateTime.Now,
        EndDate = null,
        Amenities = Amenities.Take(2).ToList()
      },
      new Room
      {
        RoomId = 3,
        Complex = Complices[1],
        Address = Complices[1].Address,
        Gender = Male,
        RoomNumber = "555",
        RoomType = Apartment,
        NumberOfBeds = 8,
        NumberOfOccupants = 7,
        HasAmenity = true,
        StartDate = DateTime.Now,
        EndDate = null,
        Amenities = Amenities.ToList()
      },
      new Room
      {
        RoomId = 4,
        Complex = Complices[1],
        Address = Complices[1].Address,
        Gender = Female,
        RoomNumber = "5555",
        RoomType = Apartment,
        NumberOfBeds = 8,
        NumberOfOccupants = 7,
        HasAmenity = true,
        StartDate = DateTime.Now,
        EndDate = null,
        Amenities = Amenities.ToList()
      },
    };

    internal static Mock<IAddressRepository> MockAddressRepo(List<Address> testAddresses)
    {
      var mockRepo = new Mock<IAddressRepository>();
      mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>()))
              .ReturnsAsync((int i) =>
              {
                var address = testAddresses.FirstOrDefault(a => a.AddressId == i);
                if (address == null)
                {
                  throw new ArgumentException();
                }
                return address;
              });
      return mockRepo;
    }

    internal static Mock<IComplexRepository> MockComplexRepo(List<Complex> testComplices)
    {
      var mockRepo = new Mock<IComplexRepository>();
      mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>()))
              .ReturnsAsync((int i) => testComplices.FirstOrDefault(c => c.ComplexId == i));
      mockRepo.Setup(repo => repo.GetComplexesByProviderIdAsync(It.IsAny<int>()))
              .ReturnsAsync((int i) => testComplices.Where(c => c.Provider.ProviderId == i));
      mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Complex>(), It.IsAny<int>()))
              .ReturnsAsync((Complex comp, int i) =>
              {
                comp.ComplexId = testComplices.Max(c => c.ComplexId) + 1;
                testComplices.Add(comp);
                return comp;
              });
      return mockRepo;
    }

    internal static Mock<IProviderRepository> MockProviderRepo(List<Lib.Models.Provider> testProviders)
    {
      var mockRepo = new Mock<IProviderRepository>();
      mockRepo.Setup(repo => repo.GetAllAsync())
              .ReturnsAsync(testProviders.AsEnumerable());
      mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>()))
              .ReturnsAsync((int i) =>
              {
                if (i <= 0 || i > testProviders.Count())
                {
                  throw new ArgumentException();
                }
                return testProviders.FirstOrDefault(p => p.ProviderId == i);
              });
      mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Lib.Models.Provider>(), It.IsAny<int>()))
              .Callback((Lib.Models.Provider prov, int i) =>
              {
                var oldProvider = testProviders.FirstOrDefault(p => p.ProviderId == i);
                oldProvider.Username = prov.Username;
                oldProvider.Password = prov.Password;
                oldProvider.CompanyName = prov.CompanyName;
                oldProvider.ContactNumber = prov.ContactNumber;
                oldProvider.Center = prov.Center;
              });
      return mockRepo;
    }

    internal static Mock<IRoomRepository> MockRoomRepo(List<Room> testRooms)
    {
      var mockRepo = new Mock<IRoomRepository>();
      mockRepo.Setup(repo => repo.GetAsync(It.IsAny<int>()))
        .ReturnsAsync((int i) =>
        {
          var room = testRooms.FirstOrDefault(r => r.RoomId == i);
          if (room == null) { throw new ArgumentException(); }
          return room;
        });
      mockRepo.Setup(repo => repo.GetAllRoomsByComplexIdAsync(It.IsAny<int>()))
        .ReturnsAsync((int i) => testRooms.Where(r => r.Complex.ComplexId == i));
      mockRepo.Setup(repo => repo.GetAllRoomsByProviderIdAsync(It.IsAny<int>()))
        .ReturnsAsync((int i) => testRooms.Where(r => r.Complex.Provider.ProviderId == i));
      mockRepo.Setup(repo => repo.GetAllRoomsByProviderIdAsync(It.IsAny<int>()))
              .ReturnsAsync((int i) => testRooms.Where(r => r.Complex.Provider.ProviderId == i));
      mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Room>(), It.IsAny<int>()))
              .ReturnsAsync((Room room, int i) =>
              {
                room.RoomId = testRooms.Max(r => r.RoomId) + 1;
                testRooms.Add(room);
                return room;
              });
      mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Room>(), It.IsAny<int>()))
              .Callback((Room editedRoom, int i) =>
              {
                if (!testRooms.Any(r => r.Complex.Provider.ProviderId == i))
                {
                  throw new ArgumentException();
                }
                if (editedRoom.Complex.Provider.ProviderId != i)
                {
                  throw new InvalidOperationException();
                }
                var oldRoom = testRooms.FirstOrDefault(r => r.RoomId == editedRoom.RoomId);
                oldRoom.EndDate = editedRoom.EndDate;
              });
      return mockRepo;
    }
  }
}
