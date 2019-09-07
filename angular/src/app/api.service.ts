import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Room } from 'src/models/room';
import { Observable } from 'rxjs';
import { ProviderLocation } from 'src/models/location';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})


export class ApiService {

  private locationUrl = 'http://localhost:57249/api/locations/';
  private roomUrl = 'http://localhost:57249/api/rooms/';

  constructor(private http: HttpClient) { }

  getLocationData() {
    return this.http.get(this.locationUrl);
  }

  getLocationById(id: any) {
    return this.http.get(this.locationUrl + id);
  }

  getRoomData() {
    return this.http.get(this.roomUrl);
  }

  getRoomById(id: number) {
    return this.http.get<Room>(`${this.roomUrl}${id}`);
  }

  getRoomsByLocationId(id: number) {
    return this.http.get(this.roomUrl + 'location/' + id);
  }

  postRoomData(obj: Room): Observable<Room> {
    return this.http.post<Room>(this.roomUrl, obj);
  }


  PostLocationData(obj: ProviderLocation): Observable<ProviderLocation> {
    return this.http
      .post<ProviderLocation>(this.locationUrl, obj);
  }

  updateRoomData(obj: Room): Observable<void> {
    return this.http.put<void>(`${this.roomUrl}${obj.roomID}`, obj, httpOptions);
  }

  deleteRoom(id: number): Observable<{}> {
    return this.http.delete(this.roomUrl + id, httpOptions);
  }
}
