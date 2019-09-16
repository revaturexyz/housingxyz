import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
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

  // receives data from parent component about itself as well as injected data for initialization
  constructor(private dialogRef: MatDialogRef<AmenityDialogueComponent>, @Inject(MAT_DIALOG_DATA) data) {
    this.amenities = data.amen;
    this.roomAmenities = data.roomAmen;
    this.editedAmenities = [];
  }

  ngOnInit() {
    if (this.roomAmenities !== null) {
      this.roomAmenities.forEach(x => this.editedAmenities.push(x));
    }
    console.log(this.roomAmenities);
    console.log(this.editedAmenities);
  }

  // this function is called when the user saves the changed amenities. It returns the list of
  // all the edited amenities, not the starting list.
  save() {
    this.dialogRef.close(this.editedAmenities);
  }

  // this function is called when the user wants to cancel changes to the selected amenities,
  // it simply returns the starting list of selected amenities.
  close() {
    this.dialogRef.close(this.roomAmenities);
  }

  // this function takes care of adding and removing from the selected amenities list.
  clickEvent(amen: Amenity) {
    if (this.editedAmenities == null) {
      return;
    }
    // if the amenity is already selected, then remove it from the selection list
    // else, add the selected amenity to the list
    if (this.editedAmenities.includes(amen)) {
      this.editedAmenities = this.editedAmenities.filter(x => x.amenityId !== amen.amenityId);
    } else {
      this.editedAmenities.push(amen);
    }
  }

  match(amen: Amenity): boolean {
    let boolCheck = false;
    this.editedAmenities.forEach(x => {
      if (x.amenityId === amen.amenityId) {
        boolCheck = true;
      }
    });
    return boolCheck;
  }

  // this function takes care of letting the component know which amenities are currently selected
  selected(amen: Amenity): boolean {
    if (this.amenities == null) {
      return false;
    }
    // if the amenity is currently selected, then change the display color slightly so the user is notified
    // else, display default color scheme
    if (this.match(amen)) {
      return true;
    } else {
      return false;
    }
  }
}
