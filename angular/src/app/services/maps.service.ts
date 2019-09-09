import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Address } from '../../models/address';
import { Maps } from '../../models/maps';
import { Observable, of, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MapsService {
  private apiUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address=';
  private key = '&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';
  address: Address = new Address(1, '1001 S Center St', 'Arlington', 'TX', '76010');
  map1: Maps;

  constructor(private httpClient: HttpClient) { }

  verifyAddress(){
    const query = this.apiUrl + this.address.StreetAddress + this.key;
    console.log(this.address.StreetAddress);
    console.log(this.apiUrl + this.address.StreetAddress + this.key);
    //this.httpClient.get<Maps>(query).toPromise().then(r => this.checkAddr(r));
    return this.httpClient.get<Maps>(query).toPromise();
  }
  checkAddr(r: Maps) {
    this.map1 = r;
    console.log(this.map1);
  }
}
