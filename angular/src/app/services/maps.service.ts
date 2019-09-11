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
  boolStatus: boolean;
  constructor(private httpClient: HttpClient) { }

  verifyAddress(): boolean {
    
    const query = this.geocodeUrl + TestServiceData.dummyAddress.streetAddress + TestServiceData.dummyAddress.zipCode + this.key;
    this.httpClient.get<Maps>(query).toPromise().then(x => {
      console.log(x.status);
      this.boolStatus = true;
      console.log(this.boolStatus);
      console.log('1');
      /*if (x.status === 'OK' ) {
        console.log(x.status);
        this.boolStatus = true;
        console.log(this.boolStatus);
        console.log('1');
        return this.boolStatus;
      }
      this.boolStatus = false;
      console.log(this.boolStatus);
      console.log('2');
      return this.boolStatus;*/
    }).catch(c => {
      console.log(c.status);
      this.boolStatus = false;
      console.log(this.boolStatus);
      console.log('2');
    });
    return true;
  }
}
