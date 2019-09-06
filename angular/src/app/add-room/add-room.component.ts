import { Component, OnInit } from '@angular/core';
import { ShowByLocationComponent } from '../show-by-location/show-by-location.component';

@Component({
  selector: 'dev-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})
export class AddRoomComponent implements OnInit {

  show: boolean = false;

  constructor() { }

  ngOnInit() {
  }

  addForm(){
    this.show = true;
  }
  
  back(){
    this.show = false;
  }
}
