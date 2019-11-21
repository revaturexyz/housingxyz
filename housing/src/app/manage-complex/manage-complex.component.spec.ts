import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageComplexComponent } from './manage-complex.component';
import {
  MatFormFieldModule, MatSelectModule,
  MatCardModule,
  MatPaginatorModule, MatTableModule
} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddRoomComponent } from './add-room/add-room.component';
import { ComplexDetailsComponent } from './complex-details/complex-details.component';
import { EditRoomComponent } from './edit-room/edit-room.component';
import { EditComplexComponent } from './edit-complex/edit-complex.component';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('ManageComplexComponent', () => {
  let component: ManageComplexComponent;
  let fixture: ComponentFixture<ManageComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatFormFieldModule, FormsModule,
        ReactiveFormsModule, MatSelectModule,
        MatCardModule,
        MatTableModule, MatPaginatorModule, NoopAnimationsModule
      ],
      declarations: [
        ManageComplexComponent, AddRoomComponent,
        ComplexDetailsComponent, EditRoomComponent,
        EditComplexComponent, AddComplexComponent ]
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
