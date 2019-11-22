import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Room } from 'src/interfaces/room';
import { RoomType } from 'src/interfaces/room-type';
import { Gender } from 'src/interfaces/gender';
import { Complex } from 'src/interfaces/complex';

import { TestServiceData } from 'src/app/services/static-test-data';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
// Component that provides form to add room to a complex
export class AddRoomComponent implements OnInit {

  seededGenderTypes = TestServiceData.dummyGender;
  seededRoomTypes = TestServiceData.dummyRoomTypeList;

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
      gender: null
    };
   }

  ngOnInit() {
  }

  postAddRoom() {
    console.log('Add Button Pressed');
    // Handle adding room to complex logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }

  cancelAddRoom() {
    console.log('Cancel Button Pressed');
    this.modeOutput.emit('details');
  }

}
