import { Component, OnInit, Input } from '@angular/core';
import { Room } from 'src/interfaces/room';
import { DatePipe } from '@angular/common';

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

  constructor(private datePipe: DatePipe) { }

  @Input () room: Room;

  ngOnInit() {
    this.today = new Date();
  }

  // this function is bound to a button. Its only purpose is to set an internal flag for whether or not the user
  // wants to enter edit mode for the room.
  startEdit() {
    this.editing = true;
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
    } else {
      this.validEnd = false;
    }
  }

  // this function executes when a user has decided to make some edits to a room. It sets a matching room in the roomList
  // to the newly edited room, and then clears all flags so that the user's view is cleared.
  submit(r: Room) {
    this.editing = false;
  }

  // this function removes a room that is currently unoccupied. It only works if the room is not occupied, with a flag set externally
  removeRoom(r: Room) {
    this.editing = false;
  }
}
