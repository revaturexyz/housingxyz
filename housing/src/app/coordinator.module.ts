import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { NavComponent } from './nav/nav.component';


@NgModule({
  declarations: [
    NavComponent,
    RoomDetailsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [],
})
export class CoordinatorModule { }