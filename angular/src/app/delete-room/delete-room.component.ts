import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-delete-room',
  templateUrl: './delete-room.component.html',
  styleUrls: ['./delete-room.component.scss']
})
export class DeleteRoomComponent implements OnInit {
  ///id of the room you would like to delete (passed from the previous page)
  roomID: any;
  /// holds the room object
  room: any;
  /// holds the location object
  location: any;

  constructor( private route: ActivatedRoute, private roomSrvc: ApiService, private router: Router) { }

  ngOnInit() {
    /// get room id that was passed from the previous page from the url
    this.route.paramMap.subscribe(params => {
      this.roomID = params.get('roomId');
    });
   this.getRoomInfo();
  }
   /// get information for the room id which was passed from the previous page
  getRoomInfo(){
    this.roomSrvc.getRoomById(this.roomID).subscribe(data =>
    {
      this.room = data
      ///get location inforamtion from the location id in the room details
      this.getLocationInfo(this.room.locationID);
    })
    }
     /// call the api to get room info
    getLocationInfo(id){
      this.roomSrvc.getLocationById(id).subscribe(data => {
        this.location = data
      })
    }
    /// set the rooms "isValid property to false"
    deleteRoom(id){
      this.roomSrvc.deleteRoom(id).subscribe();
      this.router.navigate(['']);
    }
}