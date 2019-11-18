import { Component, OnInit } from '@angular/core';
import { CoordinatorService } from '../services/coordinator.service';
import * as moment from 'moment';
import { Router } from '@angular/router';
import { Tenant } from '../../interfaces/tenant';

@Component({
  selector: 'dev-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.scss']
})
export class AddTenantComponent implements OnInit {

  tenant: Tenant;


  constructor(
    private coordService: CoordinatorService,
    private router: Router
    ) { }

  async postTenantOnSubmit() {
    try{
      await this.coordService.PostTenant(this.tenant);
      this.router.navigate(['show-tenant']);
    } catch(err)
    {
      console.log(err);
    }
  }
  // Posts a room to the date base.
  // async postRoomOnSubmit() {

  //   // Validate if an entered address can be considered real
  //   const isValidAddress = await this.mapservice.verifyAddress(this.room.apiAddress);
  //   console.log('Address is valid: ' + isValidAddress);
  //   if (!isValidAddress) {
  //     this.invalidAddress = true;
  //     return;
  //   }
  

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

  ngOnInit() {
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

  // Updates selected complex property and display string
  // based on what is selected
  // coordChoose(complex: Complex) {
  //   this.complexShowString = complex.complexName + ' | ' + complex.contactNumber;
  //   this.activeComplex = complex;
  //   this.room.apiComplex.complexId = complex.complexId;
  // }

  // Updates selected address property and display string
  // based on what is selected
  // langChoose(address: Address) {
  //   this.addressShowString = address.streetAddress;
  //   this.activeAddress = address;
  //   this.room.apiAddress = address;
  // }

  // Used for client-side validation for date input of the form.
  verifyDates(beg: Date, end: Date): boolean {
    return new Date(beg).getTime() >= new Date(end).getTime();
  }
}
