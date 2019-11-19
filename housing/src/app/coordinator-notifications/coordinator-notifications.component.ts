import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';

import { CoordinatorNotification } from '../../interfaces/coordinatorNotification';
import { Provider } from '../../interfaces/provider';

import { Router } from '@angular/router';

import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'dev-coordinator-notifications',
  templateUrl: './coordinator-notifications.component.html',
  styleUrls: ['./coordinator-notifications.component.scss']
})
export class CoordinatorNotificationsComponent implements OnInit {

  // Seed :
  notifications: Array<CoordinatorNotification> = [];

  displayedColumns = ['expires', 'companyName'];
  dataSource = new MatTableDataSource<CoordinatorNotification>(this.notifications);

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  p = 1; // Pagination control variable

  constructor( private router: Router ) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
  }

  dateCalculator(date: Date, currentNotification: CoordinatorNotification) {
    let expire = 7;
    if (currentNotification.trial) {
      expire = 7;
    } else if (currentNotification.extendedTrial) {
      expire = 30;
    }
    return moment(date).add(expire, 'days').format('MM/DD/YYYY');
  }

  getRecord(row: any) {
    console.log(row);
    this.router.navigate(['/coordinator-notifications/' + row.notificationId]);


  }

}
