import { Component, OnInit, Input, ViewChild, EventEmitter, Output } from '@angular/core';
import { Complex } from 'src/interfaces/complex';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { Room } from '../../../interfaces/room';
import { TestServiceData } from 'src/app/services/static-test-data';
import * as moment from 'moment';


export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

@Component({
  selector: 'dev-complex-details',
  templateUrl: './complex-details.component.html',
  styleUrls: ['./complex-details.component.scss']
})

// Component to show the selected complex's details
export class ComplexDetailsComponent implements OnInit {

  @Input() complexControl: Complex;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  // Decorator to output the selected mode
  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();

  // Decorator to output the targeted room
  @Output() targetRoomOutput: EventEmitter<Room> = new EventEmitter<Room>();


  // seededRooms =>
  // import dummy room data
  public seededRooms: Room[] = [
    TestServiceData.room,
    TestServiceData.room2
  ];

  // id's for columns on material table
  displayedColumns = ['room#', 'start', 'end', 'edit'];

  // data source for material table
  dataSource = new MatTableDataSource<Room>(this.seededRooms);

  // editRoom =>
  // once called, output targeted room object and change mode to edit targeted room
  editRoom(room: Room) {
    this.targetRoomOutput.emit(room);
    this.modeOutput.emit('edit-room');
  }

  // changeMode =>
  // only change the current mode to whatever is specified in the html
  changeMode(reqMode: string) {
    this.modeOutput.emit(reqMode);
  }

  // dateFormat =>
  // function to format a Date object to 'MM/YYYY'
  dateFormat(date: Date) {
    return moment(date).format('MM/YYYY');
  }

  constructor() { }

  ngOnInit() {
    // Links paginator for material table
    this.dataSource.paginator = this.paginator;
  }

}
