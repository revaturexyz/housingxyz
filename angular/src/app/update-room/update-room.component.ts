import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Room } from 'src/models/room';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators, FormGroup, FormBuilder} from '@angular/forms';

@Component({
  selector: 'app-update-room',
  templateUrl: './update-room.component.html',
  styleUrls: ['./update-room.component.scss']
})
export class UpdateRoomComponent implements OnInit {
  [x: string]: any;

  room: Object;
  roomId: number;
  mygroup: FormGroup;
  submitted: boolean=false;
  SD: any;
  ED: any;
  TYPE: string;
  MO: number;
  rm = new Room();

  constructor(private datasvc: ApiService, private route: ActivatedRoute, private formBuilder: FormBuilder, private router: Router) { 
    this.route.params.subscribe(params => this.assignRoomId(params['id']));
  }

  ngOnInit() {
    this.getRoomInfo();
    this.mygroup = this.formBuilder.group({
      RoomID:[''],
      Type:['', [Validators.required]],
      MaxOccupancy:['',[Validators.required, Validators.min(1), Validators.minLength(1)]],
      CurrentOccupancy:['',[Validators.required, Validators.min(1), Validators.minLength(1)]],
      RoomNumber:['',[Validators.required]],
      Gender:['',[Validators.required]],
      StartDate:['',[Validators.required]],
      EndDate:['',[Validators.required]],
      Description: [''],
      LocationID: ['']
   })
  }

  assignRoomId(id: number){ this.roomId=id; }
  

  getRoomInfo()
  {
    this.datasvc.getRoomById(this.roomId).subscribe(data => {
      // this.room = data;
      // this.rm.RoomID = Object.values(data)[0];
      // this.rm.Type = Object.values(data)[1];
      // this.rm.MaxOccupancy = Object.values(data)[2];
      // this.rm.CurrentOccupancy = this.rm.MaxOccupancy;
      // this.rm.RoomNumber = Object.values(data)[3];
      // this.rm.Gender = Object.values(data)[4];
      // this.rm.StartDate = Object.values(data)[5] ;
      // this.rm.EndDate = Object.values(data)[6];
      // this.rm.LocationID = Object.values(data)[11];
      // this.rm.Description = Object.values(data)[9]
    }); 
}

  // updateRoomInfo(room: Room){
  //   this.datasvc.updateRoomData(room).subscribe(data => {
  //   });
  // }

  onSubmit() {
    //update room submit
    this.submitted = true;
    this.mygroup.controls.RoomID.setValue(this.roomId);
  
    //this.mygroup.controls.LocationID.setValue(this.rm.LocationID);
       if(this.mygroup.invalid){
         return;
       }
       else{
         //success         
         this.updateRoomInfo(this.mygroup.value);
         this.submitted = false;
         this.router.navigate(['']);
       }
  }
}
