import { async, ComponentFixture, TestBed, inject, getTestBed } from '@angular/core/testing';
import { HomeComponent } from './home.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MsalService, MsalModule, MsalGuard } from '@azure/msal-angular';
import { MSAL_CONFIG } from '@azure/msal-angular/dist/msal.service';
import { User } from 'msal';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let msalService: MsalService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule,
        MsalModule
      ],
      providers: [
        { provide: MsalGuard, useValue: {} },
        MsalService,
        { provide: MSAL_CONFIG, useValue: {} }
      ]
    })
      .compileComponents();
    const testBed = getTestBed();
    msalService = testBed.get(MsalService);
    spyOn(msalService, 'getUser').and.returnValue(new User('1', 'chris', 'master', 'test', new Object()));
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

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
