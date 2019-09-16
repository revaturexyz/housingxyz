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
    apiUrl: string = environment.endpoints['localhost'];
    constructor(private http: HttpClient) {  }

    getRoomById(id: number): Observable<Room> {
        return of(TestServiceData.room);
    }
    postRoom(r: Room): Observable<Room> {
        return of(r);
    }
    getRoomsByProvider(providerId: number): Observable<Room[]> {
        return of([TestServiceData.room, TestServiceData.room2]);
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
    // returns an observable of an array of amenity from the database
    // these amenities appear on the add room form
    getAmenities(): Observable<Amenity[]> {
        console.log('get amentities method called.\n');
        return this.http.get<Amenity[]>(this.apiUrl + 'Room/amenity');
    }
}
