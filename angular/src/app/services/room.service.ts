import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity} from '../../models/amenity';
import { Observable, of, from } from 'rxjs';
import { RSA_PKCS1_PADDING } from 'constants';
import { Complex } from 'src/models/complex';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
    
    
  constructor(private http : HttpClient) { }
    room: Room;
    room2: Room;
    getRoomById(id: number): Observable<Room> {
        this.room.StreetAddress = '1001 S Center St';
        this.room.City = 'Arlington';
        this.room.State = 'TX';
        this.room.ZipCode = '76010';
        this.room.RoomNumber = '2210';
        this.room.NumberOfBeds = 2;
        this.room.RoomType = 'Apartment';
        this.room.IsOccupied = false;
        this.room.Amenities = ['Patio', 'Washer/Dryer'];
        this.room.StartDate = new Date();
        this.room.EndDate = new Date();
        this.room.Complex = new Complex();
        console.log(this.room);
        return of(this.room);
    }

    postRoom(obj: Room): Observable<Room>{
        this.room.StreetAddress = obj.StreetAddress;
        this.room.City = obj.City;
        this.room.State = obj.State;
        this.room.ZipCode = obj.ZipCode;
        this.room.RoomNumber = obj.RoomNumber;
        this.room.NumberOfBeds = obj.NumberOfBeds;
        this.room.RoomType = obj.RoomType;
        this.room.IsOccupied = obj.IsOccupied;
        this.room.Amenities = obj.Amenities;
        this.room.StartDate = obj.StartDate;
        this.room.EndDate = obj.EndDate;
        this.room.Complex = obj.Complex;
        console.log(this.room);
        return of(this.room);
    }

    getRoomsByProvider(providerId: number): Observable<Room[]>{
        // return this.http.get<Room[]>("");
        this.room.StreetAddress = '1001 S Center St';
        this.room.City = 'Arlington';
        this.room.State = 'TX';
        this.room.ZipCode = '76010';
        this.room.RoomNumber = '2210';
        this.room.NumberOfBeds = 2;
        this.room.RoomType = 'Apartment';
        this.room.IsOccupied = false;
        this.room.Amenities = ['Patio', 'Washer/Dryer'];
        this.room.StartDate = new Date();
        this.room.EndDate = new Date();
        this.room.Complex = new Complex();
        this.room2.StreetAddress = '701 S Nedderman Dr';
        this.room2.City = 'Arlington';
        this.room2.State = 'TX';
        this.room2.ZipCode = '76019';
        this.room2.RoomNumber = '323';
        this.room2.NumberOfBeds = 9001;
        this.room2.RoomType = 'Dorm';
        this.room2.IsOccupied = true;
        this.room2.Amenities = ['classrooms', 'dining hall', 'students'];
        this.room2.StartDate = new Date();
        this.room2.EndDate = new Date();
        this.room2.Complex = new Complex();
        return of([this.room, this.room2]);
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
