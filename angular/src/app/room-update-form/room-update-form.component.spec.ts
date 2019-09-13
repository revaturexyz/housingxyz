import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomUpdateFormComponent } from './room-update-form.component';

import { FormsModule } from '@angular/forms';

describe('RoomUpdateFormComponent', () => {
  let component: RoomUpdateFormComponent;
  let fixture: ComponentFixture<RoomUpdateFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule ],
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
