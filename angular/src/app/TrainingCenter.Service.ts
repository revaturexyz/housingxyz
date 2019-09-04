import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../models/room';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })

export class ApiService {
    
    constructor(private http : HttpClient) { }

    getRoomById(d: number): Observable<Room[]> {
        return this.http.get<Room[]>("");
    }
    // postRoom(room: Room): Observable 

    // }
    // getRoomById(roomId: int): Observable<Room>
    // getRoomsByProvider(providerId: int): Observable<Room[]>
    // getRoomTypes(): Observable<RoomType[]>
    // getGenders(): Observable<Gender[]>
}