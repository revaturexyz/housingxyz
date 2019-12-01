import { Component, OnInit } from '@angular/core';
import { CoordinatorService } from 'src/app/services/coordinator.service';
import * as moment from 'moment';
import { Router } from '@angular/router';
import { PostTenant } from 'src/interfaces/postTenant';
import { Batch } from 'src/interfaces/batch';
import { RedirectService } from 'src/app/services/redirect.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'dev-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.scss']
})
export class AddTenantComponent implements OnInit {

  tenant: PostTenant;
  batch: Batch;

  trainCen: string[] = ['fa416c6e-9650-44c9-8c6b-5aebd3f9a670'];
  trainCenId: string;

  show: boolean = false;

  address = false;

  genderShowString = 'Choose Gender';
  genders: string[] = ['Male', 'Female', 'Non Binary'];

  //batch info
  batchList: Batch[] = [];
  activeBatch: Batch;
  batchShowString = 'Choose Batch';

  async postTenantOnSubmit() {
    try {
      this.tenant.trainingCenter = this.trainCen[0];
      await this.coordService.PostTenant(this.tenant).toPromise();
    } catch (err) {
      console.log(err);
    }
    //this.router.navigate(['show-tenant']);
  }

  // called when te button to add an address is clicked to display the form.

  addForm() {
    this.show = true;
  }

  addAddress() {
    this.address = true;
  }


  // called when the cancel button on the add address form is clicked to hide the form.
  back() {
    this.show = false;
  }

  return() {
    this.address = false;
  }

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

  constructor(
    private coordService: CoordinatorService,
    private router: Router
  ) {
    this.tenant = {
      email: '',
      gender: '',
      firstName: '',
      lastName: '',
      apiAddress: {
        street: '',
        city: '',
        state: '',
        country: '',
        zipCode: ''
      },
      apiCar: {
        licensePlate: null,
        make: null,
        model: null,
        color: null,
        year: null,
        state: null
      },
      batch: {
        batchLanguage: '',
        startDate: new Date(),
        endDate: new Date(),
        trainingCenter: ''
      },
      trainingCenter: ''
    };
  }

  ngOnInit() {
    this.trainCenId = 'fa416c6e-9650-44c9-8c6b-5aebd3f9a670';
    this.getBatchesOnInit();
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

  getBatchesOnInit() {
    this.coordService.GetBatchByTrainingCenterId(this.trainCenId)
      .toPromise()
      .then((data) => this.batchList = data)
      .catch((err) => console.log(err));
  }

  batchChoose(batch: Batch) {
    this.batchShowString = batch.batchLanguage;
    this.activeBatch = batch;
    this.tenant.batch = batch;
  }

  genderChoose(gender: string) {
    this.genderShowString = gender;
    this.tenant.gender = gender;
  }

  trainCenChoose(trainCen: string) {
    this.tenant.trainingCenter = trainCen;
  }

  // Used for client-side validation for date input of the form.
  verifyDates(beg: Date, end: Date): boolean {
    return new Date(beg).getTime() >= new Date(end).getTime();
  }
}
