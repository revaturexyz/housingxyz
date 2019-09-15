import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { MapsService } from '../services/maps.service';

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
  isValidAddress: boolean;


  formLivingComplex: Complex;

  constructor(
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
    this.isValidAddress = await this.mapsService.verifyAddress(this.currentAddress);

    this.formLivingComplex.address = this.currentAddress;
    console.log(this.formLivingComplex);
  }

  cancelAddLivingComplex(): void {

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
