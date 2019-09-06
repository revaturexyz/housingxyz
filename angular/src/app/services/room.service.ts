import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity } from '../../models/amenity';
import { Observable, of, from } from 'rxjs';
import { Complex } from 'src/models/complex';
import { Address } from 'src/models/address';

@Injectable({
    providedIn: 'root'
})
export class RoomService {
    dummyGender: string[] = ['male', 'female', 'undefined'];
    dummyAmenity1: Amenity = new Amenity(1, 'washer/dryer');
    dummyAmenity2: Amenity = new Amenity(2, 'smart TV');
    dummmyList: Amenity[] = [this.dummyAmenity1, this.dummyAmenity2];
    room: Room = new Room(null, new Address(1, '1001 S Center St', 'Arlington', 'TX', '76010'),
        '2210', 2, 'Apartment', false, new Amenity(1, 'Patio'), new Date(), new Date(), 1);
    room2: Room = new Room(null, new Address(2, '701 S Nedderman Dr', 'Arlington', 'TX', '76019'),
        '323', 9001, 'Dorm', true, new Amenity(2, 'Washer/Dryer'), new Date(), new Date(), 2);
    constructor(private http: HttpClient) { }
    getRoomById(id: number): Observable<Room> {
        return of(this.room);
    }
    postRoom(obj: Room): Observable<Room> {
        return of(new Room(obj.RoomID, obj.Address, obj.RoomNumber, obj.NumberOfBeds,
            obj.RoomType, obj.IsOccupied, obj.Amenities, obj.StartDate, obj.EndDate, obj.ComplexID));
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
