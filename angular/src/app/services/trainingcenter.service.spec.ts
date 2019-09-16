import { TestBed, getTestBed } from '@angular/core/testing';
import { TrainingCenterService } from './trainingcenter.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Complex } from '../../interfaces/complex';
import { TestServiceData } from './static-test-data';

describe('TrainingCenterService', () => {
  let myProvider: TrainingCenterService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [TrainingCenterService]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(TrainingCenterService);
    httpMock = testBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: TrainingCenterService = TestBed.get(TrainingCenterService);
    expect(service).toBeTruthy();
  });

  // getTrainingCenters test
  describe('getTrainingCenters', () => {
    const center1: TrainingCenter = TestServiceData.trainingcenter;
    const center2: TrainingCenter = TestServiceData.trainingcenter2;

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
    const complex1: Complex = TestServiceData.dummyComplex;
    const complex2: Complex = TestServiceData.dummyComplex2;
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
