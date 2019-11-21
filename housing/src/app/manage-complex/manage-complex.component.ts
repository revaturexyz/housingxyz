import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule  } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ComplexDetailsComponent } from './complex-details/complex-details.component';
import { EditRoomComponent } from './edit-room/edit-room.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { EditComplexComponent } from './edit-complex/edit-complex.component';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { TestServiceData } from 'src/app/services/static-test-data';
import { Room } from 'src/interfaces/room';

@Component({
  selector: 'dev-manage-complex',
  templateUrl: './manage-complex.component.html',
  styleUrls: ['./manage-complex.component.scss']
})

// Component used to handle logic behind selecting and managing a complex
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
  // 'edit-room' for editing rooms,
  // 'edit-complex for editing complex
  mode = 'init';

  // target Room =>
  // variable that holds room information for editing
  targetRoom: Room;

  complexControl = new FormControl('');

  constructor() {
  }

  ngOnInit() {
  }

  changeMode(reqMode: any) {

    console.log("mode =", reqMode);
    if ( (typeof reqMode) === 'object' ){
      this.mode = 'details';
    } else {
      this.mode = reqMode;
    }
  }

  changeTargetRoom(reqRoom: Room) {
    this.targetRoom = reqRoom;
  }

}
