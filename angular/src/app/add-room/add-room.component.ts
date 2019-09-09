import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';
import { Observer } from 'rxjs';
import { ProviderService } from '../services/provider.service';
import { Address } from 'src/interfaces/address';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})

export class AddRoomComponent implements OnInit {
  amenity1: Amenity = {
    amenityId: 1,
    amenityString:  'Washer/Dryer'
  };
  amenity2: Amenity = {
      amenityId: 2,
      amenityString: 'Smart TV'
  };
  amenity3: Amenity = {
      amenityId: 3,
      amenityString: 'Patio'
  };
  room: Room = {
    roomId: 0,
    roomAddress: {
      addressId: 0,
      streetAddress: '',
      city: '',
      state: '',
      zipCode: ''
    },
    roomNumber: '',
    numberOfBeds: 2,
    roomType: '',
    isOccupied: false,
    amenities: [this.amenity1, this.amenity2, this.amenity3],
    startDate: new Date(),
    endDate: new Date(),
    complexId: 1
  };

  show = false;
  provider: Provider;
  types: string[];
  amenities: Amenity[];

  complexList: Complex[];
  activeComplex: Complex;
  complexShowString = 'Choose Complex';

  addressList: Address[];
  activeAddress: Address;
  addressShowString = 'Choose Address';

  amenityObs: Observer<Amenity[]> = {
    next: x => {
      console.log('Observer got an amenity value.\n');
      this.amenities = x;
      console.log(this.amenities);
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
      console.log('Observer got next value: x ');
      console.log(x);
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
    this.providerService.getProviderById(1).toPromise()
      .then(
        provider => this.provider = provider,
        error => console.log(error)
      );
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
    this.room.complexId = complex.complexId;
  }

  // Updates selected address property and display string
  // based on what is selected
  addressChoose(address: Address) {
    this.addressShowString = address.streetAddress;
    this.activeAddress = address;
    this.room.roomAddress = address;
  }

  toggleLaundryAmenity() {
    if (this.room.amenities.indexOf(this.amenity1) < 0) {
      this.room.amenities.push(this.amenity1);
    } else {
      this.room.amenities.splice(this.room.amenities.indexOf(this.amenity1), 1);
    }
  }

  toggleTVAmenity() {
    if (this.room.amenities.indexOf(this.amenity2) < 0) {
      this.room.amenities.push(this.amenity2);
    } else {
      this.room.amenities.splice(this.room.amenities.indexOf(this.amenity2), 1);
    }
  }

  togglePatioAmenity() {
    if (this.room.amenities.indexOf(this.amenity3) < 0) {
      this.room.amenities.push(this.amenity3);
    } else {
      this.room.amenities.splice(this.room.amenities.indexOf(this.amenity3), 1);
    }
  }
}
