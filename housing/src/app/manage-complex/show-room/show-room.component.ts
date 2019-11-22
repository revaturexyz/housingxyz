import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';

import { TestServiceData } from 'src/app/services/static-test-data';

@Component({
  selector: 'dev-show-room',
  templateUrl: './show-room.component.html',
  styleUrls: ['./show-room.component.scss']
})
export class ShowRoomComponent implements OnInit {

  seededGenderTypes = TestServiceData.dummyGender;
  seededRoomTypes = TestServiceData.dummyRoomTypeList;

  public selectOptionRoomTypeInvalid = ''; // For all select form inputs to show invalid on validation checks.
  public selectOptionGenderInvalid = ''; // For all select form inputs to show invalid on validation checks.

  @Input() complexControl: Complex;
  @Input() targetRoom: Room;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

}
