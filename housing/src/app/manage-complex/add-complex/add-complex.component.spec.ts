import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatFormFieldModule, MatSelectModule, MatTableModule  } from '@angular/material';

import { AddComplexComponent } from './add-complex.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('AddComplexComponent', () => {
  let component: AddComplexComponent;
  let fixture: ComponentFixture<AddComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatTableModule, NoopAnimationsModule ],
      declarations: [ AddComplexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddComplexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
