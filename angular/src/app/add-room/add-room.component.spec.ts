import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoomComponent } from './add-room.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';
import { TestServiceData } from '../services/static-test-data';
import { RouterTestingModule  } from '@angular/router/testing';

const address: Address = {
  addressId: 1,
  streetAddress: '123 Address St',
  city: 'Arlington',
  state: 'TX',
  zipCode: '12345'
};

describe('AddRoomComponent', () => {
  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, HttpClientTestingModule, RouterTestingModule ],
      declarations: [ AddRoomComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // ngOnInit()
  it('should initialize correctly', () => {
    expect(component.types).toBeTruthy();
    expect(component.amenities).toBeTruthy();
    expect(component.complexList).toBeTruthy();
    expect(component.addressList).toBeTruthy();
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

  // complexChoose(complex: Complex)
  it('should choose Complex on complexChoose()', () => {

    const complex: Complex = TestServiceData.dummyComplex2;

    component.complexChoose(complex);

    expect(component.activeComplex.complexName).toEqual(complex.complexName);
    expect(component.activeComplex).toBe(complex);

  });

  // addressChoose(address: Address)
  it('should choose Address on addressChoose()', () => {
    component.addressChoose(address);

    expect(component.addressShowString).toEqual(address.streetAddress);
    expect(component.activeAddress).toBe(address);
  });

  // postRoomOnSubmit()
  it('should post rooms by id on submit', async () => {
    component.room.apiAddress = address;
    component.postRoomOnSubmit();
    expect(component.show).toEqual(false);
  });

});
