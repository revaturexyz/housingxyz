import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { Room } from 'src/interfaces/room';
import { DatePipe } from '@angular/common';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AmenityDialogueComponent } from '../amenity-dialogue/amenity-dialogue.component';
import { RoomService } from '../services/room.service';
import { Amenity } from 'src/interfaces/amenity';
import { Observer } from 'rxjs';
import { ComplexService } from '../services/complex.service';

@Component({
  selector: 'dev-room-update-form',
  templateUrl: './room-update-form.component.html',
  styleUrls: ['./room-update-form.component.scss'],
  providers: [DatePipe],
})
export class RoomUpdateFormComponent implements OnInit {

  editing = false;
  bedNumBool: boolean;
  roomNumBool: boolean;
  validStart: boolean;
  validEnd: boolean;
  today: Date;
  allAmen: Amenity[];
  allowModStartDate = false;

  amenObs: Observer<Amenity[]> = {
    next: x => {
      console.log('Observer got amenities value.');
      this.room.apiAmenity = x;
    },
    error: err => console.error('Observer got amenity error: ' + err),
    complete: () => console.log('Observer got amenity complete notification'),
  };
  @Input () room: Room;
  // emits an event called roomChange to the parent component
  @Output() roomChange = new EventEmitter();
  // emits an event called deleteRoom to the parent component
  @Output() deleteRoom = new EventEmitter();

  constructor(private datePipe: DatePipe, private dialog: MatDialog, private roomService: RoomService, private complexService: ComplexService) { }

  // initializes data for the dialog, and then calls it to be rendered on screen. This dialog is used
  // to change the amenities
  openDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      amen: this.allAmen,
      roomAmen: this.room.apiAmenity
    };

    const amenityUpdate = this.dialog.open(AmenityDialogueComponent, dialogConfig);

    amenityUpdate.afterClosed().subscribe(this.amenObs);
  }

  ngOnInit() {
    this.today = new Date();
    this.editing = false;
    this.complexService.getAmenityByComplex(this.room.apiComplex.complexId).subscribe(a => this.allAmen = a);
    if (this.datePipe.transform(this.today, 'yyyy-MM-dd') >= this.datePipe.transform(this.room.startDate, 'yyyy-MM-dd')) {
      this.allowModStartDate = true;
    }
  }

  // this function is bound to a button. Its only purpose is to set an internal flag for whether or not the user
  // wants to enter edit mode for the room.
  startEdit() {
    this.editing = true;
  }

  // this funciton resets the component to display nothing and await new input from parent
  resetForm() {
    this.editing = false;
    this.room = null;
  }

  // this function sets a flag related to form validation. When the user is editing and the number of beds is determined to
  // not be a valid input, this flag allows the submit button to check for validity and become disabled if input is invalid.
  bedNumValid(valid: boolean) {
    this.bedNumBool = valid;
  }

  // this function works the same way as bedNumValid(), it simply sets a flag. This one sets a flag for the room number.
  // it also follows that the input button is diabled when this flag is set.
  roomNumValid(valid: boolean) {
    this.roomNumBool = valid;
  }

  // this function updates a boolean every time the start date is changed in the date picker. If the date is invalid, it sets
  // the flag to false. Otherwise it sets it to true.
  validateStart(event: any) {
    if (this.datePipe.transform(this.today, 'yyyy-MM-dd') <= this.datePipe.transform(event, 'yyyy-MM-dd')) {
      this.validStart = true;
      this.room.startDate = event;
      if (this.datePipe.transform(this.room.endDate, 'yyyy-MM-dd') < this.datePipe.transform(event, 'yyyy-MM-dd')) {
        this.validEnd = false;
      } else {
        this.validEnd = true;
      }
    } else {
      this.validStart = false;
      if (this.datePipe.transform(this.room.endDate, 'yyyy-MM-dd') < this.datePipe.transform(event, 'yyyy-MM-dd')) {
        this.validEnd = false;
      } else {
        this.validEnd = true;
      }
    }
  }

  // this function updates a boolean every time the end date is changed in the date picker. If the date is invalid, it sets
  // the flag to false. Otherwise it sets it to true.
  validateEnd(event: any) {
    if (this.datePipe.transform(this.room.startDate, 'yyyy-MM-dd') <= this.datePipe.transform(event, 'yyyy-MM-dd')) {
      this.validEnd = true;
      this.room.endDate = event;
    } else {
      this.validEnd = false;
    }
  }

  // this function executes when a user has decided to make some edits to a room. It sets a matching room in the roomList
  // to the newly edited room, and then clears all flags so that the user's view is cleared.
  submit(r: Room) {
    this.roomChange.emit(r);
    this.resetForm();
  }

  // this function removes a room that is currently unoccupied. It only works if the room is not occupied, with a flag set externally
  removeRoom(r: Room) {
    this.deleteRoom.emit(r);
    this.resetForm();
  }
}
