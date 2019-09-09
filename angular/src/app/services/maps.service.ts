import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Address } from '../../interfaces/address';
import { Maps } from '../../interfaces/maps';
import { Observable, of, from } from 'rxjs';
import { TestServiceData } from './static-test-data';

@Injectable({
  providedIn: 'root'
})
export class MapsService {
  private apiUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address=';
  private key = '&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';
  address: Address = {addressId: 1,
    streetAddress: '1001 S Center St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '76010'
    };
  map1: Maps;

  constructor(private httpClient: HttpClient) { }

  verifyAddress(){
    const query = this.apiUrl + this.address.streetAddress + this.key;
    console.log(this.address.streetAddress);
    console.log(this.apiUrl + this.address.streetAddress + this.key);
    return this.httpClient.get<Maps>(query).toPromise();
  }
}
