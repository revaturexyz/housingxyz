import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoomComponent } from './add-room.component';
<<<<<<< HEAD
=======
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819

describe('AddRoomComponent', () => {
  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
<<<<<<< HEAD
=======
      imports: [ FormsModule, HttpClientTestingModule ],
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
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
