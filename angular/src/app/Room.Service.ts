import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../models/room';
import { Amenity} from '../models/Amenity';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })

export class ApiService {
    
    constructor(private http : HttpClient) { }

    getRoomById(d: number): Observable<Room[]> {
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

    // getGenders(): Observable<Gender[]>{

    // }
    
    getAmenities(): Observable<Amenity[]>{
        return this.http.get<Amenity[]>("");
    }
}