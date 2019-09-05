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
