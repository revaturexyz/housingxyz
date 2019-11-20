import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Room } from '../../../interfaces/room';
import { RoomType } from '../../../interfaces/room-type';
import { Gender } from '../../../interfaces/gender';
import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {

  seededRoomTypes: Array<RoomType> = [
    { typeId: 0, roomType: 'Deluxe' },
    { typeId: 1, roomType: 'Suite' },
    { typeId: 2, roomType: 'Basement' }
  ];

  seededGenderTypes: Array<Gender> = [
    { genderId: 0, genderType: 'Unspecified' },
    { genderId: 1, genderType: 'Male' },
    { genderId: 2, genderType: 'Female' }
  ];

  formRoom: Room;

  public selectOptionRoomTypeInvalid = ''; // For all select form inputs to show invalid on validation checks.
  public selectOptionGenderInvalid = ''; // For all select form inputs to show invalid on validation checks.

  @Input() complexControl: Complex;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
    this.formRoom = {
      roomId: null,
      apiAddress: null,
      roomNumber: null,
      numberOfBeds: null,
      apiRoomType: null,
      isOccupied: false,
      apiAmenity: null,
      startDate: null,
      endDate: null,
      apiComplex: null,
      gender: null,
      leaseLength: null
    }
   }

  ngOnInit() {
  }

  postAddRoom() {
    console.log('Add Button Pressed');
    this.modeOutput.emit('details');
  }

  cancelAddRoom() {
    console.log('Cancel Button Pressed');
  }

}
