import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RequestDialogComponent } from './request-dialog.component';
import { MatDialogModule, MatDialogRef, MatDialog, MAT_DIALOG_DATA  } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Amenity } from 'src/interfaces/amenity';
import { TestServiceData } from '../services/static-test-data';
import { Room } from 'src/interfaces/room';

const room: Room = TestServiceData.room;

describe('RequestDialogComponent', () => {
  let component: RequestDialogComponent;
  let fixture: ComponentFixture<RequestDialogComponent>;
  const data: Room = room;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequestDialogComponent ],
      imports: [ MatDialogModule, FormsModule, HttpClientTestingModule  ],
      providers: [{
        provide: MatDialogRef,
        useValue: {
          close: (dialogResult: any) => { }
      }}, {provide: MAT_DIALOG_DATA,
        useValue: data
      }]
    })
    .compileComponents();
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
