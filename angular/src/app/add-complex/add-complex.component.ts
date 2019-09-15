import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { MapsService } from '../services/maps.service';
import { Router } from '@angular/router';

@Component({
  selector: 'dev-add-complex',
  templateUrl: './add-complex.component.html',
  styleUrls: ['./add-complex.component.scss']
})
export class AddComplexComponent implements OnInit {
  currentProvider: Provider;

  // Properties to show the address select, form, and track
  // the currently selected address.
  showAddressForm = false;
  addresses: Address[] = [];
  currentAddress: Address;
  addressDisplayString = 'Select an Address';
  isValidAddress = true;
  isValidDistanceToTrainingCenter = true;

  formLivingComplex: Complex;

  constructor(
    private router: Router,
    private mapsService: MapsService,
    private providerService: ProviderService
  ) {
    // Populate default form values
    this.currentAddress = {
      addressId: 0,
      streetAddress: '',
      city: '',
      state: '',
      zipCode: ''
    };

    this.formLivingComplex = {
      complexId: 0,
      address: this.currentAddress,
      complexName: '',
      contactNumber: ''
    };
  }

  ngOnInit() {
    this.getProviderOnInit();
    this.getAddressesOnInit();
  }

  async postLivingComplex(): Promise<void> {
    console.log('Selected address: ');
    console.log(this.currentAddress);

    this.isValidAddress = await this.mapsService.verifyAddress(this.currentAddress);
    console.log('Selected address is valid: ' + this.isValidAddress);
    if (!this.isValidAddress) {
      return;
    }

    const distance = await this.mapsService
      .checkDistance(
        this.currentAddress,
        this.currentProvider.providerTrainingCenter.address
      );
    this.isValidDistanceToTrainingCenter = distance <= 20;
    console.log('Selected address is close enough to the provider training center: ' + this.isValidDistanceToTrainingCenter);
    if (!this.isValidDistanceToTrainingCenter) {
      return;
    }

    this.formLivingComplex.address = this.currentAddress;

    // If we are showing a user the address form, then they have entered or edited an address
    // so the ID will be set to zero so it will attempt to be added as a new
    // address
    if (this.showAddressForm === true) {
      this.formLivingComplex.address.addressId = 0;
    }
    console.log(this.formLivingComplex);
  }

  cancelAddLivingComplex(): void {
    this.router.navigate(['']);
  }

  getProviderOnInit(): void {
    this.providerService.getProviderById(1)
      .toPromise()
      .then((provider) => this.currentProvider = provider)
      .catch((err) => console.log(err));
  }

  getAddressesOnInit(): void {
    this.providerService.getAddressesByProvider(2)
      .toPromise()
      .then((addresses) => this.addresses = addresses)
      .catch((err) => console.log(err));
  }

  openAddressForm(): void {
    this.showAddressForm = true;
  }

  closeAddressForm(): void {
    this.showAddressForm = false;
    this.addressDisplayString = 'Select an Address';
    this.currentAddress = {
      addressId: 0,
      streetAddress: '',
      city: '',
      state: '',
      zipCode: ''
    };
  }

  chooseAddress(address: Address): void {
    console.log('Choose address called');
    this.currentAddress = address;
    this.addressDisplayString = address.streetAddress;
  }
}
