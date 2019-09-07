import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoomComponent } from './add-room.component';
import { FormsModule } from '@angular/forms';

describe('AddRoomComponent', () => {
  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule ],
      declarations: [ AddRoomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
