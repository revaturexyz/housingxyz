import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Address } from '../../interfaces/address';
import { Maps } from '../../interfaces/maps';
import { Mapdist } from '../../interfaces/mapdist';
import { Observable, of, from } from 'rxjs';
import { TestServiceData } from './static-test-data';
import { promise } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class MapsService {
  private geocodeUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address=';
  private distUrl = 'https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=';
  private key = '&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';

  boolstatus: boolean;

  constructor(private httpClient: HttpClient) { }

  verifyAddress(address: Address) {
    const query = this.geocodeUrl + address.streetAddress + address.zipCode + this.key;
    return this.httpClient.get<Maps>(query).toPromise();
  }
}
