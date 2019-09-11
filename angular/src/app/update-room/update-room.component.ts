import { Component, OnInit } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { ProviderService } from '../services/provider.service';
import { Observer } from 'rxjs';
import { Room } from 'src/interfaces/room';
import { RoomService } from '../services/room.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'dev-update-room',
  templateUrl: './update-room.component.html',
  styleUrls: ['./update-room.component.scss'],
  providers: [DatePipe]
})
export class UpdateRoomComponent implements OnInit {

  complexList: Complex[];
  activeComplex: Complex;
  roomList: Room[];
  complexRooms: Room[];
  mouseOverRoom: Room;
  selectedRoom: Room;
  editing = false;
  showString = 'Choose Complex';
  highlightRoom: Room;
  bedNumBool: boolean;
  roomNumBool: boolean;
  validStart: boolean;
  validEnd: boolean;
  today: Date;

  // observer for the service request that returns an observable of complexes.
  // sets the internal complexList: Complex[] to valid data.
  complexObs: Observer<Complex[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.complexList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  // observer for the observable that returns a list of rooms.
  // this just sets the internal roomList: Room[] object to valid data.
  roomsObs: Observer<Room[]> = {
    next: x => {
      console.log('Observer got a next value.');
      this.roomList = x;
    },
    error: err => console.error('Observer got an error: ' + err),
    complete: () => console.log('Observer got a complete notification'),
  };

  // injects dependency on Provider and Room services.
  constructor(private providerService: ProviderService, private roomService: RoomService, private datePipe: DatePipe) { }

  // initializes complexes and all rooms by providers at init time.
  ngOnInit() {
    this.providerService.getComplexes(1).subscribe(this.complexObs);
    this.roomService.getRoomsByProvider(1).subscribe(this.roomsObs);
    this.today = new Date();
  }

  // funciton that runs when a complex is selected from the dropdown in the HTML.
  // sets two internal variables:
  //     showString which has default value of "Choose Complex"
  //     activeComplex which simply houses the currently selected complex. It is null by default.
  complexChoose(complex: Complex) {
    this.showString = complex.complexName;
    this.clearSelect();
    this.activeComplex = complex;
    this.complexRooms = this.roomList.filter(r => r.complexId === this.activeComplex.complexId);
    this.editing = false;
  }

  // this function is bound to an HTML element, and executes every time the mouse is detected to be hovering over the element.
  mouseOn(r: Room) {
    this.mouseOverRoom = r;
  }

  // this is the counterpart to mouseOn(). It executes whenever an element determines the mouse is no longer hovering it.
  mouseOff() {
    this.mouseOverRoom = null;
  }

  // this function executes when a room display component is clicked. It sets a working room equal to the selected room
  // in order to preserve data integrity while updates are made.
  // It also internally sets a variable, highlightRoom, to let the HTML know which room is selected and thus highlight it.
  select(r: Room) {
    const newRoom: Room = {
      roomId : r.roomId,
      roomAddress : r.roomAddress,
      roomNumber : r.roomNumber,
      numberOfBeds : r.numberOfBeds,
      roomType : r.roomType,
      isOccupied : r.isOccupied,
      amenities : r.amenities,
      startDate : r.startDate,
      endDate : r.endDate,
      complexId : r.complexId
    };
    this.selectedRoom = newRoom;
    this.highlightRoom = r;
  }

  // this function is bound to a button. Its only purpose is to set an internal flag for whether or not the user
  // wants to enter edit mode for the room.
  startEdit() {
    this.editing = true;
  }

  // helper function to disable previously made selections and stop the display of both, the table at the bottom of the page,
  // and the highlight around a selected displayed room.
  clearSelect() {
    this.selectedRoom = null;
    this.highlightRoom = null;
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

  // this function executes when a user has decided to make some edits to a room. It sets a matching room in the roomList
  // to the newly edited room, and then clears all flags so that the user's view is cleared.
  submit(r: Room) {
    this.roomList.forEach(element => {
      if (element.roomId === r.roomId) {
        element.roomId = r.roomId;
        element.roomAddress = r.roomAddress;
        element.roomNumber = r.roomNumber;
        element.numberOfBeds = r.numberOfBeds;
        element.roomType = r.roomType;
        element.isOccupied = r.isOccupied;
        element.amenities = r.amenities;
        element.startDate = r.startDate;
        element.endDate = r.endDate;
        element.complexId = r.complexId;
      }
    });
    this.selectedRoom = null;
    this.highlightRoom = null;
    this.editing = false;
  }

  // this function updates a boolean every time the start date is changed in the date picker. If the date is invalid, it sets
  // the flag to false. Otherwise it sets it to true.
  validateStart(event: any) {
    if (this.datePipe.transform(this.today, 'yyyy-MM-dd') <= this.datePipe.transform(event, 'yyyy-MM-dd')) {
      this.validStart = true;
      if (this.datePipe.transform(this.selectedRoom.endDate, 'yyyy-MM-dd') < this.datePipe.transform(event, 'yyyy-MM-dd')) {
        this.validEnd = false;
      } else {
        this.validEnd = true;
      }
    } else {
      this.validStart = false;
      if (this.datePipe.transform(this.selectedRoom.endDate, 'yyyy-MM-dd') < this.datePipe.transform(event, 'yyyy-MM-dd')) {
        this.validEnd = false;
      } else {
        this.validEnd = true;
      }
    }
  }

  // this function updates a boolean every time the end date is changed in the date picker. If the date is invalid, it sets
  // the flag to false. Otherwise it sets it to true.
  validateEnd(event: any) {
    if (this.datePipe.transform(this.selectedRoom.startDate, 'yyyy-MM-dd') <= this.datePipe.transform(event, 'yyyy-MM-dd')) {
      this.validEnd = true;
    } else {
      this.validEnd = false;
    }
  }
}
