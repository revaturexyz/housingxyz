import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { HttpClient} from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity} from '../../models/amenity';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

    dummyGender: string[] = ["male", "female", "undefined"];
    dummyAmenity1: Amenity = new Amenity(1, "washer/dryer");
    dummyAmenity2: Amenity = new Amenity(2, "smart TV");
    dummmyList: Amenity[] = [this.dummyAmenity1, this.dummyAmenity2];

  constructor(private http : HttpClient) { }

    getRoomById(id: number): Observable<Room[]> {
        return this.http.get<Room[]>("");
    }

    postRoom(obj: Room): Observable<Room>{
        return this.http.post<Room>("", obj);
    }

    getRoomsByProvider(providerId: number): Observable<Room[]>{
        return this.http.get<Room[]>("");
    }

    getRoomTypes(): Observable<string[]>{
        return this.http.get<string[]>("");
    }

    getGenders(): Observable<string[]>{
        var simpleObservable = new Observable<string[]>((sub) => {
            var GenderList : string[] = this.dummyGender;
            sub.next(GenderList);
            sub.complete();
          });
        return simpleObservable;
    }
    
    getAmenities(): Observable<Amenity[]>{
        var simpleObservable = new Observable<Amenity[]>((sub) => {
            var GenderList : Amenity[] = this.dummmyList;
            sub.complete();
          });
=======
import { HttpClient } from '@angular/common/http';
import { Room } from '../../interfaces/room';
import { Amenity } from '../../interfaces/amenity';
import { Observable, of } from 'rxjs';
import { Address } from 'src/interfaces/address';
import { TestServiceData } from './static-test-data';

@Injectable({
    providedIn: 'root'
})
export class RoomService {

    constructor(private http: HttpClient) { }

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
    getAmenities(): Observable<Amenity[]> {
        console.log('get amentities method called.\n');
        const simpleObservable = new Observable<Amenity[]>((sub) => {
            const AList: Amenity[] = TestServiceData.dummmyList;
            sub.next(AList);
            sub.complete();
        });
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
        return simpleObservable;
    }
}
