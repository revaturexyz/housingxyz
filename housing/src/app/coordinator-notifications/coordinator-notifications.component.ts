import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'dev-coordinator-notifications',
  templateUrl: './coordinator-notifications.component.html',
  styleUrls: ['./coordinator-notifications.component.scss']
})
export class CoordinatorNotificationsComponent implements OnInit {

  p = 1; // Pagination control variable
  public notifications: Array<any> = [
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: '3 days',
    companyName: 'Stark Properties, Inc'
    },
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: '3 days',
    companyName: 'Lark Properties, Inc Singing by the Grassy Path at the Downtown Disco'
    },
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: '3 days',
    companyName: 'Park Properties, Inc'
    },
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: '3 days',
    companyName: 'Wthark Properties, Inc'
    },
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: '3 days',
    companyName: 'Mzzzark Properties, Inc'
    },
    {
      notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
      providerId: 2,
      centerId: 2,
      date: '3 days',
      companyName: 'Stark Properties, Inc'
      },
      {
      notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
      providerId: 2,
      centerId: 2,
      date: '3 days',
      companyName: 'Lark Properties, Inc Singing by the Grassy Path at the Downtown Disco'
      },
      {
      notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
      providerId: 2,
      centerId: 2,
      date: '11/21/2007',
      companyName: 'Park Properties, Inc'
      },
      {
      notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
      providerId: 2,
      centerId: 2,
      date: '11/21/2007',
      companyName: 'Wthark Properties, Inc'
      },
      {
      notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
      providerId: 2,
      centerId: 2,
      date: '11/21/2007',
      companyName: 'Mzzzark Properties, Inc'
      }
  ]

  constructor() { }

  ngOnInit() {
  }

}
