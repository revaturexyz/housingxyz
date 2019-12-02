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
  public currentNotification: Notification;

  constructor() {
    // currentNotification = GET IT HERE LATER
   }

  ngOnInit() {
  }
}
