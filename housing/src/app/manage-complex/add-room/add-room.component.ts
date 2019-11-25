import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatInputModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import * as _moment from 'moment';

import { Room } from 'src/interfaces/room';
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
  seededAmenityList = TestServiceData.dummyAmenityList1;

  formRoom: Room;

  public selectOptionRoomTypeInvalid = ''; // For all select form inputs to show invalid on validation checks.
  public selectOptionGenderInvalid = ''; // For all select form inputs to show invalid on validation checks.

  @Input() complexControl: Complex;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
   }

  ngOnInit() {
    console.log('From add-room.com ' + this.complexControl.complexId);
    this.formRoom = {
      roomId: null,
      apiAddress: this.complexControl.apiAddress,
      roomNumber: '',
      numberOfBeds: null,
      apiRoomType: null,
      isOccupied: false,
      apiAmenity: null,
      startDate: new Date(),
      endDate: new Date(),
      apiComplex: this.complexControl,
      gender: null
    };
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
