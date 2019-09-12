import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRoomComponent } from './update-room.component';
import { RoomUpdateFormComponent } from '../room-update-form/room-update-form.component';
import { RoomDetailsComponent } from '../room-details/room-details.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UpdateRoomComponent', () => {
  let component: UpdateRoomComponent;
  let fixture: ComponentFixture<UpdateRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateRoomComponent, RoomDetailsComponent, RoomUpdateFormComponent ],
      imports: [ HttpClientTestingModule ]
    })
    .overrideComponent(RoomUpdateFormComponent, {
        set: { template: '<div></div>'}}
    )
    .overrideComponent(RoomDetailsComponent, {
        set: { template: '<div></div>'}}
    );
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
