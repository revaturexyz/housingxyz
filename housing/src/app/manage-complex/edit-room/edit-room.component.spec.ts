import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material';
import { By } from '@angular/platform-browser';

import { EditRoomComponent } from './edit-room.component';
import { Complex } from 'src/interfaces/complex';

/*
describe('EditRoomComponent', () => {
  let component: EditRoomComponent;
  let fixture: ComponentFixture<EditRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, ReactiveFormsModule,
        MatCheckboxModule, MatDatepickerModule,
        MatMomentDateModule, MatFormFieldModule,
        MatSelectModule, NoopAnimationsModule,
        MatInputModule
      ],
      declarations: [ EditRoomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditRoomComponent);
    component = fixture.componentInstance;
    component.complexControl = TestServiceData.dummyComplex;
    component.targetRoom = TestServiceData.room;
    fixture.detectChanges();

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have enabled submit button', () => {
    const buttonSelector = fixture.debugElement.query(By.css('#postEditRoom')).nativeElement;
    fixture.detectChanges();
    expect(buttonSelector.disabled).toBeFalsy();
  });
}); */
