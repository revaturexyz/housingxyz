import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiService } from './api.service';
import { resolveSanitizationFn } from '@angular/compiler/src/render3/view/template';
import { LocationData, MockProvider, MockRoom } from './testing/dummyData';
import { ProviderLocation } from 'src/Entities/location';

describe('ApiService', () => {
  let injector: TestBed;
  let service: ApiService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiService]
    });
    injector = getTestBed();
    service = injector.get(ApiService);
    httpMock = injector.get(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

//   it('should be created', () => {
//     const service: ApiService = TestBed.get(ApiService);
//     expect(service).toBeTruthy();
//   });

//   it('getLocationData() should get locations', () => {
//     const locations = [
//       { location: 'location_A' },
//       { location: "location_B" }
//     ];

//     service.getLocationData().subscribe((res) => {
      
//       expect(res).toEqual(locations);
//     });

//     const req = httpMock.expectOne('http://localhost:55219/api/locations/');
//     // expect(req.cancelled).toBeFalsy;
//     expect(req.request.method).toBe("GET");
//     req.flush(locations);

//   });

//  it('PostLocationData() should post the correct data', () =>{
//   const location = new MockProvider();
  
//     service.PostLocationData(location).subscribe((res) => {
//       expect(res).toEqual(location);
//     });
//     const req = httpMock.expectOne('http://localhost:55219/api/locations/');
//     expect(req.request.method).toBe("POST");
//     req.flush(location);

//   });
//   it('getRoomById() should get room by Id', () => {
//     const room = new MockRoom();
      
//     service.getRoomById(room.RoomID).subscribe((res) => {
//             expect(res).toEqual(room);
//     });

//     const req = httpMock.expectOne('http://localhost:55219/api/rooms/'+ room.RoomID);
//     // expect(req.cancelled).toBeFalsy;
//     expect(req.request.method).toBe("GET");
//     req.flush(room);

//   });
//   it('getRoomData() should get rooms', () => {
//     const rooms = [
//       { room: 'room_A' },
//       { room: "room_B" }
//     ];

//     service.getRoomData().subscribe((res) => {
      
//       expect(res).toEqual(rooms);
//     });

//     const req = httpMock.expectOne('http://localhost:55219/api/rooms/');
//     // expect(req.cancelled).toBeFalsy;
//     expect(req.request.method).toBe("GET");
//     req.flush(rooms);

//   });
//   it('postRoomData() should post the correct data', () =>{
//     const room = new MockRoom();
    
//       service.postRoomData(room).subscribe((res) => {
//         expect(res).toEqual(room);
//       });
//       const req = httpMock.expectOne('http://localhost:55219/api/rooms/');
//       expect(req.request.method).toBe("POST");
//       req.flush(room);
  
//     });
//     it('updateRoomData() should change the data of the room object ', () =>{
//       const room = new MockRoom();
      
//         service.updateRoomData(room).subscribe((res) => {
//           //expect(res).toEqual(room);
//         });
//         const req = httpMock.expectOne('http://localhost:55219/api/rooms/' + room.RoomID);
//         expect(req.request.method).toBe("PUT");
//         req.flush(room);
    
//       });
 });

