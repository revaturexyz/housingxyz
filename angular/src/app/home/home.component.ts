import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  locationList: object;
  roomList: object;
  
    constructor(private adalSvc: MsAdalAngular6Service, private datasvc: ApiService, private router: Router) {
      console.log(this.adalSvc.userInfo);
      this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
        console.log(token);
      });
    }
    
    getLocationInfo(){
      //httpclient get method
      this.datasvc.getLocationData().subscribe(data => {
        this.locationList=data;//assign data to location object
        this.getRoomInfo();
    });
    }

    getRoomInfo()
    {
      this.datasvc.getRoomData().subscribe(data => {
        this.roomList=data;
    });
    }
    
    showLocation(id: number) {
      this.router.navigate(['add-room', id]);
    }

    updateRoom(id: number) {
      this.router.navigate(['update-room', id]);
    }

  ngOnInit() {
     // get locations belonging to the provider
     setTimeout(()=>{
      this.getLocationInfo()
     }, 100)
  }
  
}
