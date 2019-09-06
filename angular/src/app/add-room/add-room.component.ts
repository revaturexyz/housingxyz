import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/models/room';
import { Complex } from 'src/models/complex';
import { Observer } from 'rxjs';
import { ProviderServiceService } from '../services/provider-service.service';
import { Address } from 'src/models/address';
import { Provider } from 'src/models/provider';
import { Amenity } from 'src/models/amenity';

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

  complexList: Complex[];
  activeComplex: Complex;
  complexShowString: string = "Choose Complex";

  addressList: Address[];
  activeAddress: Address;
  addressShowString: string = "Choose Address";

  providor:Provider;
  amenities: Amenity[];

  constructor(
    private roomService: RoomService,
    private providerService: ProviderServiceService
    ) { }
  
  amenityObs: Observer<Amenity[]> = {
    next: x => {console.log('Observer got a next value.'); this.amenities = x},
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

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
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.providerService.getAddressesByProvider(1).subscribe(this.addressObs);
    console.log(this.roomService.getRoomTypes());
    console.log(this.roomService.getRoomsByProvider(1));
    this.getAmenitiesOnSubmit();
    console.log(this.amenityObs);
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
