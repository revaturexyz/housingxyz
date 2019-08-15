import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRoomComponent } from './delete-room.component';

import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService } from '../api.service';
import { ApiServiceMock } from '../testing/mock/mock-api-service';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { LocationObject, RoomObject, } from '../testing/dummyData';
import { By } from '@angular/platform-browser';
import { formatDate } from '@angular/common';


describe('DeleteRoomComponent', () => {
  let component: DeleteRoomComponent;
  let fixture: ComponentFixture<DeleteRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteRoomComponent],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [
        { provide: ApiService, useClass: ApiServiceMock },
        {
          provide: ActivatedRoute, useValue: {
            paramMap: of({ get: (key) => 1 })
          }
        }
      ]

    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //#region html
  it('click delete button should call deleteroom with roomID to', () => {
    spyOn(component, 'deleteRoom');
    const button = fixture.debugElement.queryAll(By.css('button.btn-buttons'));
    button[1].nativeElement.click();
    expect(component.deleteRoom).toHaveBeenCalledWith(1);
  })

  it('should show correct data in html file',()=>{
    const span = fixture.debugElement.queryAll(By.css('span'));
    //include all span
    expect(span.length).toBe(8);
    //date time format to short date
    //but somehow, the actual format it show is mediumDate
    const startDate =formatDate(RoomObject.startDate,'mediumDate','en-US');
    const endDate = formatDate(RoomObject.endDate,'mediumDate','en-US');
    //manually check each data
    expect(span[0].nativeElement.textContent).toBe(`Address: ${LocationObject.address}`);
    expect(span[1].nativeElement.textContent).toBe(`${LocationObject.city}, ${LocationObject.state} ${LocationObject.zip}`);
    expect(span[2].nativeElement.textContent).toBe(`Room #: ${RoomObject.roomNumber}`);
    expect(span[3].nativeElement.textContent).toBe(`Gender: ${RoomObject.gender}`);
    expect(span[4].nativeElement.textContent).toBe(`Occupancy: ${RoomObject.currentOccupancy} of ${RoomObject.maxOccupancy}`);
    expect(span[5].nativeElement.textContent).toBe(`Start Date: ${startDate} `);
    expect(span[6].nativeElement.textContent).toBe(`End Date: ${endDate}`);
    expect(span[7].nativeElement.textContent).toBe(`${RoomObject.description}`);
  })
  //#endregion

  //#reigion component.ts

  //onNgInit
  it('should call getRoomInfo() and assign roomID', () => {
    //reset the roomID first,since it already assign in beforeEach()
    spyOn(component,'getRoomInfo');
    component.roomID = 0;
    component.ngOnInit();
    expect(component.roomID).toBe(1);
    expect(component.getRoomInfo).toHaveBeenCalled();
  })

  //getRoomInfo()
  it('should assign value to romm and call getLocationInfo()',()=>{
    //reset value
    component.room=null;
    spyOn(component,'getLocationInfo');
    component.getRoomInfo();
    
    expect(component.room).toEqual(RoomObject);
    expect(component.getLocationInfo).toHaveBeenCalledWith(RoomObject.locationID);
  })

  
  it('should should not assign value to room',()=>{
    //reset room
    component.room=null;
    spyOn(component,'getLocationInfo');
    //force return error
    const xService = fixture.debugElement.injector.get(ApiService);
    xService['apiError']=true;
    component.getRoomInfo();
    expect(component.room).toBeFalsy();
    //this expectation will throw error failed 
    expect(component.getLocationInfo).toHaveBeenCalledTimes(0);
  })

  //getLocatoinInfo
  it('should assign value to location',()=>{
    //reset value
    component.location=null;
    component.getLocationInfo(1);

    expect(component.location).toEqual(LocationObject);

  })

  it('should not assign value to location',()=>{
    component.location=null;
    //force return error
    const xService = fixture.debugElement.injector.get(ApiService);
    xService['apiError']=true;

    expect(component.location).toBeFalsy();
  })

  //deleteRoom()
  it('should call deleteRoom in service and redirect to home page',()=>{
    const xService = fixture.debugElement.injector.get(ApiService);
    spyOn(xService,'deleteRoom').and.returnValue(of());
    const routerstub = TestBed.get(Router);
    spyOn(routerstub, 'navigate');

    component.deleteRoom(1);

    expect(xService.deleteRoom).toHaveBeenCalledWith(1);
    expect(routerstub.navigate).toHaveBeenCalledWith(['']);
  })
  //#endregion
});
