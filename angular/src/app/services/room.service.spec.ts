import { TestBed, getTestBed } from '@angular/core/testing';
import { RoomService } from './room.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Address } from '../../interfaces/address';
import { Amenity } from '../../interfaces/amenity';
import { Room } from '../../interfaces/room';
import { Complex } from 'src/interfaces/complex';
import { TestServiceData } from './static-test-data';

const complex1: Complex = TestServiceData.dummyComplex;
const complex2: Complex = TestServiceData.dummyComplex2;

const amenity1: Amenity = {
  amenityId: 1,
  amenity:  'Washer/Dryer',
  isSelected: true
};
const amenity2: Amenity = {
  amenityId: 2,
  amenity: 'Smart TV',
  isSelected: true
};

const amenityList: Amenity[] = [
  amenity1,
  amenity2,
  { amenityId: 3, amenity: 'Patio', isSelected: true},
  { amenityId: 4, amenity: 'Fully Furnished', isSelected: true},
  { amenityId: 5, amenity: 'Full Kitchen', isSelected: true},
  { amenityId: 6, amenity: 'Individual Bathrooms', isSelected: true}
];

const room1: Room = {
  roomId: 0, apiAddress: {
    addressId: 1, streetAddress: '123 Address St', city:
      'Arlington', state: 'TX', zipCode: '12345'
  }, roomNumber: '', numberOfBeds: 2, roomType: '',
  isOccupied: false, amenities: amenityList, startDate:
    new Date(), endDate: new Date(), apiComplex: complex1
};
const room2: Room = {
  roomId: 0, apiAddress: {
    addressId: 2, streetAddress: '701 S Nedderman Dr',
    city: 'Arlington', state: 'TX', zipCode: '76019'
  }, roomNumber: '323', numberOfBeds: 9001,
  roomType: 'Dorm', isOccupied: true, amenities: [{ amenityId: 2, amenity: 'Washer/Dryer', isSelected: true}],
  startDate: new Date(), endDate: new Date(), apiComplex: complex2
};

describe('RoomService', () => {
  // let injector: TestBed;
  let myProvider: RoomService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RoomService]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(RoomService);
    httpMock = testBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: RoomService = TestBed.get(RoomService);
    expect(service).toBeTruthy();
  });

  describe('getRoomsByProvider', () => {
    // getRoomsByProvider test
    it('should return an Observable<Room[]>', () => {
      const someRooms = [
        room1,
        room2];
      myProvider.getRoomsByProvider(1).subscribe((room) => {
        expect(room.length).toBe(2);
        expect(room[0].apiAddress).toEqual(someRooms[0].apiAddress);
        expect(room[1].apiAddress).toEqual(someRooms[1].apiAddress);
      });
      /*// add in the baseurl later
      const  call = httpMock.expectOne(`${myProvider}/rooms`);
      expect(call.request.method).toBe("GET");
      call.flush(someRooms);
      httpMock.verify();*/
    });
  });
  describe('getRoomById', () => {
    // getRoomById test
    it('should return an Observable<Room>', () => {
      const oneRoom = room1;
      myProvider.getRoomById(0).subscribe((room) => {
        expect(room.apiAddress).toEqual(oneRoom.apiAddress);
      });
    });
  });
  describe('postRoom', () => {
    // postRoom
    it('should return an Observable<Room>', () => {
      const oneRoom = room1;
      myProvider.postRoom(oneRoom).subscribe((room) => {
        expect(room).toEqual(oneRoom);
      });
    });
  });
  describe('getRoomTypes', () => {
    it('should return an Observable<string[]>', () => {
      const roomGen = ['Apartment', 'Dorm'];
      myProvider.getRoomTypes().subscribe((types) => {
        expect(types[0]).toEqual(roomGen[0]);
        expect(types[1]).toEqual(roomGen[1]);
      });
    });
  });
  describe('getGenders', () => {
    it('should return an Observable<string[]>', () => {
      const roomGen = ['male', 'female', 'undefined'];
      myProvider.getGenders().subscribe((types) => {
        expect(types[0]).toEqual(roomGen[0]);
        expect(types[1]).toEqual(roomGen[1]);
        expect(types[2]).toEqual(roomGen[2]);
      });
    });
  });
  describe('getAmenities', () => {
    // getAmenities
    it('should return an Observable<Amenity[]>', () => {
      const amenities = [amenity1, amenity2];
      myProvider.getAmenities().subscribe((types) => {
        expect(amenities.length).toBe(2);
        expect(amenities[0]).toEqual(types[0]);
        expect(amenities[1]).toEqual(types[1]);
      });
    });
  });
});
