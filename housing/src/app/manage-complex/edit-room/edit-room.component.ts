import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import * as _moment from 'moment';

import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';

import { TestServiceData } from 'src/app/services/static-test-data';

@Component({
  selector: 'dev-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.scss']
})
// Component to provide form in order to edit room
export class EditRoomComponent implements OnInit {

  seededGenderTypes = TestServiceData.dummyGender;
  seededRoomTypes = TestServiceData.dummyRoomTypeList;

  public selectOptionRoomTypeInvalid = ''; // For all select form inputs to show invalid on validation checks.
  public selectOptionGenderInvalid = ''; // For all select form inputs to show invalid on validation checks.

  @Input() complexControl: Complex;
  @Input() targetRoom: Room;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  formRoom: Room;

  constructor() {
   }

  ngOnInit() {
    console.log('from edit room ' + this.complexControl);
    console.log('from edit room ' + this.targetRoom.isOccupied);
    // Populate default form values
    this.formRoom = this.targetRoom;
  }

  postEditRoom() {
    console.log('Add Button Pressed');
    // Handle editing room to complex logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }

  deleteRoom() {
    console.log('Delete Button Pressed');
    // Handle delete room logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }

  cancelEditRoom() {
    console.log('Cancel Button Pressed');
    this.modeOutput.emit('details');
  }

}
