import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
  // Init Form
  formRoom: Room;
  // For all select form inputs to show invalid on validation checks.
  public selectOptionRoomTypeInvalid = '';
  public selectOptionGenderInvalid = '';
  // Makes currently selected complex information available
  @Input() complexControl: Complex;
  // Decorator to output the selected mode
  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  constructor() {
   }

  ngOnInit() {
    // Sets form defaults
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
      apiComplex: this.complexControl
    };
  }
  // Adds room to complex and switches mode back to details
  postAddRoom() {
    // Handle adding room to complex logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }
  // Changes mode back to details
  cancelAddRoom() {
    this.modeOutput.emit('details');
  }
}
