import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { AddComplexComponent } from './add-complex.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestServiceData } from '../services/static-test-data';
import { Address } from '../../interfaces/address';

const complexAdd: Address = TestServiceData.dummyAddress;

describe('AddComplexComponent', () => {
  let component: AddComplexComponent;
  let fixture: ComponentFixture<AddComplexComponent>;
  let httpMock: HttpTestingController;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddComplexComponent],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule]
    })
      .compileComponents();
    const testBed = getTestBed();
    httpMock = testBed.get(HttpTestingController);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddComplexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should cancel add living complex', () => {
    component.cancelAddLivingComplex();
    expect(component).toBeTruthy();
  });

  it('should initialize page with provider', () => {
    component.ngOnInit();
    expect(component).toBeTruthy();
  });

  it('should post living complex', async () => {
    component.isValidAddress = false;
    component.postLivingComplex();
    expect(component.isValidAddress).toBeFalsy();

    component.isValidAddress = true;
    component.isValidDistanceToTrainingCenter = true;
    component.postLivingComplex();
    expect(!component.isValidDistanceToTrainingCenter).toBeFalsy();

    component.isValidAddress = true;
    component.isValidDistanceToTrainingCenter = false;
    component.postLivingComplex();
    expect(component.isValidDistanceToTrainingCenter).toBeFalsy();
  });

});
