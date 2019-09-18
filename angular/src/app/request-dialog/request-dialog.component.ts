import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Room } from 'src/interfaces/room';
import { Notify } from 'src/interfaces/notification';
import { ProviderService } from '../services/provider.service';

@Component({
  selector: 'dev-request-dialog',
  templateUrl: './request-dialog.component.html',
  styleUrls: ['./request-dialog.component.scss']
})
export class RequestDialogComponent implements OnInit {

  requestRoom: Room;
  req: Notify;

  // receives data from parent component about itself as well as injected data for initialization
  constructor(
    private dialogRef: MatDialogRef<RequestDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data,
    private providerService: ProviderService) {
    this.requestRoom = data.reqRoom;
  }

  ngOnInit() {
    if (this.requestRoom == null) {
      this.close();
    }
    const note: Notify = {
      notificationID: 0,
      providerID: 1,
      roomID: this.requestRoom.roomId,
      title: '',
      reason: ''
    };
    this.req = note;
  }

  // this function is called when the user saves the request. It sends the request then
  // returns control to the parent
  save() {
    this.providerService.postRequestByProvider(this.req);
    this.dialogRef.close();
  }

  // this function is called when the user cancels a request. It returns control to the parent
  close() {
    this.dialogRef.close();
  }

}
