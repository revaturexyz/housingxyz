import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from '../../interfaces/room';
import { Amenity } from '../../interfaces/amenity';
import { Observable, of } from 'rxjs';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';
import { environment } from 'src/environments/environment';
import { RoomType } from 'src/interfaces/room-type';
import { Gender } from 'src/interfaces/gender';

@Injectable({
    providedIn: 'root'
})
export class RoomService {
    roomUrl = environment.endpoints.providerXYZ;

    constructor(private httpBus: HttpClient) { }

    getRoomById(id: number): Observable<Room> {
        return of(TestServiceData.room);
    }

    postRoom(room: Room, providerId: number): Observable<Room> {
        const postRoomUrl = this.roomUrl + 'Room/' + providerId;
        return this.httpBus.post<Room>(postRoomUrl, JSON.parse(JSON.stringify(room)));
    }

    getRoomsByProvider(providerId: number): Observable<Room[]> {
        const providerRoomsUrl = `${this.roomUrl}Room/provider/${providerId}`;
        return this.httpBus.get<Room[]>(providerRoomsUrl);
    }
    getRoomTypes(): Observable<RoomType[]> {
        const url = this.roomUrl + 'Room/type';
        return this.httpBus.get<RoomType[]>(url);
    }
    getGenders(): Observable<Gender[]> {
        const url = this.roomUrl + 'Gender';
        return this.httpBus.get<Gender[]>(url);
    }

    getAmenities(): Observable<Amenity[]> {
        const amenitiesUrl = this.roomUrl + 'Room/amenity';
        return this.httpBus.get<Amenity[]>(amenitiesUrl);
    }

    updateRoom(r: Room, providerId: number) {
        return this.httpBus.put<Room>(this.roomUrl + `Room/${providerId}`, r);
    }

    deleteRoom(r: Room, provider: number) {
        return this.httpBus.delete(this.roomUrl + `Room/${r.roomId}/provider/${provider}`);
    }
}
