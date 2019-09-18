import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { RoomUpdateFormComponent } from './room-update-form.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestServiceData } from '../services/static-test-data';
import { Room } from 'src/interfaces/room';
import { MsalService, MsalModule, MsalGuard } from '@azure/msal-angular';
import { MSAL_CONFIG } from '@azure/msal-angular/dist/msal.service';
import { User } from 'msal';
import { RouterTestingModule } from '@angular/router/testing';

const room: Room = TestServiceData.room;

describe('RoomUpdateFormComponent', () => {
  let component: RoomUpdateFormComponent;
  let fixture: ComponentFixture<RoomUpdateFormComponent>;
  let msalService: MsalService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule, MatDialogModule, HttpClientTestingModule, MsalModule, RouterTestingModule],
      declarations: [RoomUpdateFormComponent],
      providers: [{ provide: MsalGuard, useValue: {} }, MsalService, { provide: MSAL_CONFIG, useValue: {} }]

    })
      .compileComponents();

    const testBed = getTestBed();
    msalService = testBed.get(MsalService);
    spyOn(msalService, 'getUser').and.returnValue(new User('1', 'chris', 'master', 'test', new Object()));

  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomUpdateFormComponent);
    component = fixture.componentInstance;
    component.room = room;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
