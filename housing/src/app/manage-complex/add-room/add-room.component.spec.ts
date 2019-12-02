import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AddRoomComponent } from './add-room.component';
import { By } from '@angular/platform-browser';
import { Complex } from 'src/interfaces/complex';

/* describe('AddRoomComponent', () => {
  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule, ReactiveFormsModule,
        MatDatepickerModule, MatMomentDateModule,
        MatFormFieldModule, MatSelectModule,
        MatInputModule, NoopAnimationsModule
      ],
      declarations: [ AddRoomComponent ]
    })
    .compileComponents();

  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRoomComponent);
    component = fixture.componentInstance;
    component.complexControl = TestServiceData.dummyComplex2;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have disabled submit button', () => {
    const buttonSelector = fixture.debugElement.query(By.css('#postAddRoom')).nativeElement;
    fixture.detectChanges();
    expect(buttonSelector.valid).toBeFalsy();
  });
}); */
