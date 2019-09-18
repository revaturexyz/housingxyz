import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmenityDialogueComponent } from './amenity-dialogue.component';
import { Amenity } from 'src/interfaces/amenity';
import { TestServiceData } from '../services/static-test-data';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule, MatRippleModule } from '@angular/material';

const amenity1: Amenity = TestServiceData.dummyAmenity1;
const amenity2: Amenity = TestServiceData.dummyAmenity2;

describe('AmenityDialogueComponent', () => {
  let component: AmenityDialogueComponent;
  let fixture: ComponentFixture<AmenityDialogueComponent>;
  let data: Amenity[] = [amenity1, amenity2];
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AmenityDialogueComponent],
      imports: [MatDialogModule, MatRippleModule],
      providers: [{ provide: MatDialogRef, useValue: {} }, { provide: MAT_DIALOG_DATA, useValue: data }]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmenityDialogueComponent);
    component = fixture.componentInstance;
    component.roomAmenities = data;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
