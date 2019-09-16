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
    return this.httpBus.get<Provider[]>(this.apiUrl + 'Provider/');
  }

  getProviderById(id: number): Observable<Provider> {
    return this.httpBus.get<Provider>(this.apiUrl + 'Provider/' + id);
  }

  getComplexes(id: number): Observable<Complex[]> {
    return this.httpBus.get<Complex[]>(this.apiUrl + 'Complex/provider/' + id);
  }

  getAddressesByProvider(provider: number): Observable<Address[]> {
    return this.httpBus.get<Address[]>(this.apiUrl + 'Address/provider/' + provider);
  }
}
