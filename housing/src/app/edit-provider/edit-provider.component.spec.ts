import { async, inject, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';

import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { EditProviderComponent } from './edit-provider.component';

describe('EditProviderComponent', () => {
  let component: EditProviderComponent;
  let fixture: ComponentFixture<EditProviderComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, RouterTestingModule, HttpClientTestingModule ],
      declarations: [ EditProviderComponent ],
      providers: [ ]
    })
    .compileComponents();
    const testBed = getTestBed();
    httpMock = testBed.get(HttpTestingController);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
