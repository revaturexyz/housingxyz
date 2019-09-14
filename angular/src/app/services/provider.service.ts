import { Injectable } from '@angular/core';
import { Provider } from 'src/interfaces/provider';
import { HttpClient } from '@angular/common/http';
import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Observable } from 'rxjs';
import { Complex } from 'src/interfaces/complex';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';

@Injectable({
  providedIn: 'root'
})

export class ProviderService {

  constructor(private httpBus: HttpClient) { }

  getProviders(): Observable<Provider[]> {
    const simpleObservable = new Observable<Provider[]>((sub) => {
      // observable execution
      const provList: Provider[] = [];
      provList.push(TestServiceData.dummyProvider);
      sub.next(provList);
      sub.complete();
    });
    return simpleObservable;
  }

  getProviderById(id: number): Observable<Provider> {
    // Retrieve and add a value to our observable from the test providers
    // array matching the id if it exists.
    const simpleObservable = new Observable<Provider>((sub) => {
      // observable execution
      sub.next(TestServiceData.testProviders
        .find(x => x.providerId == id));
      sub.complete();
    });
    return simpleObservable;
  }

  getComplexes(id: number): Observable<Complex[]> {
    const simpleObservable = new Observable<Complex[]>((sub) => {
      // observable execution
      const complexList: Complex[] = [];
      complexList.push(TestServiceData.dummyComplex);
      complexList.push(TestServiceData.dummyComplex2);
      sub.next(complexList);
      sub.complete();
    });
    return simpleObservable;
  }

  getAddressesByProvider(provider: number): Observable<Address[]> {
    const simpleObservable = new Observable<Address[]>((sub) => {
      // observable execution
      const addrList: Address[] = [];
      addrList.push(TestServiceData.dummyAddress);
      addrList.push(TestServiceData.livPlusAddress);
      sub.next(addrList);
      sub.complete();
    });
    return simpleObservable;
  }
}
