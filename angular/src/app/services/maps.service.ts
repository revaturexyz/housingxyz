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

  constructor(private httpClient: HttpClient) { }

  verifyAddress() {
    const query = this.geocodeUrl + TestServiceData.dummyAddress.streetAddress + this.key;
    this.httpClient.get<Maps>(query).toPromise().then(x => {
      console.log(x);
      if (x.status === 'OK' ) {
        console.log(x.status);
        return true;
      }
      return false;
    });
  }
}
