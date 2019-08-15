import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AddRoomComponent } from './add-room.component';
import { By } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule, NgControlStatusGroup } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ModuleWithComponentFactories } from '@angular/core';
import {RouterTestingModule}  from '@angular/router/testing'
import {Room} from 'src/Entities/room';
import {RoomData,RoomErrorList} from '../testing/dummyData'
import { ApiService } from '../api.service';
import { ApiServiceMock } from '../testing/mock/mock-api-service';


describe('AddRoomComponent', () => {

  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;
  let errorList: string[];


  beforeEach(async(() => {

    TestBed.configureTestingModule({
      declarations: [ AddRoomComponent ],
      imports:[
        FormsModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        RouterTestingModule
      ],
      providers: [
        { provide: ApiService, useClass: ApiServiceMock }

      ]

    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    errorList = RoomErrorList.ErrorList;
  });
//methods
it('should create', () => {
  expect(component).toBeTruthy();
});
//onNgInit()
it('should initialize formgroup by calling onNgInit()', () => {
  fixture.detectChanges();
  console.log(component.mygroup);
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
 
   
   component.ngOnInit();
    expect(component.mygroup.controls['Type'].value).toBe("");
    expect(component.mygroup.controls['MaxOccupancy'].value).toBe("");
    expect(component.mygroup.controls['RoomNumber'].value).toBe("");
    expect(component.mygroup.controls['Gender'].value).toBe("");
    expect(component.mygroup.controls['StartDate'].value).toBe("");
})
  //onSubmit
 it('should  set submitted to true and show all error message', () => {
  component.onSubmit();
  expect(component.submitted).toBeTruthy();
  fixture.detectChanges();
  //check total number of error content showed
  const errors = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(errors.length).toBe(5);
  for (let i = 0; i < errors.length; i++) {
    expect(errors[i].nativeElement.textContent).toBe(errorList[i]);
  }

});

 it('should only show type is required', () => {
  //input some data to form
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
  component.onSubmit();
  fixture.detectChanges();

  const error = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(error.length).toBe(1);
  expect(error[0].nativeElement.textContent).toBe(errorList[0]);
});

 it('should only show maxoccupancy is required', () => {
   //input some data to form
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
  component.onSubmit();
  fixture.detectChanges();

  const error = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(error.length).toBe(1);
  expect(error[0].nativeElement.textContent).toBe(errorList[1]);
});

it('should only show RoomNumber is required', () => {
  //input some data to form
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
  component.onSubmit();
  fixture.detectChanges();

  const error = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(error.length).toBe(1);
  expect(error[0].nativeElement.textContent).toBe(errorList[2]);
});

it('should only show Gender is required', () => {
  //input some data to form
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
  component.onSubmit();
  fixture.detectChanges();

  const error = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(error.length).toBe(1);
  expect(error[0].nativeElement.textContent).toBe(errorList[3]);
});

it('should only show StartDate is required', () => {
  //input some data to form
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.onSubmit();
  fixture.detectChanges();

  const error = fixture.debugElement.queryAll(By.css('div.validate'));
  expect(error.length).toBe(1);
  expect(error[0].nativeElement.textContent).toBe(errorList[4]);
});

it('it should not have error message and should call postRoomInfo', () => {
  //input some data to form
  component.mygroup.controls['Type'].setValue(RoomData.Type);
  component.mygroup.controls['MaxOccupancy'].setValue(RoomData.MaxOccupancy);
  component.mygroup.controls['RoomNumber'].setValue(RoomData.RoomNumber);
  component.mygroup.controls['Gender'].setValue(RoomData.Gender);
  component.mygroup.controls['StartDate'].setValue(RoomData.StartDate);
  

  //spy on postRoomInfo method
  spyOn(component, 'postRoomInfo');
  component.onSubmit();
  //call postRoomInfo
  expect(component.postRoomInfo).toHaveBeenCalledWith(RoomData);
  expect(component.submitted).toBeFalsy();


  //formgroup reset
  expect(component.mygroup.controls['Type'].value).toBe(null);
  expect(component.mygroup.controls['MaxOccupancy'].value).toBe(null);
  expect(component.mygroup.controls['RoomNumber'].value).toBe(null);
  expect(component.mygroup.controls['Gender'].value).toBe(null);
  expect(component.mygroup.controls['StartDate'].value).toBe(null);

});
// //#endregion


it('should post room info into database', () => {
  //create providerLocation object
  const room = new Room();
  room.Type= RoomData.Type;
  room.MaxOccupancy= RoomData.MaxOccupancy;
  room.RoomNumber= RoomData.RoomNumber;
  room.Gender= RoomData.Gender;
  room.StartDate= RoomData.StartDate;
  room.EndDate= RoomData.EndDate;
  room.Description= RoomData.Description;

  component.postRoomInfo(room);

})

//need change
it('should get error from post request', () => {
  //create providerLocation object
  const room = new Room();
  room.Type = RoomData.Type;
  room.MaxOccupancy = RoomData.MaxOccupancy;
  room.RoomNumber = RoomData.RoomNumber;
  room.Gender = RoomData.Gender;
  room.StartDate = RoomData.StartDate;
  //get service first
  const xService = fixture.debugElement.injector.get(ApiService);
  xService['apiError']=true;
  
  component.postRoomInfo(room);
})


 //html
 it('click submit button should call onSubmit()', () => {
  spyOn(component, 'onSubmit');
  const button = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
  button.click();
  expect(component.onSubmit).toHaveBeenCalled();
})

//html
it('click submit button should call OnSubmit()', () => {
  spyOn(component, 'onSubmit');
  const button = fixture.debugElement.query(By.css('button[type=submit]')).nativeElement;
  button.click();
  expect(component.onSubmit).toHaveBeenCalled();
})
 





  

 });

