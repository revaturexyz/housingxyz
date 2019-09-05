import { Injectable } from '@angular/core';

import {  of, throwError, Observable } from 'rxjs';

import { mockLocationList, mockRoomList } from '../dummyData';

import { RoomObject, LocationObject } from '../dummyData';
import { Room } from 'src/Entities/room';







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

  getLocationData(){

    if(!this.apiError)


      return of(mockLocationList);

    else{

      return throwError(new Error("failed") );

    }

  }

  getRoomData(){

    if(!this.apiError)


      return of(mockRoomList);
          else{

      return throwError(new Error("failed"));

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

  updateRoomData(room: any){
    return of();
  }

  getRoomsByLocationId(id:number){
    if(!this.apiError)
      return of(mockRoomList);
    else
      return of();
  }

 



}