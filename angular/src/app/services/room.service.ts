import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity } from '../../models/amenity';
import { Observable, of } from 'rxjs';
import { Address } from 'src/models/address';

@Injectable({
    providedIn: 'root'
})
export class RoomService {
    dummyGender: string[] = ['male', 'female', 'undefined'];
    dummyAmenity1: Amenity = new Amenity(1, 'washer/dryer');
    dummyAmenity2: Amenity = new Amenity(2, 'smart TV');
    dummmyList: Amenity[] = [this.dummyAmenity1, this.dummyAmenity2];
    room: Room = new Room(
        null,
        new Address(1, '1001 S Center St', 'Arlington', 'TX', '76010'),
        '',
        2,
        '',
        false,
        this.dummyAmenity1,
        new Date(),
        new Date(),
        1
    );
    room2: Room = new Room(
        null,
        new Address(2, '701 S Nedderman Dr', 'Arlington', 'TX', '76019'),
        '323',
        9001,
        'Dorm',
        true,
        new Amenity(2, 'Washer/Dryer'),
        new Date(),
        new Date(),
        2
    );
    postToRoom: Room;
    postAddress: Address;
    constructor(private http: HttpClient) { }
    getRoomById(id: number): Observable<Room> {
        return of(this.room);
    }
    postRoom(r: Room): Observable<Room> {
        return of(r);
    }
    getRoomsByProvider(providerId: number): Observable<Room[]> {
        return of([this.room, this.room2]);
    }
    getRoomTypes(): Observable<string[]> {
        return of(['Apartment', 'Dorm']);
    }
    getGenders(): Observable<string[]> {
        const simpleObservable = new Observable<string[]>((sub) => {
            const GenderList: string[] = this.dummyGender;
            sub.next(GenderList);
            sub.complete();
        });
        return simpleObservable;
    }
    getAmenities(): Observable<Amenity[]> {
        const simpleObservable = new Observable<Amenity[]>((sub) => {
            const GenderList: Amenity[] = this.dummmyList;
            sub.complete();
        });
        return simpleObservable;
    }
}
