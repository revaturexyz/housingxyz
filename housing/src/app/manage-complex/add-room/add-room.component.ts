import { Component, OnInit } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  // roomId: number;
  // apiAddress: Address;
  // roomNumber: string;
  // numberOfBeds: number;
  // apiRoomType: RoomType;
  // isOccupied: boolean;
  // apiAmenity: Amenity[];
  // startDate: Date;
  // endDate: Date;
  // apiComplex: Complex;
  // gender: string;
  // leaseLength: number;


  postAddRoom() {
    console.log('Add Button Pressed');
  }

  cancelAddRoom() {
    console.log('Cancel Button Pressed');
  }

}
