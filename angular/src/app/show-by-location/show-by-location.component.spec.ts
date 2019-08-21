import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowByLocationComponent } from './show-by-location.component';
import { ApiService } from '../api.service';
import { ApiServiceMock } from '../testing/mock/mock-api-service';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { mockLocationList, mockRoomList } from '../testing/dummyData';
import { Router } from '@angular/router';

describe('ShowByLocationComponent', () => {
  let component: ShowByLocationComponent;
  let fixture: ComponentFixture<ShowByLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowByLocationComponent ],
      imports: [
        FormsModule,
        RouterTestingModule
      ],
      providers: [
        { provide: ApiService, useClass: ApiServiceMock }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowByLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getLocationInfo in ngOnInit',()=>{
    spyOn(component,'getLocationInfo');
    component.ngOnInit();
    expect(component.getLocationInfo).toHaveBeenCalled();
  })

  it('should assign location id and call getRoomInfoByLocation in selectoption',()=>{
    spyOn(component,'getRoomInfoByLocation');
    component.selectOption(1);
    expect(component.locationID).toBe(1);
    expect(component.getRoomInfoByLocation).toHaveBeenCalled();

  })

  it('should assign a location list in getLocationInfo',()=>{
    component.getLocationInfo();
    expect(component.locationList).toEqual(mockLocationList);
  })

  it('should assign room list in getRoomInfoByLocation',()=>{
    component.getRoomInfoByLocation();
    expect(component.roomList).toEqual(mockRoomList);
  })
  it('should navigate to update-room with id',()=>{
    const routerstub = TestBed.get(Router);
    spyOn(routerstub, 'navigate');
    component.updateRoom(1);
    expect(routerstub.navigate).toHaveBeenCalledWith(['update-room', 1]);
  })

  it('should navigate to add-room with id',()=>{
    const routerstub = TestBed.get(Router);
    spyOn(routerstub, 'navigate');
    component.locationID="1";
    component.showLocation();
    expect(routerstub.navigate).toHaveBeenCalledWith(['/add-room', 1]);
  })
});
