import { TestBed, getTestBed } from '@angular/core/testing';
import { ProviderService } from './provider.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';
import { Provider } from '../../interfaces/provider';
import { TrainingCenter } from '../../interfaces/trainingcenter';
import { Complex } from 'src/interfaces/complex';
import { Address } from 'src/interfaces/address';

describe('ProviderService', () => {
  let  myProvider: ProviderService;
  let  httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProviderService]
    });

    const testBed = getTestBed();
    myProvider = testBed.get(ProviderService);
    httpMock = testBed.get(HttpTestingController);
  });

  it('should be created', () => {
    const service: ProviderService = TestBed.get(ProviderService);
    expect(service).toBeTruthy();
  });

  describe('getProviders', () => {
    const provider1: Provider = {
      providerId: 1,
      companyName: 'Liv+',
      streetAddress: '123 Address St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345',
      contactNumber: '123-123-1234',
      providerTrainingCenter: {
        centerId: 1,
        streetAddress: '1001 S. Center St',
        city: 'Arlington',
        state: 'Texas',
        zipCode: '70610',
        centerName: 'UT Arlington',
        contactNumber: '1231231234'
    }
  };


    it('should return an Observable<Provider[]>', () => {
    const  someProviders = [provider1];
    myProvider.getProviders().subscribe((provider) => {
    expect(provider.length).toBe(1);
    expect(provider[0].streetAddress).toEqual(someProviders[0].streetAddress);
    });
    });

  });

  describe('getProviderById', () => {
    const provider1: Provider = {
      providerId: 1,
      companyName: 'Liv+',
      streetAddress: '123 Address St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345',
      contactNumber: '123-123-1234',
      providerTrainingCenter: {
        centerId: 1,
        streetAddress: '1001 S. Center St',
        city: 'Arlington',
        state: 'Texas',
        zipCode: '70610',
        centerName: 'UT Arlington',
        contactNumber: '1231231234'
    }
  };


    it('should return an Observable<Provider>', () => {
    const  someProviders = provider1;
    myProvider.getProviderById(1).subscribe((provider) => {
    expect(provider.companyName).toEqual(someProviders.companyName);
    expect(provider.streetAddress).toEqual(someProviders.streetAddress);
    expect(provider.providerTrainingCenter.centerId).toEqual(someProviders.providerTrainingCenter.centerId);
    });
    });

  });

  describe('getComplexes', () => {
    const dummyComplex: Complex = {
      complexId: 1,
      streetAddress: '123 Complex St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345',
      complexName: 'Liv+ Appartments',
      contactNumber: '123-123-1234'
  };

    const dummyComplex2: Complex = {
      complexId: 2,
      streetAddress: '234 Complex St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '23456',
      complexName: 'Liv- Appartments',
      contactNumber: '123-123-1234'
  };


    it('should return an Observable<Complex[]>', () => {
    const  someComplexes = [dummyComplex, dummyComplex2];
    myProvider.getComplexes(1).subscribe((complex) => {
    expect(complex.length).toBe(2);
    expect(complex[0].complexName).toEqual(someComplexes[0].complexName);
    expect(complex[1].complexName).toEqual(someComplexes[1].complexName);
    });
    });

  });

  describe('getAddressesByProvider', () => {
    const address1: Address = {
      addressId: 1,
      streetAddress: '123 Address St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345'
  };

    const address2: Address = {
      addressId: 2,
      streetAddress: '1001 S Center St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '76010'
  };


    it('should return an Observable<Address[]>', () => {
      const  someAddressess = [address1, address2];
      myProvider.getAddressesByProvider(1).subscribe((address) => {
      expect(address.length).toBe(2);
      expect(address[0].streetAddress).toEqual(someAddressess[0].streetAddress);
      expect(address[1].streetAddress).toEqual(someAddressess[1].streetAddress);
      });
      });

  });


});
