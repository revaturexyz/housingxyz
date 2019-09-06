import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/models/room';
import { ShowByLocationComponent } from '../show-by-location/show-by-location.component';
import { Amenity } from 'src/models/amenity';
import { Address } from 'src/models/address';
import { ProviderLocation } from 'src/models/location';
import { Provider } from 'src/models/provider';
import { Observer } from 'rxjs';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {
  room: Room = new Room(
    null,
    new Address(1, '1001 S Center St', 'Arlington', 'TX', '76010'),
    '',
    2,
    '',
    false,
    new Amenity(1, 'washer/dryer'),
    new Date(),
    new Date(),
    1
  );
  show: boolean = false;
  providor:Provider;
  amenities: Amenity[];
  
  amenityObs: Observer<Amenity[]> = {
    next: x => {console.log('Observer got a next value.'); this.amenities = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  constructor(private roomService: RoomService) { }

  getRoomByIdOnSubmit() {
    this.roomService.getRoomById(1);
  }
  postRoomOnSubmit() {
    console.log(this.room);
    this.roomService.postRoom(this.room);
  }
  getRoomsByProviderOnSubmit() {
    this.roomService.getRoomsByProvider(1);
  }
  getRoomTypesOnSubmit() {
    this.roomService.getRoomTypes();
  }
  getGendersOnSubmit() {
    this.roomService.getGenders();
  }
  getAmenitiesOnSubmit() {
    this.roomService.getAmenities().subscribe(this.amenityObs);
  }
  ngOnInit() {
    this.getAmenitiesOnSubmit();
    console.log(this.amenityObs);
  }

  addForm(){
    this.show = true;
  }
  
  back(){
    this.show = false;
  }
}
