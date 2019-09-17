import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Address } from '../../interfaces/address';
import { Maps } from '../../interfaces/maps/maps';
import { MapsGeoLocation } from '../../interfaces/maps/maps-geo-location';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MapsService {
  private geocodeUrl = 'https://maps.googleapis.com/maps/api/geocode/json?address=';

  constructor(private httpClient: HttpClient) { }

  async verifyAddress(address: Address): Promise<boolean> {
    const query = this.geocodeUrl +
      address.streetAddress + '+' +
      address.city + '+' +
      address.state + '+' +
      address.zipCode +
      '&key=' + environment.googleMapsKey;

    return await this.httpClient.get<Maps>(query).toPromise()
      .then((mapsResult) => {
        return mapsResult.status === 'OK';
      })
      .catch((error) => {
        console.log(error);
        return false;
      });
  }

  async getCoordinates(address: Address): Promise<MapsGeoLocation> {
    console.log(address);
    const query = this.geocodeUrl +
      address.streetAddress + '+' +
      address.city + '+' +
      address.state + '+' +
      address.zipCode +
      '&key=' + environment.googleMapsKey;

    return await this.httpClient.get<Maps>(query).toPromise()
      .then((mapsResult) => {
        return mapsResult.results[0].geometry.location;
      })
      .catch((error) => {
        console.log(error);
        return null;
      });
  }
  async checkDistance(address1: Address, address2: Address): Promise<number> {
    const origin = await this.getCoordinates(address1);
    const destination = await this.getCoordinates(address2);

    // Uses the Haversine formula to determine the distance
    // between two coordinates on the globe.
    //
    // Our threshold for distance is small enough that
    // accounting for curvature of the Earth is not be required.
    const R = 3963.19;
    const dLat = Math.PI / 180 * (destination.lat - origin.lat);
    const dLon = Math.PI / 180 * (destination.lng - origin.lng);
    const a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(Math.PI / 180 * (origin.lat)) * Math.cos(Math.PI / 180 * (destination.lat)) *
      Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    const d = R * c;
    return d;
  }
}
