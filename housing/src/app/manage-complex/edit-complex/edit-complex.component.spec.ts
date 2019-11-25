import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatFormFieldModule, MatSelectModule, MatTableModule  } from '@angular/material';

import { EditComplexComponent } from './edit-complex.component';

describe('EditComplexComponent', () => {
  let component: EditComplexComponent;
  let fixture: ComponentFixture<EditComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, ReactiveFormsModule, MatFormFieldModule, MatSelectModule, MatTableModule ],
      declarations: [ EditComplexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditComplexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});