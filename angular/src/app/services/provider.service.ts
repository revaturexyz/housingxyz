import { Injectable } from '@angular/core';
import { Provider } from 'src/interfaces/provider';
import { HttpClient } from '@angular/common/http';
import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Observable } from 'rxjs';
import { Complex } from 'src/interfaces/complex';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ProviderService {

  apiUrl: string = environment.endpoints.localhost;

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
        .find(x => x.providerId === id));
      sub.complete();
    });
    return simpleObservable;
  }

  getComplexesByProvider(providerId: number): Observable<Complex[]> {
    return this.httpBus.get<Complex[]>(this.apiUrl + 'Complex/provider/' + providerId);
  }

  postComplex(complex: Complex, providerId: number) {
    const postComplexUrl = this.apiUrl + 'Complex/provider/' + providerId;

    return this.httpBus.post(postComplexUrl, JSON.parse(JSON.stringify(complex)));
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
