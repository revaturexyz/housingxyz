import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRoomComponent } from './update-room.component';
import { RoomDetailsComponent } from '../room-details/room-details.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

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
});
