import { Component, OnInit } from '@angular/core';
import { Notification } from '../../../interfaces/account/notification';

import * as moment from 'moment';

@Component({
  selector: 'dev-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrls: ['./notification-details.component.scss']
})

// Component to allow coordinator to see complete details of notification as well as manipulate them.
export class NotificationDetailsComponent implements OnInit {
  constructor() {
    // currentNotification = GET IT HERE LATER
   }

  ngOnInit() {
  }

  public currentNotification: Notification;

  // Date stuff is done on backend. This function is on life support
  /* dateCalculator(date: Date, currentNotification: Notification) {
    let expire = 7;
    if (currentNotification.trial) {
      expire = 7;
    } else if (currentNotification.extendedTrial) {
      expire = 0;
    }
    return moment(date).add(expire, 'days').format('MM/DD/YYYY');
  } */
}
