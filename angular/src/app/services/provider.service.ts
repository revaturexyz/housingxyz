import { Injectable } from '@angular/core';
import { Provider } from 'src/models/provider';
import { HttpClient } from '@angular/common/http';
import { TrainingCenter } from 'src/models/trainingcenter';
import { Observable } from 'rxjs';
import { Complex } from 'src/models/complex';
import { Address } from 'src/models/address';


@Injectable({
  providedIn: 'root'
})

export class ProviderServiceService {

  dummyTrainCenter: TrainingCenter = new TrainingCenter(
    1,
    "UIC",
    "123 s. Chicago Ave",
    "Chicago",
    "Illinois",
    "60645",
    "UIC",
    "3213213214"
  );
  dummyAddress: Address = new Address(1, '123 Address St', 'Arlington', 'TX', '12345');
  dummyComplex: Complex = new Complex(1, '123 Complex St', 'Arlington', 'TX', '12345', 'Liv+ Appartments', '123-123-1234');
  dummyComplex2: Complex = new Complex(2, '234 Complex St', 'Arlington', 'TX', '23456', 'Liv- Appartments', '123-123-1234');
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
    const simpleObservable = new Observable<Complex[]>((sub) => {
      // observable execution
      const complexList: Complex[] = [];
      complexList.push(this.dummyComplex);
      complexList.push(this.dummyComplex2);
      sub.next(complexList);
      sub.complete();
    });
    return simpleObservable;
  }

  getAddressesByProvider(provider: number): Observable<Address[]> {
    const simpleObservable = new Observable<Address[]>((sub) => {
      // observable execution
      const addrList: Address[] = [];
      addrList.push(this.dummyAddress);
      sub.next(addrList);
      sub.complete();
    });
    return simpleObservable;
  }
}
