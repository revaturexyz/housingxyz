import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';
import { Observer } from 'rxjs';
import { ProviderService } from '../services/provider.service';
import { Address } from 'src/interfaces/address';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})

export class AddRoomComponent implements OnInit {
  room: Room = {
    roomId: 0,
    roomAddress: {
      addressId: 1,
      streetAddress: '1001 S Center St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '76010'
    },
    roomNumber: '',
    numberOfBeds: 2,
    roomType: '',
    isOccupied: false,
    amenities: {
      amenityId: 1,
      amenityString: 'washer/dryer'
    },
    startDate: new Date(),
    endDate: new Date(),
    complexId: 1
  };
  show = false;

  complexList: Complex[];
  activeComplex: Complex;
  complexShowString = 'Choose Complex';

  addressList: Address[];
  activeAddress: Address;
  addressShowString = 'Choose Address';

  providor: Provider;
  amenities: Amenity[];
  types: string[];

  amenityObs: Observer<Amenity[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.amenities = x;
  },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  typeObs: Observer<string[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.types = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  // An Observer for receiving and prcessing return values
  // from the providerService.getComplexes() method
  complexObs: Observer<Complex[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.complexList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  // An Observer for receiving and processing return values
  // from the providerService.getAddressesByProvider() method
  addressObs: Observer<Address[]> = {
    next: x => {
      console.log('Observer got next value.');
      this.addressList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  constructor(
    private roomService: RoomService,
    private providerService: ProviderService
  ) { }

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
    this.roomService.getRoomTypes().subscribe(this.typeObs);
  }
  getGendersOnSubmit() {
    this.roomService.getGenders();
  }
  getAmenitiesOnSubmit() {
    this.roomService.getAmenities().subscribe(this.amenityObs);
  }
  ngOnInit() {
    this.getRoomTypesOnSubmit();
    this.getAmenitiesOnSubmit();
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.providerService.getAddressesByProvider(1).subscribe(this.addressObs);
    console.log(this.roomService.getRoomTypes());
    console.log(this.roomService.getRoomsByProvider(1));
    console.log(this.types);
  }

  addForm() {
    this.show = true;
  }

  back() {
    this.show = false;
  }

  // Updates selected complex property and display string
  // based on what is selected
  complexChoose(complex: Complex) {
    this.complexShowString = complex.complexName + ' | ' + complex.contactNumber;
    this.activeComplex = complex;
  }

  // Updates selected address property and display string
  // based on what is selected
  addressChoose(address: Address) {
    this.addressShowString = address.streetAddress;
    this.activeAddress = address;
  }
}
