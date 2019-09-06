import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/models/room';
import { ShowByLocationComponent } from '../show-by-location/show-by-location.component';
import { Complex } from 'src/models/complex';
import { Observer } from 'rxjs';
import { ProviderServiceService } from '../services/provider-service.service';
import { Address } from 'src/models/address';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {
  room: Room;
  show: boolean = false;

  complexList: Complex[];
  activeComplex: Complex;
  complexShowString: string = "Choose Complex";

  addressList: Address[];
  activeAddress: Address;
  addressShowString: string = "Choose Address";
  
  constructor(
    private roomService: RoomService,
    private providerService: ProviderServiceService
    ) { }

  getRoomByIdOnSubmit() {
    this.roomService.getRoomById(1);
  }
  /*postRoomOnSubmit() {
    this.roomService.postRoom(this.room);
  }*/
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
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.providerService.getAddressesByProvider(1).subscribe(this.addressObs);
    console.log(this.roomService.getRoomTypes());
    console.log(this.roomService.getRoomsByProvider(1));
  }

  // An Observer for receiving and prcessing return values
  // from the providerService.getComplexes() method
  complexObs: Observer<Complex[]> = {
    next: x => {console.log('Observer got a next value.'); this.complexList = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  // An Observer for receiving and processing return values
  // from the providerService.getAddressesByProvider() method
  addressObs: Observer<Address[]> = {
    next: x => {console.log('Observer got next value.'); this.addressList = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  addForm(){
    this.show = true;
  }
  
  back(){
    this.show = false;
  }

  // Updates selected complex property and display string
  // based on what is selected
  complexChoose(complex: Complex) {
    this.complexShowString = complex.ComplexName + ' | ' + complex.ContactNumber ;
    this.activeComplex = complex;
  }

  // Updates selected address property and display string
  // based on what is selected
  addressChoose(address: Address) {
    this.addressShowString = address.StreetAddress;
    this.activeAddress = address;
  }
}
