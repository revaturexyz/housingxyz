import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MsalModule, MsalGuard, MsalService } from '@azure/msal-angular';
import { MSAL_CONFIG } from '@azure/msal-angular/dist/msal.service';
import { User } from 'msal';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let msalService: MsalService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, MsalModule, RouterTestingModule],
      declarations: [LoginComponent],
      providers: [{ provide: MsalGuard, useValue: {} }, MsalService, { provide: MSAL_CONFIG, useValue: {} }]
    })
      .compileComponents();
    const testBed = getTestBed();
    msalService = testBed.get(MsalService);
    spyOn(msalService, 'getUser').and.returnValue(new User('1', 'chris', 'master', 'test', new Object()));
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
