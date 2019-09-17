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
    
    apiUrl: string;
    constructor(private httpBus: HttpClient) {
    this.apiUrl = environment.endpoints.providerXYZ;
    }

    getRoomById(id: number): Observable<Room> {
        return of(TestServiceData.room);
    }
    postRoom(r: Room): Observable<Room> {
        return of(r);
    }
    getRoomsByProvider(providerId: number): Observable<Room[]> {
        return this.httpBus.get<Room[]>(this.apiUrl + `Room/provider/${providerId}`);
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
        console.log('get amentities method called.\n');
        const simpleObservable = new Observable<Amenity[]>((sub) => {
            const AList: Amenity[] = TestServiceData.dummmyList;
            sub.next(AList);
            sub.complete();
        });
        return simpleObservable;
    }

    updateRoom(r: Room, providerId: number) {
        console.log(JSON.stringify(r));
        return this.httpBus.put<Room>(this.apiUrl + `Room/${providerId}`, r);
    }

    deleteRoom(r: Room, provider: number) {
        return this.httpBus.delete(this.apiUrl + `Room/${r.roomId}/provider/${provider}`);
    }
}
