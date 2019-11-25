import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TestServiceData } from 'src/app/services/static-test-data';

import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-edit-complex',
  templateUrl: './edit-complex.component.html',
  styleUrls: ['./edit-complex.component.scss']
})

// Component used to provide form in order to edit complex
export class EditComplexComponent implements OnInit {

  // This makes the details of the complex you want to edit avalible
  @Input() targetComplex: Complex;
  // Decorator to output the selected mode
  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();
  // seed for simulating all amenities
  seededAmenityList = TestServiceData.dummyAmenityList1;
  // Init for form binding
  formLivingComplex: Complex;

  constructor() {
   }

  ngOnInit() {
    // Populate default form values
    this.formLivingComplex = this.targetComplex;
  }

  // to save edits to db and change mode back to details
  putEditComplex() {
    // Handle Submit Here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }

  // to cancel all changes and change mode back to deatils
  cancelEditComplex() {
    this.modeOutput.emit('details');
  }

  // to delete complex from db and change mode back to details
  deleteComplex() {
    // Handle Delete Here
    this.modeOutput.emit('details');
  }

  // This will be used to select defaults for amenity selection list
  compareWithFunc(a, b) {
    return a.name === b.name;
  }

}
