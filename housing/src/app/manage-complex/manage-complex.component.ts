import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { FormControl } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'dev-manage-complex',
  templateUrl: './manage-complex.component.html',
  styleUrls: ['./manage-complex.component.scss']
})
export class ManageComplexComponent implements OnInit {

  public seededComplexes: Array<Complex> = [
    {
      complexId: 1,
      apiAddress: {
        addressId: 1,
        streetAddress: "843 test street",
        city: "Arlington",
        state: "Texas",
        zipcode: "76010"
      },
      apiProvider: {
        providerId: 1,
        companyName: "Stark Agency",
        address: {
          addressId: 2,
          streetAddress: "999 test drive",
          city: "Arlington",
          state: "Texas",
          zipcode: "76020"
        },
        contactNumber: "(893) 783-4848",
        apiTrainingCenter: {
          centerId: 1,
          apiAddress:
          {
            addressId: 1,
            streetAddress: "843 test street",
            city: "Arlington",
            state: "Texas",
            zipcode: "76010"
          },
          centerName: "Revature",
          contactNumber: "(383) 384-4992"
        }
      },
      complexName: "Liv +",
      contactNumber: "(728) 738-4737"

    },
  ]

  complexControl = new FormControl('');
  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) { 
    iconRegistry.addSvgIcon(
      'edit',
      sanitizer.bypassSecurityTrustResourceUrl('../../assets/icons/edit-24px.svg'));
  }

  ngOnInit() {
  }

}
