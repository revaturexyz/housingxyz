import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { NavComponent } from './nav/nav.component';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';


@NgModule({
  declarations: [
    NavComponent,
    RoomDetailsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    }
  ],
})
export class CoordinatorModule { }