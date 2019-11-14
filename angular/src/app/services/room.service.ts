import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Room } from '../../interfaces/room';
import { Amenity } from '../../interfaces/amenity';
import { Observable, of } from 'rxjs';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';
import { environment } from 'src/environments/environment';
import { RoomType } from 'src/interfaces/room-type';
import { Gender } from 'src/interfaces/gender';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  roomUrl = environment.endpoints.providerXYZ + 'api/';

  httpOptions: any;

  constructor(
    private httpBus: HttpClient,
    msalService: MsalService
  ) {
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: msalService.getUser().userIdentifier
      })
    };
  }

  getRoomById(id: number): Observable<Room> {
    return of(TestServiceData.room);
  }

  postRoom(room: Room, providerId: number): Observable<any> {
    const postRoomUrl = this.roomUrl + 'Room/' + providerId;
    return this.httpBus.post<Room>(postRoomUrl, JSON.parse(JSON.stringify(room)), this.httpOptions);
  }

  getRoomsByProvider(providerId: number): Observable<any> {
    const providerRoomsUrl = `${this.roomUrl}Room/provider/${providerId}`;
    return this.httpBus.get<Room[]>(providerRoomsUrl, this.httpOptions);
  }

  getRoomTypes(): Observable<any> {
    const url = this.roomUrl + 'Room/type';
    return this.httpBus.get<RoomType[]>(url, this.httpOptions);
  }

  getGenders(): Observable<any> {
    const url = this.roomUrl + 'Gender';
    return this.httpBus.get<Gender[]>(url, this.httpOptions);
  }

  getAmenities(): Observable<any> {
    const amenitiesUrl = this.roomUrl + 'Room/amenity';
    return this.httpBus.get<Amenity[]>(amenitiesUrl, this.httpOptions);
  }

  updateRoom(r: Room, providerId: number) {
    return this.httpBus.put<Room>(this.roomUrl + `Room/${providerId}`, r, this.httpOptions);
  }

  deleteRoom(r: Room, provider: number) {
    return this.httpBus.delete(this.roomUrl + `Room/${r.roomId}/provider/${provider}`, this.httpOptions);
  }
}
