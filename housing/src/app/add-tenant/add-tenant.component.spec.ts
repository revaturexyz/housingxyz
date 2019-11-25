import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';

import { AddTenantComponent } from './add-tenant.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestTenantData } from '../services/test-tenant-static';
import { RouterTestingModule } from '@angular/router/testing';
import { Batch } from 'src/interfaces/batch';
import { TenantAddress } from 'src/interfaces/tenantAddress';
import { CoordinatorService } from '../services/coordinator.service';
import { AuthService } from '../services/auth.service';

const address: TenantAddress = {
  addressId: 'asdf',
  street: '123 GregMad St',
  city: 'Colton',
  state: 'Victory',
  country: 'BrokenWill',
  zipCode: '12345'
}

describe('AddTenantComponent', () => {
  let component: AddTenantComponent;
  let fixture: ComponentFixture<AddTenantComponent>;
  const authSpy = jasmine.createSpyObj('AuthService', ['login']);
  const tenantSpy = jasmine.createSpyObj('CoordniatorService', ['PostTenant']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddTenantComponent],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule],
      providers: [
        { provide: AuthService, useValue: authSpy },
        { provide: CoordinatorService, useValue: tenantSpy }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // ngOnInit()
  it('should initialize correctly', () => {
    expect(component.batchList).toBeTruthy();
  });

  // addForm()
  it('should add form on addForm()', () => {
    component.addForm();
    expect(component.show).toBeTruthy();
  });

  // back()
  it('should remove form on back()', () => {
    component.back();
    expect(component.show).toBeFalsy();
  });

  // addAddress()
  it('should add form on addAddress()', () => {
    component.addAddress();
    expect(component.show).toBeTruthy();
  });

  // return()
  it('should remove form on return()', () => {
    component.return();
    expect(component.show).toBeFalsy();
  });

  // batchChoose(batch: Batch)
  it('should choose Batch on batchChoose()', () => {
    const batch: Batch = TestTenantData.dummyBatch;

    component.batchChoose(batch);

    expect(component.activeBatch.batchLanguage).toEqual(batch.batchLanguage);
    expect(component.activeBatch).toBe(batch);
  });

  // postTenantOnSubmit()
  it('should post tenants by id on submit', async () => {
    component.tenant.tenantAddress = address;
    component.postTenantOnSubmit();
    expect(component.show).toEqual(false);
  });
});
