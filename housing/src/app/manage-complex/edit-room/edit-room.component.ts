import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as _moment from 'moment';

import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';
import { Gender } from 'src/interfaces/gender';
import { RoomType } from 'src/interfaces/room-type';
import { Amenity } from 'src/interfaces/amenity';

// import { TestServiceData } from 'src/app/services/static-test-data';

@Component({
  selector: 'dev-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.scss']
})
// Component to provide form in order to edit room
export class EditRoomComponent implements OnInit {
  // TODO: POPULATE THESE
  genderTypes: Gender[];
  roomTypes: RoomType[];
  amenityList: Amenity[];

  // For all select form inputs to show invalid on validation checks.
  public selectOptionRoomTypeInvalid = '';
  public selectOptionGenderInvalid = '';
  // These make the selected complex and room information available
  @Input() complexControl: Complex;
  @Input() targetRoom: Room;
  // Decorator to output the selected mode
  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();
  // Init for form
  formRoom: Room;

  constructor() {
   }

  ngOnInit() {
    // Populate default form values
    this.formRoom = this.targetRoom;
  }
  // Save edits and change mode back to details
  postEditRoom() {
    // Handle editing room to complex logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }
  // Delete room from db and change mode to details
  deleteRoom() {
    // Handle delete room logic here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }
  // Cancel all changes and change mode to details
  cancelEditRoom() {
    this.modeOutput.emit('details');
  }

}
