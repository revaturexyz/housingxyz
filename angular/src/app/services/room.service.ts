import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from '../../interfaces/room';
import { Amenity } from '../../interfaces/amenity';
import { Observable, of } from 'rxjs';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class RoomService {
    roomUrl = `${ environment.endpoints.providerXYZ }Room`;

    constructor(private httpBus: HttpClient) { }

    getRoomById(id: number): Observable<Room> {
        return of(TestServiceData.room);
    }
    postRoom(r: Room): Observable<Room> {
        return of(r);
    }
    getRoomsByProvider(providerId: number): Observable<Room[]> {
        const providerRoomsUrl = `${this.roomUrl}/provider/${providerId}`;
        return this.httpBus.get<Room[]>(providerRoomsUrl);
    }
    getRoomTypes(): Observable<string[]> {
        return of(['Apartment', 'Dorm']);
    }
    getGenders(): Observable<string[]> {
        const simpleObservable = new Observable<string[]>((sub) => {
            const GenderList: string[] = TestServiceData.dummyGender;
            sub.next(GenderList);
            sub.complete();
        });
        return simpleObservable;
    }

    getAmenities(): Observable<Amenity[]> {
        const amenitiesUrl = `${this.roomUrl}/amenity`;
        console.log('Get amenities called');
        return this.httpBus.get<Amenity[]>(amenitiesUrl);
    }
}
