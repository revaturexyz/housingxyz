import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'dev-coordinator-notifications',
  templateUrl: './coordinator-notifications.component.html',
  styleUrls: ['./coordinator-notifications.component.scss']
})
export class CoordinatorNotificationsComponent implements OnInit {


  public notifications: Array<any> = [
    {
    notificationId: 'fabfd4f2-9e08-4950-a13b-574b6b8beade',
    providerId: 2,
    centerId: 2,
    date: Date,
    companyName: 'Stark Properties, Inc'
    },
    {
      notificationId: 6,
      centerId: 0,
      apiAddress: null,
      centerName: 'Liv +',
      contactNumber: '2143367788'
    },
    {
      notificationId: 7,
      centerId: 1,
      apiAddress: null,
      centerName: 'Sunshine Springs on the Hillside Waterfall Incoporated by the Rainbow over Dreams Lost in Code',
      contactNumber: '2141231112'
    },
    {
      notificationId: 8,
      centerId: 3,
      apiAddress: null,
      centerName: 'Courtshire Yards',
      contactNumber: '2141231112'
    },
    {
      notificationId: 9,
      centerId: 4,
      apiAddress: null,
      centerName: 'Liv +',
      contactNumber: '2143367788'
    },
    {
      notificationId: 1,
      centerId: 5,
      apiAddress: null,
      centerName: 'Sunshine Springs',
      contactNumber: '2141231112'
    },
    {
      notificationId: 2,
      centerId: 6,
      apiAddress: null,
      centerName: 'Courtshire Yards',
      contactNumber: '2141231112'
    },
    {
      notificationId: 3,
      centerId: 0,
      apiAddress: null,
      centerName: 'Liv +',
      contactNumber: '2143367788'
    },
    {
      notificationId: 4,
      centerId: 1,
      apiAddress: null,
      centerName: 'Sunshine Springs',
      contactNumber: '2141231112'
    },
    {
      notificationId: 5,
      centerId: 3,
      apiAddress: null,
      centerName: 'Courtshire Yards',
      contactNumber: '2141231112'
    },
    {
      notificationId: 6,
      centerId: 4,
      apiAddress: null,
      centerName: 'Liv +',
      contactNumber: '2143367788'
    },
    {
      notificationId: 7,
      centerId: 5,
      apiAddress: null,
      centerName: 'Sunshine Springs',
      contactNumber: '2141231112'
    },
    {
      notificationId: 8,
      centerId: 6,
      apiAddress: null,
      centerName: 'Courtshire Yards',
      contactNumber: '2141231112'
    }
  ]

  constructor() { }

  ngOnInit() {
  }

}
