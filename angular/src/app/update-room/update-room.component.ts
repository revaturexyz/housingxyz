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

  complexObs: Observer<Complex[]> = {
    next: x => {console.log('Observer got a next value.'); this.complexList = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  roomsObs: Observer<Room[]> = {
    next: x => {console.log('Observer got a next value.'); this.roomList = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  showString: string = 'Choose Complex';

  constructor(private providerService: ProviderServiceService, private roomService: RoomService) { }

  ngOnInit() {
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.roomService.getRoomsByProvider(1).subscribe()
  }

  complexChoose(complex: Complex){
    this.showString = complex.ComplexName;
    this.activeComplex = complex;
    this.complexRooms = this.roomList.filter(r => r.ComplexID == this.activeComplex.ComplexId);
  }
}
