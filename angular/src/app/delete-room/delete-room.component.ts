import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-delete-room',
  templateUrl: './delete-room.component.html',
  styleUrls: ['./delete-room.component.scss']
})
export class DeleteRoomComponent implements OnInit {

  roomID: any;
  room: any;
  location: any;

  constructor( private route: ActivatedRoute, private roomSrvc: ApiService, private router: Router) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.roomID = params.get('roomId');
    });
   this.getRoomInfo();
  }

  getRoomInfo(){
    this.roomSrvc.getRoomById(this.roomID).subscribe(data =>
    {
      this.room = data
      this.getLocationInfo(this.room.locationID);
    })
    }

    getLocationInfo(id){
      this.roomSrvc.getLocationById(id).subscribe(data => {
        this.location = data
      })
    }

    deleteRoom(id){
      this.roomSrvc.deleteRoom(id).subscribe();
      this.router.navigate(['']);
    }
}