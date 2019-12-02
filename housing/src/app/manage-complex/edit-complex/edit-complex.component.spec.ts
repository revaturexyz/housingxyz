import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatSelectModule, MatTableModule  } from '@angular/material';
import { EditComplexComponent } from './edit-complex.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';

/*
describe('EditComplexComponent', () => {
  let component: EditComplexComponent;
  let fixture: ComponentFixture<EditComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        FormsModule, ReactiveFormsModule,
        MatFormFieldModule, MatSelectModule,
        MatTableModule, NoopAnimationsModule ],
      declarations: [ EditComplexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditComplexComponent);
    component = fixture.componentInstance;
    component.targetComplex = TestServiceData.dummyComplex;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have enabled submit button', () => {
    const buttonSelector = fixture.debugElement.query(By.css('#putEditComplex')).nativeElement;
    fixture.detectChanges();
    expect(buttonSelector.disabled).toBeFalsy();
  });
}); */
