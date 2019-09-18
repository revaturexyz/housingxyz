import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RoomUpdateFormComponent } from './room-update-form.component';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestServiceData } from '../services/static-test-data';
import { Room } from 'src/interfaces/room';

const room: Room = TestServiceData.room;

describe('RoomUpdateFormComponent', () => {
  let component: RoomUpdateFormComponent;
  let fixture: ComponentFixture<RoomUpdateFormComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ FormsModule, MatDialogModule, HttpClientTestingModule ],
      declarations: [ RoomUpdateFormComponent ],
      
    })
    .compileComponents();
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
