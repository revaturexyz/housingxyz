import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { EditRoomComponent } from './edit-room.component';
import { Complex } from 'src/interfaces/complex';

import { TestServiceData } from 'src/app/services/static-test-data';

describe('EditRoomComponent', () => {
  let component: EditRoomComponent;
  let fixture: ComponentFixture<EditRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, ReactiveFormsModule,
        MatCheckboxModule, MatDatepickerModule,
        MatMomentDateModule, MatFormFieldModule,
        MatSelectModule
      ],
      declarations: [ EditRoomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

  });

  it('should create', () => {
    component.complexControl = TestServiceData.dummyComplex;
    component.targetRoom = TestServiceData.room;
    expect(component).toBeTruthy();
  });
});
