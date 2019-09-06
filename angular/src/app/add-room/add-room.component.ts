import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {
  constructor(private roomService: RoomService) { }
  getRoomTypesOnSubmit() {
    console.log(this.roomService.getRoomTypes());
  }
  ngOnInit() {
    console.log(this.roomService.getRoomTypes());
  }
}
