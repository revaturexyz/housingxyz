import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { ProviderService } from '../services/provider.service';
import { Observer } from 'rxjs';
import { Room } from 'src/interfaces/room';
import { RoomService } from '../services/room.service';
// import { templateJitUrl } from '@angular/compiler';
// import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';

@Component({
  selector: 'dev-update-room',
  templateUrl: './update-room.component.html',
  styleUrls: ['./update-room.component.scss']
})
export class UpdateRoomComponent implements OnInit {

  complexList: Complex[];
  activeComplex: Complex;
  roomList: Room[];
  complexRooms: Room[];
  mouseOverRoom: Room;
  selectedRoom: Room;
  editing = false;
  showString = 'Choose Complex';
  highlightRoom: Room;
  bedNumBool: boolean;
  roomNumBool: boolean;
  displayedColumns = ['num', 'beds', 'type', 'occ', 'amen', 'start', 'end'];
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

  constructor(private providerService: ProviderService, private roomService: RoomService) { }

  ngOnInit() {
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.roomService.getRoomsByProvider(1).subscribe(this.roomsObs);
  }

  complexChoose(complex: Complex) {
    this.showString = complex.complexName;
    this.clearSelect();
    this.activeComplex = complex;
    // console.log(this.roomList);
    this.complexRooms = this.roomList.filter(r => r.complexId === this.activeComplex.complexId);
    console.log(this.selectedRoom);
  }
  mouseOn(r: Room) {
    this.mouseOverRoom = r;
  }

  mouseOff() {
    this.mouseOverRoom = null;
  }

  select(r: Room) {
    const newRoom: Room = {
      roomId : r.roomId,
      roomAddress : r.roomAddress,
      roomNumber : r.roomNumber,
      numberOfBeds : r.numberOfBeds,
      roomType : r.roomType,
      isOccupied : r.isOccupied,
      amenities : r.amenities,
      startDate : r.startDate,
      endDate : r.endDate,
      complexId : r.complexId
    };
    this.selectedRoom = newRoom;
    this.highlightRoom = r;
  }

  startEdit() {
    this.editing = true;
  }

  clearSelect() {
    this.selectedRoom = null;
    this.highlightRoom = null;
  }

  bedNumValid(valid: boolean) {
    this.bedNumBool = valid;
    console.log(valid);
  }

  roomNumValid(valid: boolean) {
    this.roomNumBool = valid;
    console.log(valid);
  }

  submit(r: Room) {
    this.roomList.forEach(element => {
      if (element.roomId === r.roomId) {
        element = r;
      }});
    console.log(this.roomList);
  }
}
