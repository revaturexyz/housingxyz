import { Component, OnInit } from '@angular/core';
import { RoomService } from '../services/room.service';
import { Room } from 'src/interfaces/room';
import { MapsService } from '../services/maps.service';
import { Complex } from 'src/interfaces/complex';
import { ProviderService } from '../services/provider.service';
import { Address } from 'src/interfaces/address';
import { Provider } from 'src/interfaces/provider';
import { Amenity } from 'src/interfaces/amenity';
import * as moment from 'moment';
import { TestServiceData } from '../services/static-test-data';
import { Router } from '@angular/router';
import { RoomType } from 'src/interfaces/room-type';
import { RedirectService } from '../services/redirect.service';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})

export class AddRoomComponent implements OnInit {

  // values to verify entered and selected address
  // meet our validation and distance thresholds
  invalidAddress: boolean;
  invalidDistanceToTraining: boolean;
  invalidDistanceToComplex: boolean;

  // the form Room object
  room: Room;

  // moments objects used to create validation for the date picker.
  // Also some string variables to use them in the Html.
  startDate = moment();
  midDate = this.startDate.clone().add(6, 'months');
  endDate = this.startDate.clone().add(2, 'y');
  freshDate;
  displayStart;
  displayMid;
  displayEnd;


  // used for hiding or displaying the address form
  show = false;

  // room form data
  provider: Provider;
  types: RoomType[] = [];
  amenities: Amenity[] = [];

  // values for displaying and allowing selection
  // of living complexes
  complexList: Complex[] = [];
  activeComplex: Complex;
  complexShowString = 'Choose Complex';

  // values for displaying and allowing selection
  // of addresses
  addressList: Address[] = [];
  activeAddress: Address;
  addressShowString = 'Choose Address';

  constructor(
    private router: Router,
    private roomService: RoomService,
    private providerService: ProviderService,
    private mapservice: MapsService,
    private redirect: RedirectService
  ) {
    // Initialize an empty address and room object for our
    // forms
    this.room = {
      roomId: 0,
      apiAddress: {
        addressId: 0,
        streetAddress: '',
        city: '',
        state: '',
        zipcode: ''
      },
      roomNumber: '',
      numberOfBeds: 4,
      apiRoomType: null,
      isOccupied: false,
      apiAmenity: null,
      startDate: new Date(),
      endDate: new Date(),
      apiComplex: {
        complexId: 0,
        complexName: '',
        contactNumber: '',
        apiProvider: null,
        apiAddress: null
      }
    };
  }

  ngOnInit() {
    this.provider = this.redirect.checkProvider();
    if (this.provider !== null) {
      this.getProviderOnInit(this.provider.providerId).then(p => {
        this.provider = p;

        this.getComplexesOnInit();
        this.getAddressesOnInit();
      });
    } else {
    }

    this.setUpMomentData();

    this.getRoomTypesOnInit();
    this.getAmenitiesOnInit();
  }

  private setUpMomentData() {
    this.freshDate = moment();
    if (this.freshDate > this.startDate) {
      this.midDate = this.startDate.clone().add(6, 'months');
      this.endDate = this.startDate.clone().add(2, 'y');
    }
    this.displayStart = this.startDate.format('YYYY-MM-DD');
    this.displayMid = this.midDate.format('YYYY-MM-DD');
    this.displayEnd = this.endDate.format('YYYY-MM-DD');
  }

  async postRoomOnSubmit() {

    // Validate if an entered address can be considered real
    const isValidAddress = await this.mapservice.verifyAddress(this.room.apiAddress);
    console.log('Address is valid: ' + isValidAddress);
    if (!isValidAddress) {
      this.invalidAddress = true;
      return;
    }

    // Get and verify that the distance from a room to the provider
    // training center is less than or equal to 20 miles
    console.log('Validating distance to training center');
    const isValidDistanceTrainerCenter =
      await this.mapservice.checkDistance(
        this.room.apiAddress,
        this.provider.apiTrainingCenter.apiAddress);
    if (!(isValidDistanceTrainerCenter <= 20)) {
      this.invalidDistanceToTraining = true;
      return;
    }

    // Get and validate that the distance from a room to a provider
    // living complex is less than or equal to five miles
    console.log('Validating distance to living complex');
    console.log('Living complex address: ' + this.activeComplex.apiAddress);
    const isValidDistanceComplex =
      await this.mapservice.checkDistance(
        this.room.apiAddress,
        this.activeComplex.apiAddress);
    if (!(isValidDistanceComplex <= 5 )) {
      this.invalidDistanceToComplex = true;
      return;
    }

    // Reset our flags.
    this.invalidAddress = false;
    this.invalidDistanceToTraining = false;
    this.invalidDistanceToComplex = false;

    // If we are showing the address form but an address was previously
    // selected, then we can assume that a new address has been entered
    // and must set the address ID to zero
    if (this.show) {
      if (this.room.apiAddress.addressId > 0) {
        this.room.apiAddress.addressId = 0;
      }
    }

    // Set the amenities list values in the room based on what is
    // selected
    this.room.apiAmenity = this.amenities.filter(y => y.isSelected);

    console.log(this.room);

    try {
      await this.roomService.postRoom(this.room, this.provider.providerId).toPromise();
      this.router.navigate(['show-rooms']);
    } catch (err) {
      console.log(err);
    }
  }

  // Called in OnInit to populate the addresses list
  getAddressesOnInit() {
    this.providerService.getAddressesByProvider(this.provider.providerId)
      .toPromise()
      .then((data) => this.addressList = data)
      .catch((err) => console.log(err));
  }

  // Called in OnInit to populate the amenities list
  getAmenitiesOnInit() {
    this.roomService.getAmenities()
      .toPromise()
      .then((data) => this.amenities = data)
      .catch((err) => console.log(err));
  }

  // Called in OnInit to populate the complexes list
  getComplexesOnInit() {
    this.providerService.getComplexesByProvider(this.provider.providerId)
      .toPromise()
      .then((data) => this.complexList = data)
      .catch((err) => console.log(err));
  }

  // Called in OnInit to populate room types list
  getRoomTypesOnInit() {
    this.roomService.getRoomTypes()
      .toPromise()
      .then((data) => this.types = data)
      .catch((err) => console.log(err));
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
    this.room.apiComplex.complexId = complex.complexId;
  }

  // Updates selected address property and display string
  // based on what is selected
  addressChoose(address: Address) {
    this.addressShowString = address.streetAddress;
    this.activeAddress = address;
    this.room.apiAddress = address;
  }


  getProviderOnInit(providerId: number): Promise<Provider> {
    console.log('this runs before error');
    return this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => this.provider = provider);
  }

  // Used for client-side validation for date input of the form.
  verifyDates(beg: Date, end: Date): boolean {
    return new Date(beg).getTime() >= new Date(end).getTime();
  }
}
