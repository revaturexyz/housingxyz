import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { FormControl } from '@angular/forms';
import { MatIconRegistry } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ComplexDetailsComponent } from './complex-details/complex-details.component';
import { TestServiceData } from 'src/app/services/static-test-data';


@Component({
  selector: 'dev-manage-complex',
  templateUrl: './manage-complex.component.html',
  styleUrls: ['./manage-complex.component.scss']
})
export class ManageComplexComponent implements OnInit {

  public seededComplexes: Complex[] = [
    TestServiceData.dummyComplex,
    TestServiceData.dummyComplex2,
    TestServiceData.dummyComplex,
    TestServiceData.dummyComplex2
  ];

  // mode selection =>
  // 'init' for initial loading,
  // 'details' for after provider is selected,
  // 'add-room' for adding room,
  // 'manage-room' for editing rooms
  mode = 'init';
  complexControl = new FormControl('');

  constructor(iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'edit',
      sanitizer.bypassSecurityTrustResourceUrl('/assets/icons/edit-24px.svg'));
  }

  ngOnInit() {
  }

  changeMode(reqMode: string) {
    this.mode = reqMode;
  }

}
