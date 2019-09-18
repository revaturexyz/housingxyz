import { TestBed, getTestBed } from '@angular/core/testing';

import { ComplexService } from './complex.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { Amenity } from 'src/interfaces/amenity';
import { TestServiceData } from './static-test-data';

const amenity1: Amenity = TestServiceData.dummyAmenity1;
const amenity2: Amenity = TestServiceData.dummyAmenity2;

describe('ComplexService', () => {
  let myProvider: ComplexService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ComplexService]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(ComplexService);
    httpMock = testBed.get(HttpTestingController);
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
        amenity2];
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
});
