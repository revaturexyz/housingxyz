import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { Amenity } from 'src/interfaces/amenity';

@Component({
  selector: 'dev-amenity-dialogue',
  templateUrl: './amenity-dialogue.component.html',
  styleUrls: ['./amenity-dialogue.component.scss'],
})
export class AmenityDialogueComponent implements OnInit {

  amenities: Amenity[];
  roomAmenities: Amenity[];
  editedAmenities: Amenity[];

  constructor(private dialogRef: MatDialogRef<AmenityDialogueComponent>, @Inject(MAT_DIALOG_DATA) data) {
    this.amenities = data.amen;
    this.roomAmenities = data.roomAmen;
    this.editedAmenities = [];
  }

  ngOnInit() {
    this.roomAmenities.forEach(x => this.editedAmenities.push(x));
  }
  
  save() {
    this.dialogRef.close(this.editedAmenities);
  }

  close() {
    this.dialogRef.close(this.roomAmenities);
  }

  clickEvent(amen: Amenity) {
    if (this.editedAmenities == null) {
      return;
    }
    if (this.editedAmenities.includes(amen)) {
      this.editedAmenities = this.editedAmenities.filter(x => x !== amen);
    } else {
      this.editedAmenities.push(amen);
    }
  }

  selected(amen: Amenity): boolean {
    if (this.amenities == null) {
      return false;
    }
    if (this.editedAmenities.includes(amen)) {
      return true;
    } else {
      return false;
    }
  }
}
