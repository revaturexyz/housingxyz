import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRoomComponent } from './add-room.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Address } from 'src/interfaces/address';
import { Complex } from 'src/interfaces/complex';

describe('AddRoomComponent', () => {
  let component: AddRoomComponent;
  let fixture: ComponentFixture<AddRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, HttpClientTestingModule ],
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

  // getRoomByIdOnSubmit()
  // it('should get rooms by id on submit', () => {
  //   component.getRoomByIdOnSubmit();
  //   expect
  // });


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

    const complex: Complex = {
      complexId: 2,
      streetAddress: '234 Complex St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '23456',
      complexName: 'Liv- Appartments',
      contactNumber: '123-123-1234'
  };

    component.complexChoose(complex);

    expect(component.activeComplex.complexName).toEqual(complex.complexName);
    expect(component.activeComplex).toBe(complex);

  });

  // addressChoose(address: Address)
  it('should choose Address on addressChoose()', () => {

    const address: Address = {
      addressId: 1,
      streetAddress: '123 Address St',
      city: 'Arlington',
      state: 'TX',
      zipCode: '12345'
  };

    component.addressChoose(address);

    expect(component.addressShowString).toEqual(address.streetAddress);
    expect(component.activeAddress).toBe(address);

  });
});
