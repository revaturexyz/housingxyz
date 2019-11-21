import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Room } from '../../../interfaces/room';
import { RoomType } from '../../../interfaces/room-type';
import { Gender } from '../../../interfaces/gender';
import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-edit-room',
  templateUrl: './edit-room.component.html',
  styleUrls: ['./edit-room.component.scss']
})
export class EditRoomComponent implements OnInit {

  @Input() complexControl: Complex;
  @Input() targetRoom: Room;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  formRoom: Room;

  constructor() {
    this.formRoom = this.targetRoom;
   }

  ngOnInit() {
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
  }

}
