import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageComplexComponent } from './manage-complex.component';
import {
  MatFormFieldModule, MatSelectModule,
  MatCardModule, MatIconModule,
  MatPaginatorModule, MatTableModule
} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddRoomComponent } from './add-room/add-room.component';
import { ComplexDetailsComponent } from './complex-details/complex-details.component';
import { EditRoomComponent } from './edit-room/edit-room.component';
import { EditComplexComponent } from './edit-complex/edit-complex.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('ManageComplexComponent', () => {
  let component: ManageComplexComponent;
  let fixture: ComponentFixture<ManageComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatFormFieldModule, FormsModule,
        ReactiveFormsModule, MatSelectModule,
        MatCardModule, MatIconModule,
        MatTableModule, MatPaginatorModule, NoopAnimationsModule
      ],
      declarations: [
        ManageComplexComponent, AddRoomComponent,
        ComplexDetailsComponent, EditRoomComponent,
        EditComplexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageComplexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
