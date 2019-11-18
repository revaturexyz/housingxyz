import { Component, OnInit } from '@angular/core';
import { CoordinatorNotification } from '../../../interfaces/coordinatorNotification';

import * as moment from 'moment';

import {Address} from '../../../interfaces/address';
import {TrainingCenter} from '../../../interfaces/trainingcenter';

@Component({
  selector: 'dev-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrls: ['./notification-details.component.scss']
})
export class NotificationDetailsComponent implements OnInit {

// Seed :
currentNotification: CoordinatorNotification = {
  notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
  providerId: 3,
  centerId: 2,
  centerDetails: {
  centerId: 2,
  apiAddress: {
    addressId: 5,
    streetAddress: '123 Main',
    city: 'Dallas',
    state: 'Tx',
    zipcode: '75055',
  },
  centerName: 'UT Arlington',
  contactNumber: '2143369999'
  },
  createdDate: new Date('11/15/2019'),
  active: true,
  trial: false,
  extendedTrial: true,
  providerDetails: {
  providerId: 3,
  companyName: 'Big Apartments in the sky',
  address: {
    addressId: 5,
    streetAddress: '123 Main',
    city: 'Dallas',
    state: 'Tx',
    zipcode: '75055',
  },
  contactNumber: '1234567890',
  apiTrainingCenter: {
    centerId: 7,
    apiAddress: {
      addressId: 6,
      streetAddress: '123 Main',
      city: 'Dallas',
      state: 'Tx',
      zipcode: '75055',
    },
    centerName: 'UT Arlington',
    contactNumber: '1234678999',
  }
  }
};

  constructor() {
    // currentNotification = GET IT BOY
   }

  ngOnInit() {
  }

  dateCalculator(date: Date, currentNotification: CoordinatorNotification) {
    let expire = 7;
    if (currentNotification.trial) {
      expire = 7;
    } else if (currentNotification.extendedTrial) {
      expire = 0;
    }
    return moment(date).add(expire, 'days').format('MM/DD/YYYY');
  }
}
