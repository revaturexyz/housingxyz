import { Component, OnInit, Input } from '@angular/core';
import { Room } from 'src/interfaces/room';

@Component({
  selector: 'dev-room-details',
  templateUrl: './room-details.component.html',
  styleUrls: ['./room-details.component.scss']
})
export class RoomDetailsComponent implements OnInit {

  constructor() { }

  @Input () room: Room;

  ngOnInit() {
  }

}
