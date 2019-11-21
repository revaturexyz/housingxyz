import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-edit-complex',
  templateUrl: './edit-complex.component.html',
  styleUrls: ['./edit-complex.component.scss']
})
export class EditComplexComponent implements OnInit {

  @Input() targetComplex: Complex;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

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

}
