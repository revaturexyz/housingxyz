import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomUpdateFormComponent } from './room-update-form.component';

describe('RoomUpdateFormComponent', () => {
  let component: RoomUpdateFormComponent;
  let fixture: ComponentFixture<RoomUpdateFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoomUpdateFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomUpdateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
