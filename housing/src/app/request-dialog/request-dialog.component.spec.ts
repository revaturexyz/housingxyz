import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { RequestDialogComponent } from './request-dialog.component';
import { MatDialogModule, MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Amenity } from 'src/interfaces/amenity';
import { TestServiceData } from '../services/static-test-data';
import { Room } from 'src/interfaces/room';
import { MsalService, MsalModule, MsalGuard } from '@azure/msal-angular';
import { MSAL_CONFIG } from '@azure/msal-angular/dist/msal.service';
import { User } from 'msal';
import { RouterTestingModule } from '@angular/router/testing';

const room: Room = TestServiceData.room;

describe('RequestDialogComponent', () => {
  let component: RequestDialogComponent;
  let fixture: ComponentFixture<RequestDialogComponent>;
  const data: Room = room;
  let msalService: MsalService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RequestDialogComponent],
      imports: [MatDialogModule, FormsModule, HttpClientTestingModule, MsalModule, RouterTestingModule],
      providers: [
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { }
          }
        },
        { provide: MAT_DIALOG_DATA, useValue: data },
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
    fixture = TestBed.createComponent(RequestDialogComponent);
    component = fixture.componentInstance;
    component.requestRoom = data;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
