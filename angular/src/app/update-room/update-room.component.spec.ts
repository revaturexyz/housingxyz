import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { LocationObject, RoomObject, UpdateRoomData, } from '../testing/dummyData';
import  { UpdateRoomComponent } from './update-room.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ApiService } from '../api.service';
import { ApiServiceMock } from '../testing/mock/mock-api-service';
import { UpdateRoomErrorList, RoomData } from '../testing/dummyData';
import { By } from '@angular/platform-browser';
import { formatDate } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

describe('UpdateRoomComponent', () => {
  let component: UpdateRoomComponent;
  let fixture: ComponentFixture<UpdateRoomComponent>;
  let errorList: string[];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateRoomComponent ],
      imports:[
        RouterTestingModule,
        FormsModule,
        HttpClientTestingModule,
        ReactiveFormsModule
      ],
      providers: [
        { provide: ApiService, useClass: ApiServiceMock },
        {
          provide: ActivatedRoute, useValue: {
            params: of({ id:1 })
          }
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    errorList = UpdateRoomErrorList.ErrorList;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

 //#region html

 it('click update button should call updateroom with roomID to', () => {
  spyOn(component, 'onSubmit');
  const button = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
  button.click();
  expect(component.onSubmit).toHaveBeenCalled();
})
//#endregion

//onNgInit()
it('should initialize formgroup by calling onNgInit()', () => {
  fixture.detectChanges();
  console.log(component.mygroup);
  //component.mygroup.controls['CurrentOccupancy'].setValue(UpdateRoomData.CurrentOccupancy);
  component.mygroup.controls['MaxOccupancy'].setValue(UpdateRoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(UpdateRoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(UpdateRoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(UpdateRoomData.StartDate);
 
   
   component.ngOnInit();
    expect(component.mygroup.controls['CurrentOccupancy'].value).toBe("");
    expect(component.mygroup.controls['MaxOccupancy'].value).toBe("");
    expect(component.mygroup.controls['RoomNumber'].value).toBe("");
    expect(component.mygroup.controls['Gender'].value).toBe("");
    expect(component.mygroup.controls['StartDate'].value).toBe("");
})

 //getRoomInfo()
 it('should assign value to room',()=>{
  //reset value
  component.room=null;
  component.getRoomInfo();
  expect(component.room).toEqual(RoomObject);
 
})

it('should not assign value to room',()=>{
  //reset room
  component.room=null;
  //force return error
  const xService = fixture.debugElement.injector.get(ApiService);
  xService['apiError']=true;
  component.getRoomInfo();
  expect(component.room).toBeFalsy();
})

// it('should  set submitted to true and show all error message', () => {
//   component.onSubmit();
//   expect(component.submitted).toBeTruthy();
//   component.mygroup.controls.roomID
//   //change in html
//   fixture.detectChanges();
//   const error = fixture.debugElement.queryAll(By.css('div.validate'));
//   //check total number of error content showed
//   expect(error.length).toBe(9);

//   for (let i = 0; i < error.length; i++) {
//     expect(error[i].nativeElement.textContent).toBe(errorList[i]);
//   }

// });
 });
