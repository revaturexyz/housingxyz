import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRoomComponent } from './update-room.component';
import { RoomDetailsComponent } from '../room-details/room-details.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Complex } from 'src/interfaces/complex';
import { TestServiceData } from '../services/static-test-data';

describe('UpdateRoomComponent', () => {
  let component: UpdateRoomComponent;
  let fixture: ComponentFixture<UpdateRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateRoomComponent, RoomDetailsComponent ],
      imports: [ HttpClientTestingModule ]
    })
    .overrideComponent(RoomDetailsComponent, {
        set: { template: '<div></div>'}}
    )
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize correctly', () => {
    expect(component.complexList).toBeTruthy();
    expect(component.roomList).toBeTruthy();
  });


  it('should choose Complex', () => {

    // given this complex
    const complex: Complex = TestServiceData.dummyComplex2;

  // execute test case
    component.complexChoose(complex);

  // assertion
    expect(component.showString).toEqual(complex.complexName);
    expect(component.activeComplex).toBe(complex);
    expect(component.complexRooms).toBeTruthy(); // assertion that the available rooms are filtered successfully

  });
});
