import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';

import { AddTenantComponent } from './add-tenant.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestTenantData, CoordinatorServiceStub } from '../services/test-tenant-static';
import { RouterTestingModule } from '@angular/router/testing';
import { Batch } from 'src/interfaces/batch';
import { PostTenantAddress } from 'src/interfaces/postTenAddress';
import { PostCar } from 'src/interfaces/postCar'
import { CoordinatorService } from '../services/coordinator.service';
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
}

class MockCoordinatorService extends CoordinatorService {
  PostTenant(postTenant: PostTenant): Observable<HttpEvent<PostTenant>> {
    return this.httpOptions.toBeTruthy(TestTenantData.dummyTenant);
  }
}

describe('AddTenantComponent', () => {
  let component: AddTenantComponent;
  let fixture: ComponentFixture<AddTenantComponent>;
  let coordService: CoordinatorService;
  let batchStub;
  const authSpy = jasmine.createSpyObj('AuthService', ['login']);
  const coordSpy = jasmine.createSpyObj('CoordniatorService', ['PostTenant', 'GetBatchByTrainingCenterId']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AddTenantComponent],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule],
      providers: [
        AddTenantComponent,
        { provide: AuthService, useValue: authSpy },
        { provide: CoordinatorService, useValue: coordSpy, useClass: CoordinatorServiceStub }
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    coordService = TestBed.get(CoordinatorService);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
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
    expect(component.address).toBeTruthy();
  });

  // return()
  it('should remove form on return()', () => {
    component.return();
    expect(component.address).toBeFalsy();
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
  it('should post tenants by id on submit', async () => {
    component.tenant.apiAddress = TestTenantData.dummyAddress;
    component.tenant.apiBatch = TestTenantData.dummyPostBatch;
    component.postTenantOnSubmit();
    expect(component.show).toEqual(false);
  });
});
