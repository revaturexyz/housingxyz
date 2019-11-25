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
  // the values to the provider object are set on initialization
  currentProvider: Provider;

  // these boolean flags are overwritten by promises returned from
  // the verifyAddress service method that is called by the postLivingComplex method
  // they are then attribute-bound to the template to display an error message to
  // the user if the address is invalid
  isValidAddress = true;
  isValidDistanceToTrainingCenter = true;

  // the values to the complex object are set in the constructor
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
    // This is not how redirects should work if no provider is selected.
    // It is likely a guard will need to be implemented to accomplish this task.
    this.currentProvider = this.redirect.checkProvider();
    if (this.currentProvider !== null) {
      this.getProviderOnInit(this.currentProvider.providerId).then((p) => {
        this.currentProvider = p;
      });
    }
  }

  // this method is called when the Submit button is clicked
  postLivingComplex(): void {
    // verify the complex address
    this.mapsService
      .verifyAddress(this.formLivingComplex.apiAddress)
      .then((isValid) => {
        // set our flag and return if not
        this.isValidAddress = isValid;
        if (!this.isValidAddress) {
          return;
        }

        // get the distance
        this.mapsService
          .checkDistance(this.formLivingComplex.apiAddress, this.currentProvider.apiTrainingCenter.apiAddress)
          .then((distance) => {
            // set the distance flag and return if false
            this.isValidDistanceToTrainingCenter = distance <= 20;
            if (!this.isValidDistanceToTrainingCenter) {
              return;
            }

            // set the complex provider Id for our API call
            this.formLivingComplex.apiProvider.providerId = this.currentProvider.providerId;
            // calls upon this service method to send a post request
            this.postToService() // and redirects the page after the promise is kept
              .then(() => this.router.navigate(['']));
          });
      })
      .catch((err) => console.log(err));
  }

  private postToService() {
    // call the API, post a log of our restult, and redirect
    return this.providerService // postComplex makes a post request via a HttpClient object and thus returns an Observable
      .postComplex(this.formLivingComplex, this.currentProvider.providerId)
      .toPromise()  // that first needs to be converted to a promise
      .then((result) => {
        console.log('Post is a success: ');
        console.log(result);
      })
      .catch((err) => {
        console.log('POST failed: ');
        console.log(err);
      });
  }

  // this method is called if the user clicks the Cancel button
  cancelAddLivingComplex(): void {
    this.router.navigate(['']);
  }

  // this method returns a provider from the database
  // based on its Id, this method is called when the page
  // first loads and when that's the case, should return the provider with
  // the default provider value of 1.
  getProviderOnInit(providerId: number): Promise<Provider> {
    return this.providerService
      .getProviderById(providerId)
      .toPromise()
      .then((provider) => (this.currentProvider = provider));
  }
}
