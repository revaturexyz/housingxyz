import { Injectable } from '@angular/core';
import { Provider } from 'src/interfaces/provider';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Complex } from 'src/interfaces/complex';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ProviderService {

  apiUrl: string = environment.endpoints.providerXYZ;

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

  getAddressesByProvider(providerId: number): Observable<Address[]> {
    const addressUrl = this.apiUrl + 'Address/provider/' + providerId;

    return this.httpBus.get<Address[]>(addressUrl);
  }
}
