import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmenityDialogueComponent } from './amenity-dialogue.component';
import { Amenity } from 'src/interfaces/amenity';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule, MatRippleModule } from '@angular/material';

/*
describe('AmenityDialogueComponent', () => {
  let component: AmenityDialogueComponent;
  let fixture: ComponentFixture<AmenityDialogueComponent>;

  const amenity1: Amenity = { amenityId: 4, amenity: 'Washer No Dryer Unit', isSelected: true };
  const amenity2: Amenity = { amenityId: 4, amenity: 'Washer And Dryer Unit', isSelected: true };
  const data: Amenity[] = [amenity1, amenity2];
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AmenityDialogueComponent],
      imports: [MatDialogModule, MatRippleModule],
      providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: data }
      ]
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

  it('should return null on clickEvent() when editedAmenities is null', () => {
    fixture.detectChanges();
    component.editedAmenities = null;
    const result = component.clickEvent(amenity1);
    expect(result).toBeUndefined();
  });

  it('should add amenity on clickEvent() to editedAmenities if amenity is not in editedAmenities', () => {
    fixture.detectChanges();
    const amenity3 = { amenityId: 4, amenity: 'Washer No Dryer Unit', isSelected: true };
    component.clickEvent(amenity3);
    expect(component.editedAmenities).toContain(amenity3);
  });

  it('should remove amenity on clickEvent() to editedAmenities', () => {
    fixture.detectChanges();
    component.editedAmenities.push(amenity1);
    component.clickEvent(amenity1);
    let removed = false;
    if (!component.editedAmenities.includes(amenity1)) {
      removed = true;
    }
    expect(removed).toBeTruthy();
  });
}); */
