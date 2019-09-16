import { TestBed, getTestBed } from '@angular/core/testing';

import { MapsService } from './maps.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestServiceData } from './static-test-data';
import { Address } from '../../interfaces/address';
import { HasEventTargetAddRemove } from 'rxjs/internal/observable/fromEvent';

const newAdd: Address = TestServiceData.dummyAddress;
const livAdd: Address = TestServiceData.livPlusAddress;
const utaAdd: Address = TestServiceData.UTA;

describe('MapsService', () => {
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

  it('should verify address', () => {
    const mockAdd: Address = newAdd;
    myProvider.verifyAddress(mockAdd).then(result => {
      expect(result).toBeFalsy();
      const call = httpMock.expectOne(`https://maps.googleapis.com/maps/api/geocode/json`
        + `?address=${mockAdd.streetAddress + mockAdd.zipCode}&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A`);
      expect(call.request.method).toBe('GET');
      call.flush(mockAdd);
      httpMock.verify();
    });
  });

  it('should get coordinates of address', () => {
    const mockAdd: Address = newAdd;
    myProvider.getCoordinates(mockAdd).then(result => {
      expect(result).toEqual({lat: 12, lng: 12});
      const call = httpMock.expectOne(`https://maps.googleapis.com/maps/api/geocode/json`
        + `?address=${mockAdd.streetAddress + mockAdd.zipCode}&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A`);
      expect(call.request.method).toBe('GET');
      call.flush(mockAdd);
      httpMock.verify();
    });
  });

  it('should get distance of 2 address', () => {
    const mockAdds: Address[] = [livAdd, utaAdd];
    spyOn(myProvider, 'getCoordinates').and.returnValue(Promise.resolve({lat: 12, lng: 12}));
    myProvider.checkDistance(mockAdds[0], mockAdds[1]).then(result => {
      expect(result).toEqual(12);
      const call = httpMock.expectOne(`https://maps.googleapis.com/maps/api/geocode/json?`
        + `address=${mockAdds[0].streetAddress + mockAdds[0].zipCode}&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A`);
      const calls = httpMock.expectOne(`https://maps.googleapis.com/maps/api/geocode/json?`
        + `address=${mockAdds[1].streetAddress + mockAdds[1].zipCode}&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A`);
      expect(call.request.method).toBe('GET');
      expect(calls.request.method).toBe('GET');
      call.flush(mockAdds[0]);
      call.flush(mockAdds[1]);
      httpMock.verify();
    });
  });
});
