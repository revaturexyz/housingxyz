import { Injectable } from '@angular/core';
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
        return simpleObservable;
    }
}
