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

  postLivingComplex(): void {
    // verify the complex address
    this.mapsService.verifyAddress(this.formLivingComplex.apiAddress)
      .then((isValid) => {

        // set our flag and return if not
        this.isValidAddress = isValid;
        if (!this.isValidAddress) {
          return;
        }

        // get the distance
        this.mapsService.checkDistance(
          this.formLivingComplex.apiAddress,
          this.currentProvider.apiTrainingCenter.apiAddress
        ).then((distance) => {

          // set the distance flag and return if false
          this.isValidDistanceToTrainingCenter = distance <= 20;
          if (!this.isValidDistanceToTrainingCenter) {
            return;
          }

          // set the complex provider Id for our API call
          this.formLivingComplex.apiProvider.providerId = this.currentProvider.providerId;

          // call the API, post a log of our restult, and redirect
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
        });
      })
      .catch((err) => console.log(err));
  }

  cancelAddLivingComplex(): void {
    this.router.navigate(['']);
  }

  getProviderOnInit() {
    let providerId = 0;
    try {
      providerId = JSON.parse(localStorage.getItem('currentProvider')).providerId;
    } catch {
      this.router.navigate(['/login']);
    }

    this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => {
        this.currentProvider = provider;
      })
      .catch((err) => console.log(err));
  }
}
