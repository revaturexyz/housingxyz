import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { NavComponent } from './nav/nav.component';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { AddTenantComponent } from './add-tenant/add-tenant.component';
import { ShowTenantComponent } from './show-tenant/show-tenant.component';

@NgModule({
  declarations: [
    NavComponent,
    RoomDetailsComponent,
    AddTenantComponent,
    ShowTenantComponent
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