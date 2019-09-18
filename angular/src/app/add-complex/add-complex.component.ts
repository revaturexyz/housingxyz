import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { MapsService } from '../services/maps.service';
import { Router } from '@angular/router';
import { RedirectService } from '../services/redirect.service';

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
    private providerService: ProviderService,
    private redirect: RedirectService
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
        zipcode: ''
      },
      complexName: '',
      contactNumber: ''
    };
  }

  ngOnInit() {
    this.currentProvider = this.redirect.checkProvider();
    if (this.currentProvider !== null) {
      this.getProviderOnInit(this.currentProvider.providerId)
      .then(p => {
        this.currentProvider = p;
      });
    } else {
    }
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
          )
          .then((distance) => {
            // set the distance flag and return if false
            this.isValidDistanceToTrainingCenter = distance <= 20;
            if (!this.isValidDistanceToTrainingCenter) {
              return;
            }

            // set the complex provider Id for our API call
            this.formLivingComplex.apiProvider.providerId = this.currentProvider.providerId;

            this.postToService()
              .then(() => this.router.navigate(['']));
          });
        })
      .catch((err) => console.log(err));
  }

  private postToService() {
    // call the API, post a log of our restult, and redirect
    return this.providerService.postComplex(this.formLivingComplex, this.currentProvider.providerId)
      .toPromise()
      .then((result) => {
        console.log('Post is a success: ');
        console.log(result);
      })
      .catch((err) => {
        console.log('POST failed: ');
        console.log(err);
    });
  }

  cancelAddLivingComplex(): void {
    this.router.navigate(['']);
  }

  getProviderOnInit(providerId: number): Promise<Provider> {
    return this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => this.currentProvider = provider);
  }
}
