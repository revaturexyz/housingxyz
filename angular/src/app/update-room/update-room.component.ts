import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { ProviderService } from '../services/provider.service';
import { Observer } from 'rxjs';
import { Room } from 'src/interfaces/room';
import { RoomService } from '../services/room.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'dev-update-room',
  templateUrl: './update-room.component.html',
  styleUrls: ['./update-room.component.scss'],
  providers: [DatePipe]
})
export class UpdateRoomComponent implements OnInit {

  complexList: Complex[];
  activeComplex: Complex;
  roomList: Room[];
  complexRooms: Room[];
  show: boolean = false;

  complexObs: Observer<Complex[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.complexList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  roomsObs: Observer<Room[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.roomList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  showString = 'Choose Complex';

  constructor(private providerService: ProviderService, private roomService: RoomService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.roomService.getRoomsByProvider(1).subscribe(this.roomsObs);
  }

  complexChoose(complex: Complex) {
    this.showString = complex.complexName;
    this.activeComplex = complex;
    // console.log(this.roomList);
    this.complexRooms = this.roomList.filter(r => r.complexId === this.activeComplex.complexId);
    this.show = true;
  }
}
