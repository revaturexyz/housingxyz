import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/interfaces/room';
import { MapsService } from '../services/maps.service';
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
  validAddress: boolean;
  validDistanceToTraining: boolean;
  validDistanceToComplex: boolean;

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
    amenities: null,
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

  constructor(
    private roomService: RoomService,
    private providerService: ProviderService,
    private mapservice: MapsService
  ) { }

  ngOnInit() {
    // Populate the user options and form
    // data objects
    this.getRoomTypesOnInit();
    this.getAmenitiesOnInit();
    this.getComplexesOnInit();
    this.getAddressesOnInit();
    this.providerService.getProviderById(1).toPromise()
      .then(
        provider => this.provider = provider,
        error => console.log(error)
      );
    console.log(this.roomService.getRoomTypes());
    console.log(this.roomService.getRoomsByProvider(1));
    console.log(this.types);
  }


  async postRoomOnSubmit() {
    const isValidAddress = await this.mapservice.verifyAddress(this.room.roomAddress);
    console.log('Address is valid: ' + isValidAddress);
    if (isValidAddress) {
      const isValidDistanceTrainerCenter =
                    await this.mapservice.checkDistance(this.room.roomAddress, this.provider.providerTrainingCenter.streetAddress);
      if ( isValidDistanceTrainerCenter <= 20) {
        const isValidDistanceComplex = await this.mapservice.checkDistance(this.room.roomAddress, this.activeComplex.streetAddress);
        if ( isValidDistanceComplex <= 2 ) {
          this.validAddress = false;
          this.validDistanceToTraining = false;
          this.validDistanceToComplex = false;
          if (this.show) {
            if (this.room.roomAddress.addressId > 0) {
              this.room.roomAddress.addressId = 0;
            }
          }
          this.room.amenities = this.amenities.filter(y => y.isSelected);
          console.log(this.room);
          this.roomService.postRoom(this.room);
        } else {
          this.validDistanceToComplex = true;
        }
      } else {
        this.validDistanceToTraining = true;
      }
    } else {
      this.validAddress = true;
    }
  }

  // Called in OnInit to populate the addresses list
  getAddressesOnInit() {
    this.providerService.getAddressesByProvider(1).toPromise()
      .then(
        (data) => {
          console.log("Received response for get addresses");
          this.addressList = data;
        })
      .catch(
        (error) => console.log(error)
      );
  }

  // Called in OnInit to populate the amenities list
  getAmenitiesOnInit() {
    this.roomService.getAmenities().toPromise()
      .then( 
        (data) => {
          console.log("Received response for get amenities");
          this.amenities = data;
        })
      .catch(
        (error) => console.log(error)
      );
  }

  // Called in OnInit to populate the complexes list
  getComplexesOnInit() {
    this.providerService.getComplexes(1).toPromise()
      .then( (data) => {
        console.log("Received response for get complexes");
        this.complexList = data;
      })
      .catch(
        (error) => console.log(error)
      );
  }

  // Called in OnInit to populate room types list
  getRoomTypesOnInit() {
    this.roomService.getRoomTypes().toPromise()
      .then(
        (data) => {
          console.log("Received response for get room types");
          this.types = data;
        })
      .catch(
        (error) => console.log(error)
      );
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
}
