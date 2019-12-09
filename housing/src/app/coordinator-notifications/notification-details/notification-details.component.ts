import { Component, OnInit } from '@angular/core';
import { Notification } from '../../../interfaces/account/notification';

@Component({
  selector: 'dev-notification-details',
  templateUrl: './notification-details.component.html',
  styleUrls: ['./notification-details.component.scss']
})
// Component to allow coordinator to see complete details of notification as well as manipulate them.
export class NotificationDetailsComponent implements OnInit {
  public currentNotification: Notification = {
    notificationId: null,
    coordinatorId: null,
    providerId: null,
    updateAction: null,
    status: null,
    createdAt: null
  };

  constructor() {
    // currentNotification = GET IT HERE LATER
  }

  ngOnInit() {
  }
}
