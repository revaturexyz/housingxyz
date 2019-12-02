import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Room } from 'src/interfaces/room';
import { Complex } from 'src/interfaces/complex';

// import { TestServiceData } from 'src/app/services/static-test-data';

@Component({
  selector: 'dev-show-room',
  templateUrl: './show-room.component.html',
  styleUrls: ['./show-room.component.scss']
})
export class ShowRoomComponent implements OnInit {
  // Seeded data for view testing
 // seededGenderTypes = TestServiceData.dummyGender;
 // seededRoomTypes = TestServiceData.dummyRoomTypeList;
  // For all select form inputs to show invalid on validation checks.
  public selectOptionRoomTypeInvalid = '';
  public selectOptionGenderInvalid = '';
  // These make the selected complex and room information available
  @Input() complexControl: Complex;
  @Input() targetRoom: Room;
  // Decorator to output the selected mode
  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }
  // Button handler for when user clicks back button
  handleBackButton() {
    this.modeOutput.emit('details');
  }
}
