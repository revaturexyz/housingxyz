import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/models/complex';
import { ProviderServiceService } from '../services/provider-service.service';
import { Observer } from 'rxjs';
import { Room } from 'src/models/room';
import { RoomService } from '../services/room.service';

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
  editing: boolean = false;
  showString = 'Choose Complex';
  highlightRoom: Room;
  

  complexObs: Observer<Complex[]> = {
    next: x => {console.log('Observer got a next value.'); this.complexList = x; },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  roomsObs: Observer<Room[]> = {
    next: x => {console.log('Observer got a next value.'); this.roomList = x; },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  constructor(private providerService: ProviderServiceService, private roomService: RoomService) { }

  ngOnInit() {
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.roomService.getRoomsByProvider(1).subscribe(this.roomsObs);
  }

  complexChoose(complex: Complex) {
    this.showString = complex.ComplexName;
    this.selectedRoom = null;
    this.activeComplex = complex;
    // console.log(this.roomList);
    this.complexRooms = this.roomList.filter(r => r.ComplexID === this.activeComplex.ComplexId);
  }
  mouseOn(r: Room) {
    this.mouseOverRoom = r;
  }

  mouseOff() {
    this.mouseOverRoom = null;
  }

  select(r: Room) {
    this.selectedRoom = new Room(r.RoomID, r.Address, r.RoomNumber, r.NumberOfBeds, r.RoomType, r.IsOccupied, r.Amenities, r.StartDate, r.EndDate, r.ComplexID);
    this.highlightRoom = r;
  }
  startEdit() {
    this.editing = true;
  }
}
