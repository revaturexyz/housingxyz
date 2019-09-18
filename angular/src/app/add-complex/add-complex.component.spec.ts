import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { AddComplexComponent } from './add-complex.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestServiceData } from '../services/static-test-data';
import { Address } from '../../interfaces/address';
import { MsalGuard, MsalService, MsalModule } from '@azure/msal-angular';
import { MSAL_CONFIG } from '@azure/msal-angular/dist/msal.service';
import { User } from 'msal';


const complexAdd: Address = TestServiceData.dummyAddress;
const provider = TestServiceData.testProvider2;

describe('AddComplexComponent', () => {
  let component: AddComplexComponent;
  let fixture: ComponentFixture<AddComplexComponent>;
  let httpMock: HttpTestingController;
  let msalService: MsalService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddComplexComponent],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule, MsalModule],
      providers: [{ provide: MsalGuard, useValue: {} }, MsalService, { provide: MSAL_CONFIG, useValue: {} }]
    })
      .compileComponents();
    const testBed = getTestBed();
    httpMock = testBed.get(HttpTestingController);
    msalService = testBed.get(MsalService);
    spyOn(msalService, 'getUser').and.returnValue(new User('1', 'chris', 'master', 'test', new Object()));

  }));

  beforeEach(() => {
    localStorage.setItem('currentProvider', JSON.stringify(provider));
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
