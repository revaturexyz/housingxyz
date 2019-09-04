import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ProviderLocation } from 'src/Entities/location';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'dev-add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.scss']
})
export class AddLocationComponent implements OnInit {
  
  locationGroup: FormGroup;
  submitted: boolean = false;

  constructor(private datasvc: ApiService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.locationGroup = this.formBuilder.group({
      Address: ['',[Validators.required]],
      State:['',[Validators.required, Validators.min(2),Validators.max(2)]],
      City:['',[Validators.required]],
      ZipCode:['',[Validators.required,Validators.minLength(5),Validators.maxLength(5), Validators.pattern('[0-9]*')]],
      TrainingCenter:['',[Validators.required]]
    })
  }

  PostLocationInfo(obj: ProviderLocation){
    obj.ProviderID = 1;
     this.datasvc.PostLocationData(obj).subscribe(data => {
      //post location success    
      this.locationGroup.reset();
    }, error => {
      //post location error handling 
      console.log("Error", error);
    }) 
  }

  OnSubmit(){
    this.submitted = true;
    if(this.locationGroup.invalid){
      return;
    }
    else{
      this.PostLocationInfo(this.locationGroup.value);
    this.submitted = false;
    this.router.navigate(['']); // redirect to home 
    }
  }
}
