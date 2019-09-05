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

  getProviders(): Observable<Provider[]>{
    //this.httpBus.getProviders()
    var simpleObservable = new Observable<Provider[]>((sub) => {
      // observable execution
      var provList : Provider[] = [];
      provList.push(this.dummyProv);
      sub.next(provList);
      sub.complete();
    });
  return simpleObservable;
  }

  getProviderById(id: number): Observable<Provider>{
    var simpleObservable = new Observable<Provider>((sub) => {
      // observable execution
      sub.next(this.dummyProv);
      sub.complete();
    });
    return simpleObservable;
  }

  getComplexes(id: number): Observable<Complex[]>{
    var simpleObservable = new Observable<Complex[]>((sub) => {
      // observable execution
      var complexList : Complex[] = [];
      complexList.push(this.dummyComplex);
      sub.next(complexList);
      sub.complete();
    });
    return simpleObservable;
  }

  getAddressesByProvider(provider: number): Observable<Address[]>{
    var simpleObservable = new Observable<Address[]>((sub) => {
      // observable execution
      var addrList : Address[] = [];
      addrList.push(this.dummyAddress);
      sub.next(addrList);
      sub.complete();
    });
    return simpleObservable;
  }
}
