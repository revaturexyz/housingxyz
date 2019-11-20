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

  formRoom: Room;

  constructor() {
    this.formRoom = this.targetRoom;
   }

  ngOnInit() {
  }

}
