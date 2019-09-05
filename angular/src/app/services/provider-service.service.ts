import { Injectable } from '@angular/core';
import { Provider } from 'src/models/Provider';
import { HttpClient } from '@angular/common/http';
import { Trainingcenter } from 'src/models/trainingcenter';
import { Observable } from 'rxjs';
import { Complex } from 'src/models/complex';
import { Address } from 'src/models/address';


@Injectable({
  providedIn: 'root'
})

export class ProviderServiceService {

  dummyTrainCenter: Trainingcenter = new Trainingcenter();
  dummyAddress: Address = new Address(1, '123 Address St', 'Arlington', 'TX', '12345');
  dummyComplex: Complex = new Complex(1, '123 Complex St', 'Arlington', 'TX', '12345', 'Liv+ Appartments', '123-123-1234');
  dummyProv: Provider = new Provider('Liv+', '123 Address St', 'Arlington', 'TX', '12345', '123-123-1234', this.dummyTrainCenter);

  constructor(private httpBus: HttpClient) { }


  getProviders(): Observable<Provider[]> {
    const simpleObservable = new Observable<Provider[]>((sub) => {
      // observable execution
      const provList: Provider[] = [];
      provList.push(this.dummyProv);
      sub.next(provList);
      sub.complete();
    });
    return simpleObservable;
  }

  getProviderById(id: number): Observable<Provider>{
    const simpleObservable = new Observable<Provider>((sub) => {
      // observable execution
      sub.next(this.dummyProv);
      sub.complete();
    });
    return simpleObservable;
  }

  getComplexes(id: number): Observable<Complex[]> {
    let simpleObservable = new Observable<Complex[]>((sub) => {
      // observable execution
      const complexList: Complex[] = [];
      complexList.push(this.dummyComplex);
      sub.next(complexList);
      sub.complete();
    });
    return simpleObservable;
  }

  getAddressesByProvider(provider: number): Observable<Address[]> {
    let simpleObservable = new Observable<Address[]>((sub) => {
      // observable execution
      const addrList: Address[] = [];
      addrList.push(this.dummyAddress);
      sub.next(addrList);
      sub.complete();
    });
    return simpleObservable;
  }
}
