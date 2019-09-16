import { TestBed, getTestBed } from '@angular/core/testing';
import { ProviderService } from './provider.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';
import { Provider } from '../../interfaces/provider';
import { TrainingCenter } from '../../interfaces/trainingcenter';
import { Complex } from 'src/interfaces/complex';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from '../services/static-test-data';

const provider1: Provider = TestServiceData.dummyProvider;
<<<<<<< HEAD
=======
const listProvider: Provider[] = TestServiceData.testProviders;
>>>>>>> feat-add-complex

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
    it('should return an Observable<Provider[]>', () => {
<<<<<<< HEAD
    const  someProviders = [provider1];
    myProvider.getProviders().subscribe((provider) => {
    expect(provider.length).toBe(1);
    expect(provider[0].streetAddress).toEqual(someProviders[0].streetAddress);
    });
    });

=======
      const  someProviders = [provider1];
      myProvider.getProviders().subscribe((provider) => {
      expect(provider.length).toBe(1);
      expect(provider[0].address).toEqual(someProviders[0].address);
      });
    });
>>>>>>> feat-add-complex
  });

  describe('getProviderById', () => {
    it('should return an Observable<Provider>', () => {
<<<<<<< HEAD
    const  someProviders = provider1;
    myProvider.getProviderById(1).subscribe((provider) => {
    expect(provider.companyName).toEqual(someProviders.companyName);
    expect(provider.streetAddress).toEqual(someProviders.streetAddress);
    expect(provider.providerTrainingCenter.centerId).toEqual(someProviders.providerTrainingCenter.centerId);
    });
    });

  });

  describe('getComplexes', () => {
=======
      const  someProviders = listProvider;
      myProvider.getProviderById(1).subscribe((provider) => {
      expect(provider.companyName).toEqual(someProviders[1].companyName);
      expect(provider.address).toEqual(someProviders[1].address);
      expect(provider.providerTrainingCenter.centerId).toEqual(someProviders[1].providerTrainingCenter.centerId);
      });
    });
  });

  describe('getComplexesByProvider', () => {
>>>>>>> feat-add-complex
    const complex1: Complex = TestServiceData.dummyComplex;

    const complex2: Complex = TestServiceData.dummyComplex2;

    it('should return an Observable<Complex[]>', () => {
    const  someComplexes = [complex1, complex2];
<<<<<<< HEAD
    myProvider.getComplexes(1).subscribe((complex) => {
=======
    myProvider.getComplexesByProvider(1).subscribe((complex) => {
>>>>>>> feat-add-complex
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
