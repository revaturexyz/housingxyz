import { TestBed, getTestBed, async } from '@angular/core/testing';

import { MapsService } from './maps.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Address } from '../../interfaces/address';
import { HasEventTargetAddRemove } from 'rxjs/internal/observable/fromEvent';
import { environment } from 'src/environments/environment';

/*
const newAdd: Address = TestServiceData.dummyAddress;
const livAdd: Address = TestServiceData.livPlusAddress;
const utaAdd: Address = TestServiceData.UTA;

describe('MapsService', async () => {
  let myProvider: MapsService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MapsService]
    });
    const testBed = getTestBed();
    myProvider = testBed.get(MapsService);
    httpMock = testBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: MapsService = TestBed.get(MapsService);
    expect(service).toBeTruthy();
  });

  it('should verify address', (done) => {
    const mockAdd: Address = livAdd;
    const query = 'https://maps.googleapis.com/maps/api/geocode/json?address=' +
      mockAdd.streetAddress + '+' +
      mockAdd.city + '+' +
      mockAdd.state + '+' +
      mockAdd.zipcode +
      '&key=' + environment.googleMapsKey;
    myProvider.verifyAddress(mockAdd).then(result => {
      expect(result).toBeFalsy();
    });
    const call = httpMock.expectOne(query);
    expect(call.request.method).toBe('GET');
    call.flush(mockAdd);
    httpMock.verify();
    done();
  });

  it('should get coordinates of address', (done) => {
    const mockAdd: Address = newAdd;
    myProvider.getCoordinates(mockAdd).then(result => {
      expect(result).toEqual(null);
    });
    const call = httpMock.expectOne(`https://maps.googleapis.com/maps/api/geocode/json`
      + `?address=${mockAdd.streetAddress}+${mockAdd.city}+${mockAdd.state}+${mockAdd.zipcode}`
      + `&key=` + environment.googleMapsKey);
    expect(call.request.method).toBe('GET');
    call.flush(mockAdd);
    httpMock.verify();
    done();
  });

  it('should get distance of 2 address', (done) => {
    const mockAdds: Address[] = [livAdd, utaAdd];
    spyOn(myProvider, 'getCoordinates').and.returnValue(Promise.resolve({ lat: 12, lng: 12 }));
    myProvider.checkDistance(mockAdds[0], mockAdds[1]).then(result => {
      expect(result).toEqual(0);
    });
    done();
  });
}); */
