import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from '../app.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { Router } from '@angular/router';
import { doesNotThrow } from 'assert';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    // spyOn(component, 'getLocationInfo');
    // spyOn(component, 'getRoomInfo');
    // component.locationList = mockLocationList[0];
    // spyOn(component, 'ngOnInit');
    fixture.detectChanges();
  });
});
