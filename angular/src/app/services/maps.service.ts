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
  private apiUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address=';
  private distUrl = 'https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=';
  private key = '&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';

  map: Maps;
  distance: number;
  addressExists: boolean = false;

  constructor(private httpClient: HttpClient) { }

  verifyAddress() {
    const query = this.apiUrl + TestServiceData.dummyAddress.streetAddress + this.key;
    this.httpClient.get<Maps>(query).toPromise().then(x => {
      this.map = x;
      console.log(this.map);
      if (this.map.status == 'OK'){
        this.addressExists = true;
        console.log(this.addressExists);
      }
      return this.addressExists;
    });
  }

  checkDistance(){
    //const query = this.distUrl + TestServiceData.dummyAddress.streetAddress + '&destinations=' + TestServiceData.UTA.streetAddress + this.key;
    //const query = this.distUrl + '3810maryvale' + '&destinations=' + '749SCooperSt' + this.key;
    const query = 'https://maps.googleapis.com/maps/api/distancematrix/json?origins=3810maryvale&destinations=749SCooperSt&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';
    //const query = 'https://maps.googleapis.com/maps/api/geocode/json?address=3810maryvale&key=AIzaSyCxYMcmEjlHQ2r2CywMgyK7YEplxurqW2A';
    console.log(query);
    this.httpClient.get<Mapdist>(query).toPromise();
  }
}
