import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Address } from 'src/interfaces/address';

@Component({
  selector: 'dev-add-complex',
  templateUrl: './add-complex.component.html',
  styleUrls: ['./add-complex.component.scss']
})
export class AddComplexComponent implements OnInit {
  currentProvider: Provider;

  showAddressForm = false;

  addresses: Address[] = [];
  currentAddress: Address = {
    addressId: 0,
    streetAddress: '',
    city: '',
    state: '',
    zipCode: ''
  };
  addressDisplayString = 'Select an Address';

  constructor(
    private providerService: ProviderService
  ) { }

  ngOnInit() {
    this.getProviderOnInit();

    this.getAddressesOnInit();
  }

  getProviderOnInit(): void {
    this.providerService.getProviderById(2)
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
