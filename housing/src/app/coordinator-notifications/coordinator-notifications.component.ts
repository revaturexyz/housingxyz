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
  notifications: Array<CoordinatorNotification> = [
  {
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
    createdDate: new Date('11/05/2019'),
    active: false,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Lexington Properties',
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
  },
  {
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
    createdDate: new Date('11/05/2019'),
    active: false,
    trial: true,
    extendedTrial: false,
    providerDetails: {
    providerId: 3,
    companyName: 'Evergreen Properties',
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
  },
  {
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
    createdDate: new Date('11/10/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Woodside Properrties',
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
  },
  {
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
    createdDate: new Date('11/05/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Evergreen Properties',
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
  },
  {
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
    createdDate: new Date('11/10/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'A Lofty Long Name Converged through lots of merges of company negotiations to run off the screen Woodside Apartments',
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
  },
  {
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
    companyName: 'Salty Buildings',
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
  },
  {
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
    createdDate: new Date('11/05/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Stark Management',
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
  },
  {
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
    createdDate: new Date('11/10/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Woodside Courts',
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
  },
  {
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
    createdDate: new Date('11/05/2019'),
    active: true,
    trial: false,
    extendedTrial: true,
    providerDetails: {
    providerId: 3,
    companyName: 'Evergreen properties',
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
  },
];

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
