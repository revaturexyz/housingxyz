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

  isValidAddress = true;
  isValidDistanceToTrainingCenter = true;

  formLivingComplex: Complex;

  constructor(
    private router: Router,
    private mapsService: MapsService,
    private providerService: ProviderService
  ) {
    // Populate default form values
    this.formLivingComplex = {
      complexId: 0,
      apiProvider: {
        providerId: 0,
        address: null,
        contactNumber: '',
        companyName: '',
        apiTrainingCenter: null
      },
      apiAddress: {
        addressId: 0,
        streetAddress: '',
        city: '',
        state: '',
        zipCode: ''
      },
      complexName: '',
      contactNumber: ''
    };
  }

  ngOnInit() {
    this.getProviderOnInit();
  }

  async postLivingComplex(): Promise<void> {
    console.log('Entered address: ');
    console.log(this.formLivingComplex.apiAddress);

    this.isValidAddress = await this.mapsService.verifyAddress(this.formLivingComplex.apiAddress);
    console.log('Selected address is valid: ' + this.isValidAddress);
    if (!this.isValidAddress) {
      return;
    }

    const distance = await this.mapsService
      .checkDistance(
        this.formLivingComplex.apiAddress,
        this.currentProvider.apiTrainingCenter.apiAddress
      );
    this.isValidDistanceToTrainingCenter = distance <= 20;
    console.log('Selected address is close enough to the provider training center: ' + this.isValidDistanceToTrainingCenter);
    if (!this.isValidDistanceToTrainingCenter) {
      return;
    }

    this.formLivingComplex.apiProvider.providerId = this.currentProvider.providerId;
    console.log(this.formLivingComplex);
    this.providerService.postComplex(this.formLivingComplex, this.currentProvider.providerId)
      .toPromise()
      .then((result) => {
        console.log('Post is a success: ');
        console.log(result);
        this.router.navigate(['']);
      })
      .catch((err) => {
        console.log('POST failed: ');
        console.log(err);
        this.router.navigate(['']);
      });
  }

  cancelAddLivingComplex(): void {
    this.router.navigate(['']);
  }

  getProviderOnInit() {
    this.providerService.getProviderById(1)
      .toPromise()
      .then((provider) => {
        this.currentProvider = provider;
        console.log(provider);
      })
      .catch((err) => console.log(err));
  }
}
