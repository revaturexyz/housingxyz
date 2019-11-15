import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { TrainingCenter } from 'src/interfaces/trainingCenter';
import { Address } from 'src/interfaces/address';
import { MapsService } from '../services/maps.service';
import { Router } from '@angular/router';
import { RedirectService } from '../services/redirect.service';

@Component({
  selector: 'dev-add-provider',
  templateUrl: './add-provider.component.html',
  styleUrls: ['./add-provider.component.scss']
})
export class AddProviderComponent implements OnInit {

  formProvider: Provider;

  public seededComplexes: Array<TrainingCenter> = [
    {
      centerId: 0,
      apiAddress: null,
      centerName: 'Liv +',
      contactNumber: '2143367788'
    },
    {
      centerId: 1,
      apiAddress: null,
      centerName: 'Sunshine Springs',
      contactNumber: '2141231112'
    },
    {
      centerId: 3,
      apiAddress: null,
      centerName: 'Courtshire Yards',
      contactNumber: '2141231112'
    }
  ]

  constructor(
    private router: Router,
    private mapsService: MapsService,
    private providerService: ProviderService,
    private redirect: RedirectService
    ) {
      this.formProvider = {
        providerId: 0,
        companyName: '',
        contactNumber: '',
        address: {
          addressId: 0,
          streetAddress: '',
          city: '',
          state: '',
          zipcode: ''
        },
        apiTrainingCenter: {
          centerId: 0,
          centerName: '',
          contactNumber: '',
          apiAddress: {
            addressId: 0,
            streetAddress: '',
            city: '',
            state: '',
            zipcode: ''
          }
        }
      };
     }

  ngOnInit() {
  }

  postAddProvider() {
    console.log('Handle Submit');

    // // call the API, post a log of our restult, and redirect
    // return this.providerService 
    //   .postRequestByProvider()
    //   .toPromise()  // that first needs to be converted to a promise
    //   .then((result) => {
    //     console.log('Post is a success: ');
    //     console.log(result);
    //   })
    //   .catch((err) => {
    //     console.log('POST failed: ');
    //     console.log(err);
    //   });
  }

    // this method is called if the user clicks the Cancel button
    cancelAddProvider(): void {
      this.router.navigate(['']);
    }

}
