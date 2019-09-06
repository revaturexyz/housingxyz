import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/models/room';
import { ShowByLocationComponent } from '../show-by-location/show-by-location.component';
import { Amenity } from 'src/models/amenity';
import { Address } from 'src/models/address';

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
    this.roomService.getAmenities();
  }
  ngOnInit() {
    console.log(this.roomService.getRoomTypes());
    console.log(this.roomService.getRoomsByProvider(1));
  }

  addForm(){
    this.show = true;
  }
  
  back(){
    this.show = false;
  }
}
