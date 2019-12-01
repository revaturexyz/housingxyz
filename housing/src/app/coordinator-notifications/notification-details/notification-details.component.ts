import { Component, OnInit } from '@angular/core';
import { CoordinatorNotification } from '../../../interfaces/account/coordinatorNotification';

import { TestServiceData } from 'src/app/services/static-test-data';

import * as moment from 'moment';

@Component({
  selector: 'dev-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrls: ['./notification-details.component.scss']
})

// Component to allow coordinator to see complete details of notification as well as manipulate them.
export class NotificationDetailsComponent implements OnInit {

  // Seed :
  currentNotification = TestServiceData.testCoordinatorNotification;

  constructor() {
    // currentNotification = GET IT HERE LATER
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
