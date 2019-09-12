<<<<<<< HEAD
// import { TestBed } from '@angular/core/testing';

// import { RoomService } from './room.service';

// describe('RoomService', () => {
//   beforeEach(() => TestBed.configureTestingModule({}));

//   it('should be created', () => {
//     const service: RoomService = TestBed.get(RoomService);
//     expect(service).toBeTruthy();
//   });
// });
=======
import { TestBed } from '@angular/core/testing';
import { RoomService } from './room.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';

describe('RoomService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }).compileComponents());

  it('should be created', () => {
    const service: RoomService = TestBed.get(RoomService);
    expect(service).toBeTruthy();
  });
});
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
