import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'dev-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  locationList: object;
  roomList: object;

    constructor(private router: Router) {

    }

    // getLocationInfo(){
    //   //httpclient get method
    //   this.datasvc.getLocationData().subscribe(data => {
    //     this.locationList=data;//assign data to location object
    //     this.getRoomInfo();
    // });
    // }

    // getRoomInfo()
    // {
    //   this.datasvc.getRoomData().subscribe(data => {
    //     this.roomList=data;
    // });
    // }

    showLocation(id: number) {
      this.router.navigate(['add-room', id]);
    }

    updateRoom(id: number) {
      this.router.navigate(['update-room', id]);
    }

  ngOnInit() { }
}
