import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';

import { AddTenantComponent } from './add-tenant.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestTenantData, TenantServiceStub } from '../services/test-tenant-static';
import { RouterTestingModule } from '@angular/router/testing';
import { Batch } from 'src/interfaces/batch';
import { PostTenantAddress } from 'src/interfaces/postTenAddress';
import { PostCar } from 'src/interfaces/postCar';
import { TenantService } from '../services/tenant.service';
import { AuthService } from '../services/auth.service';
import { EMPTY, of, Observable } from 'rxjs';
import { PostTenant } from 'src/interfaces/postTenant';
import { HttpEvent } from '@angular/common/http';

const address: PostTenantAddress = {
  street: '123 GregMad St',
  city: 'Colton',
  state: 'Victory',
  country: 'BrokenWill',
  zipCode: '12345'
};

describe('AddTenantComponent', () => {
  let component: AddTenantComponent;
  let fixture: ComponentFixture<AddTenantComponent>;
  let coordService: TenantService;
  const batchStub = null;
  const authSpy = jasmine.createSpyObj('AuthService', ['login']);
  const coordSpy = jasmine.createSpyObj('CoordniatorService', ['PostTenant', 'GetBatchByTrainingCenterId']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddTenantComponent],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule],
      providers: [
        AddTenantComponent,
        { provide: AuthService, useValue: authSpy },
        { provide: TenantService, useValue: coordSpy, useClass: TenantServiceStub }
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    coordService = TestBed.get(TenantService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // addCarForm()
  it('should add form on addForm()', () => {
    component.addCarForm();
    expect(component.showCarForm).toBeTruthy();
    expect(component.showAddressForm).toBeFalsy();
  });

  // closeCarForm()
  it('should remove form on back()', () => {
    component.closeCarForm();
    expect(component.showCarForm).toBeFalsy();
  });

  // addAddressForm()
  it('should add form on addAddress()', () => {
    component.addAddressForm();
    expect(component.showAddressForm).toBeTruthy();
    expect(component.showCarForm).toBeFalsy();
  });

  // closeAddressForm()
  it('should remove form on return()', () => {
    component.closeAddressForm();
    expect(component.showAddressForm).toBeFalsy();
  });

  // ngOnInit()
  it('should initialize correctly', () => {
    expect(component.batchList).toBeTruthy();
  });

  // batchChoose(batch: Batch)
  it('should choose Batch on batchChoose()', () => {
    const batch: Batch = TestTenantData.dummyBatch;

    component.batchChoose(batch);

    expect(component.activeBatch.batchCurriculum).toEqual(batch.batchCurriculum);
    expect(component.activeBatch).toBe(batch);
  });

  // postTenantOnSubmit()
  it('should post tenants on submit', async () => {
    component.tenant.apiAddress = TestTenantData.dummyAddress;
    component.tenant.apiBatch = TestTenantData.dummyPostBatch;
    expect(component.postTenantOnSubmit()).toBeTruthy();
  });
});
