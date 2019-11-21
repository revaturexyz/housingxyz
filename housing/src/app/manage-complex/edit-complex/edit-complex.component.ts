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

  @Input() targetComplex: Complex;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  seededAmenityList = TestServiceData.dummyAmenityList1; // seed for simulating all amenities

  formLivingComplex: Complex;

  constructor() {
   }

  ngOnInit() {
    // Populate default form values
    this.formLivingComplex = this.targetComplex;
  }

  putEditComplex() {
    console.log('Edit Complex Submit');
    // Handle Submit Here
    this.modeOutput.emit('details'); // Sent to parent to change mode back to details
  }

  cancelEditComplex() {
    console.log('Cancel Complex Changes');
    this.modeOutput.emit('details');
  }

  deleteComplex() {
    console.log('Cancel Complex Changes');
    // Handle Delete Here
    this.modeOutput.emit('details');
  }

  // This will be used to select defaults for amenity selection list
  compareWithFunc(a, b) {
    return a.name === b.name;
  }

}
