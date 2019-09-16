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

  endPoint: string;
  constructor(private httpBus: HttpClient) {
    this.endPoint = environment.endpoints['localhost'];
  }

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
    const simpleObservable = new Observable<Provider>((sub) => {
      // observable execution
      sub.next(TestServiceData.dummyProvider);
      sub.complete();
    });
    return simpleObservable;
  }

  getComplexesByProvider(id: number): Observable<Complex[]> {
    return this.httpBus.get<Complex[]>(this.endPoint + `Complex/provider/${id}`);
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
