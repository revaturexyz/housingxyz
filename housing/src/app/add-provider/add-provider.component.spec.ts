import { async, inject, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';

import { AddProviderComponent } from './add-provider.component';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';


describe('AddProviderComponent', () => {
  let component: AddProviderComponent;
  let fixture: ComponentFixture<AddProviderComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, RouterTestingModule, HttpClientTestingModule ],
      declarations: [ AddProviderComponent ],
      providers: [ ]
    })
    .compileComponents();
    const testBed = getTestBed();
    httpMock = testBed.get(HttpTestingController);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
