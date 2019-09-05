import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiService } from '../api.service';
import { Room } from 'src/models/room';
import { FormControl, Validators, FormGroup, FormBuilder} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-room',
  templateUrl: './add-room.component.html',
  styleUrls: ['./add-room.component.scss']
})

export class AddRoomComponent implements OnInit {
  room: Room;
  numberOfBeds: FormControl;
  location: Object;
  rooms: boolean = false;
  mygroup: FormGroup;
  submitted: boolean = false;
  success: boolean = false;
  StartDate: Date;
  EndDate: Date;
  LocationID: number;
  
 
  constructor(private datasvc: ApiService, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router) { 

    this.route.params.subscribe(params => this.assignLocationId(params['id']));
  }
 
  ngOnInit() {
   // this.getRoomInfo();//get data when the page is loaded

    this.mygroup = this.formBuilder.group({
       Type:['',[Validators.required]],
       MaxOccupancy:['',[Validators.required,Validators.min(1)]],
       RoomNumber:['',[Validators.required]],
       Gender:['',[Validators.required]],
       StartDate:['',[Validators.required]],
       EndDate:['',[]],
       Description:['',[]]
    })
    
  }

  assignLocationId(id: number) {
    this.LocationID = id; 
  }

 //Get RoomInfo working: Test It 
  getRoomInfo(){
    //httpclient get method
    this.datasvc.getLocationData().subscribe(data => {
      this.location = data;//assign data to location object   
  })
  }
 
  postRoomInfo(value: Room){
    value.IsActive = true;
    value.RoomID = 0;
    value.CurrentOccupancy = value.MaxOccupancy;
    value.LocationID = this.LocationID;
    
     this.datasvc.postRoomData(value).subscribe(data => {
      //post success
      console.log("Post success");

    }, error => {
      //httpclient post error handling 
      console.log("Error", error);
    })
  }


  onSubmit() {
    //add room submit
    this.submitted = true;
       if(this.mygroup.invalid){
         return;
       }else
       {
         //success
         this.success = true;
         this.postRoomInfo(this.mygroup.value);
         console.log(this.mygroup.value);
         this.mygroup.reset();
         this.submitted = false;
         this.router.navigate(['']); // redirect to home
       }
  }
}
