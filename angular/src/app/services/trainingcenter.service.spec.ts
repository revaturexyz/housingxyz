import { TestBed, getTestBed } from '@angular/core/testing';
import { TrainingcenterService } from './trainingcenter.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Complex } from '../../interfaces/complex';

describe('TrainingcenterService', () => {
  let myProvider: TrainingcenterService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TrainingcenterService]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(TrainingcenterService);
    httpMock = testBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: TrainingcenterService = TestBed.get(TrainingcenterService);
    expect(service).toBeTruthy();
  });

  // getTrainingCenters test
  describe('getTrainingCenters', () => {
    const center1: TrainingCenter = {
      centerId: 1,
      streetAddress: '1001 S. Center St',
      city: 'Arlington',
      state: 'Texas',
      zipCode: '70610',
      centerName: 'UT Arlington',
      contactNumber: '1231231234'
    };
    const center2: TrainingCenter = {
      centerId: 2,
      streetAddress: '123 s. Chicago Ave',
      city: 'Chicago',
      state: 'Illinois',
      zipCode: '60645',
      centerName: 'UIC',
      contactNumber: '3213213214'
    };
    it('should return an Observable<TrainingCenter[]>', () => {
      const someCenters = [center1, center2];
      myProvider.getTrainingCenters().subscribe((center) => {
        expect(center.length).toBe(2);
        expect(center).toEqual(someCenters);
      });
      /*// add in the baseurl later
      const  call = httpMock.expectOne(`${myProvider}/rooms`);
      expect(call.request.method).toBe("GET");
      call.flush(someRooms);
      httpMock.verify();*/
    });
  });
  describe('getComplexesByTrainingCenter', () => {
    const complex1: Complex = {
      complexId: 1,
      streetAddress: '123 Complex St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345',
      complexName: 'Liv+ Appartments',
      contactNumber: '123-123-1234'
    };
    const complex2: Complex = {
      complexId: 2,
      streetAddress: '234 Complex St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '23456',
      complexName: 'Liv- Appartments',
      contactNumber: '123-123-1234'
    };
    it('should return an Observable<Complex[]>', () => {
      const someComplexs = [complex1, complex2];
      myProvider.getComplexesByTrainingCenter(1).subscribe((complex) => {
        expect(complex.length).toBe(2);
        expect(complex).toEqual(someComplexs);
      });
      /*// add in the baseurl later
      const  call = httpMock.expectOne(`${myProvider}/rooms`);
      expect(call.request.method).toBe("GET");
      call.flush(someRooms);
      httpMock.verify();*/
    });
  });
});
