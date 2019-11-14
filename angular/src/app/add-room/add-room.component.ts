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
import { Router } from '@angular/router';
import { RoomType } from 'src/interfaces/room-type';
import { RedirectService } from '../services/redirect.service';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})

export class AddRoomComponent implements OnInit {

  // These booleans are used to either show or not show the error messages for
  // entered and selected addresses to make sure they meet our validation and distance thresholds
  invalidAddress: boolean;
  invalidDistanceToTraining: boolean;
  invalidDistanceToComplex: boolean;

  // the form Room object
  // It is a default room needed to help the form with two way binding.
  // Initialized below.
  room: Room;

  // Moments objects used to create validation for the date picker.
  // An easier way to store and manipulate the dates for proper validation.
  startDate = moment();
  midDate = this.startDate.clone().add(6, 'months');
  endDate = this.startDate.clone().add(2, 'y');
  freshDate;

  // These variables are used so that when we convert the moments into strings
  // they are stored in the proper format to use in the HTML.
  displayStart;
  displayMid;
  displayEnd;

  // This variable is used to display the add address form when a new address needs
  // to be created.
  show = false;

  // values for displaying and allowing selection
  // of the room type, amenities, and the provider.
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
    // The variable is declared above.
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

  // This methods runs when the component is called.
  ngOnInit() {
    // This is not how redirects should work if no provider is selected.
    // It is likely a guard will need to be implemented to accomplish this task.
    this.provider = this.redirect.checkProvider();
    if (this.provider !== null) {
      // If so we get the providers information, complexes, and the address
      this.getProviderOnInit(this.provider.providerId).then(p => {
        this.provider = p;

        this.getComplexesOnInit();
        this.getAddressesOnInit();
      });
    }

    // Here we get the room types, amenities, and we work with moments for
    // the date picker validation.
    this.setUpMomentData();

    this.getRoomTypesOnInit();
    this.getAmenitiesOnInit();
  }

  // This method is used to help with the date picker validation
  private setUpMomentData() {
    // We create a moment with todays date.
    this.freshDate = moment();
    // If todays date is greater than the start date of the date picker.
    if (this.freshDate > this.startDate) {
      // Here we change the startDate to today so that the user can not pick a date later than today.
      // We then add 6 months (the furthest the start date can be) and then add 2 years
      // ( the furthest that an end date can be)
      this.midDate = this.startDate.clone().add(6, 'months');
      this.endDate = this.startDate.clone().add(2, 'y');
    }
    // Here we format and conver the moments into string to be used in the HTML
    this.displayStart = this.startDate.format('YYYY-MM-DD');
    this.displayMid = this.midDate.format('YYYY-MM-DD');
    this.displayEnd = this.endDate.format('YYYY-MM-DD');
  }

  // Posts a room to the date base.
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
    if (!(isValidDistanceComplex <= 5)) {
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

  // called when te button to add an address is clicked to display the form.
  addForm() {
    this.show = true;
  }

  // called when the cancel button on the add address form is clicked to hide the form.
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

  // Called in OnInit to get the provider that was choosen at the beginning.
  getProviderOnInit(providerId: number): Promise<Provider> {
    return this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => this.provider = provider);
  }

  // Used for client-side validation for date input of the form.
  verifyDates(beg: Date, end: Date): boolean {
    return new Date(beg).getTime() >= new Date(end).getTime();
  }
}
