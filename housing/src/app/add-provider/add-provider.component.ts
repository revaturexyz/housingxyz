import { Component, OnInit } from '@angular/core';
// import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { TrainingCenter } from 'src/interfaces/trainingcenter';
import { Address } from 'src/interfaces/address';
import { MapsService } from '../services/maps.service';
import { Router } from '@angular/router';
import { RedirectService } from '../services/redirect.service';

import { TestServiceData } from '../services/static-test-data';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'dev-add-provider',
  templateUrl: './add-provider.component.html',
  styleUrls: ['./add-provider.component.scss']
})

// Component to display form for adding a Provider to the system
export class AddProviderComponent implements OnInit {

  formProvider: Provider;

  public selectOptionInvalid = '';
  public seededComplexes = TestServiceData.testTrainingCenters;

  constructor(
    private router: Router,
    private mapsService: MapsService,
    // private providerService: ProviderService,
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
    console.log(this.formProvider);
    // This will need to post the provider into the system
    //        with an expiration of 7 days.
    // This will need to post a new notification into the system.
    //

  }

    // this method is called if the user clicks the Cancel button
    cancelAddProvider(): void {
      this.router.navigate(['']);
    }

}
