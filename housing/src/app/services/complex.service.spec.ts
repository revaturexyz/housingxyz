import { TestBed, getTestBed } from '@angular/core/testing';

import { ComplexService } from './complex.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { Amenity } from 'src/interfaces/amenity';
import { TestServiceData } from './static-test-data';
import { RouterTestingModule } from '@angular/router/testing';

/* 
const amenity1: Amenity = TestServiceData.dummyAmenity1;
const amenity2: Amenity = TestServiceData.dummyAmenity2;

class MockMsalService {
  getUser(): User { return new User('1', 'chris', 'master', 'test', new Object()); }
}

describe('ComplexService', () => {
  let myProvider: ComplexService;
  let httpMock: HttpTestingController;
  let msalService: MsalService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, MsalModule, RouterTestingModule],
      providers: [
        ComplexService,
        { provide: MsalGuard, useValue: {} },
        { provide: MsalService, useClass: MockMsalService },
        { provide: MSAL_CONFIG, useValue: {} }
      ]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(ComplexService);
    httpMock = testBed.get(HttpTestingController);
    msalService = testBed.get(MsalService);
  });

  it('should be created', () => {
    const service: ComplexService = TestBed.get(ComplexService);
    expect(service).toBeTruthy();
  });

  describe('getAmenityByComplex', () => {
    // getRoomsByProvider test
    it('should return an Observable<Amenity[]>', () => {
      const someAmenity = [
        amenity1,
        amenity2
      ];
      myProvider.getAmenityByComplex(1).subscribe((amenity) => {
        expect(amenity.length).toBe(someAmenity.length);
        expect(amenity).toEqual(someAmenity);
      });
      const call = httpMock.expectOne(`${myProvider.apiUrl}Complex/${1}/amenity`);
      expect(call.request.method).toBe('GET');
      call.flush(someAmenity);
      httpMock.verify();
    });
  });
}); */
