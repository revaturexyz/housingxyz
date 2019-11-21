import { Component, OnInit, Input, ViewChild, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
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
export class ComplexDetailsComponent implements OnInit {

  @Input() complexControl: Complex;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  @Output() modeOutput: EventEmitter<string> = new EventEmitter<string>();
  @Output() targetRoomOutput: EventEmitter<Room> = new EventEmitter<Room>();



  public seededRooms: Room[] = [
    TestServiceData.room,
    TestServiceData.room2
  ];

  displayedColumns = ['room#', 'start', 'end', 'edit'];

  dataSource = new MatTableDataSource<Room>(this.seededRooms);

  editRoom(room: Room)
  {
    this.targetRoomOutput.emit(room);
    this.modeOutput.emit('edit-room');
    console.log("room->", room);
  }

  changeMode(reqMode: string)
  {
    console.log("reqMode =",reqMode);
    this.modeOutput.emit(reqMode);
  }

  dateFormat(date: Date)
  {
    return moment(date).format('MM/YYYY');
  }

  constructor() { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
  }

}
