import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import * as angular from 'angular';
import { Router } from '@angular/router';

@Component({
  selector: 'dev-show-by-location',
  templateUrl: './show-by-location.component.html',
  styleUrls: ['./show-by-location.component.scss']
})
export class ShowByLocationComponent implements OnInit {

  locationList: any;
  locationID: any;
  roomList: any;

  constructor(private datasvc: ApiService, private router: Router) { }

  selectOption(id: any)
  {
    this.locationID = id;
    console.log(id);
    this.getRoomInfoByLocation();
  }

  getLocationInfo(){
    //httpclient get method
    this.datasvc.getLocationData().subscribe(data => {
      this.locationList=data;//assign data to location object
  });
  }

  getRoomInfoByLocation(){
    this.datasvc.getRoomsByLocationId(this.locationID).subscribe(data=>{
      this.roomList=data;
    })
  }

  updateRoom(id: number) {
    this.router.navigate(['update-room', id]);
  }

  showLocation() {
    this.router.navigate(['/add-room', Number(this.locationID)]);
  }

  ngOnInit() {
    this.getLocationInfo();  
  }
}
