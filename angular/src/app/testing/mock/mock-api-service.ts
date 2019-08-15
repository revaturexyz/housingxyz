import { Injectable } from '@angular/core';
import {  of, throwError, Observable } from 'rxjs';
import { RoomObject, LocationObject } from '../dummyData';



@Injectable()
export class ApiServiceMock {
  apiError=false;
  constructor() { }

  setError(){
    this.apiError = true;
  }
  PostLocationData(obj:any){
    if(!this.apiError)
      return of("success");
    else{
      return throwError( new Error("failed") );
    }     
  }
  postRoomData(obj:any){
    if(!this.apiError)
      return of("success");
      else{
        return throwError(new Error("failed"));
      }
  }

  getRoomById(id: number)
  {
    if(!this.apiError)
      return of(RoomObject);
    else{
      return of();
    }     
  }

  getLocationById(id:number){
    if(!this.apiError)
      return of(LocationObject);
    else
      return of();
  }

  deleteRoom(id:number){
    return of();
  }


}