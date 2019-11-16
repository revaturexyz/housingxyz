import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { ProviderSelectComponent } from './provider-select/provider-select.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StickyNavModule } from 'ng2-sticky-nav';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';
import {
  MatChipsModule, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule, MatTableModule, MatDialogModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { RoomUpdateFormComponent } from './room-update-form/room-update-form.component';
import { AmenityDialogueComponent } from './amenity-dialogue/amenity-dialogue.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRippleModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { RequestDialogComponent } from './request-dialog/request-dialog.component';
import { AuthGuard } from './guards/auth.guard';
import { InterceptorService } from './services/interceptor.service';
import { AddProviderComponent } from './add-provider/add-provider.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CoordinatorModule } from './coordinator.module';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    ProviderSelectComponent,
    AddRoomComponent,
    UpdateRoomComponent,
    RoomDetailsComponent,
    RoomUpdateFormComponent,
    AmenityDialogueComponent,
    AddComplexComponent,
    RequestDialogComponent,
    AddProviderComponent,
    CoordinatorNotificationsComponent,
    NotificationDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatCardModule,
    NgbModule,
    MatTableModule,
    MatChipsModule,
    CdkTableModule,
    MatCardModule,
    MatDialogModule,
    StickyNavModule,
    BrowserAnimationsModule,
    MatRippleModule,
    NgxPaginationModule,
    CoordinatorModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ],
  entryComponents: [AmenityDialogueComponent, RequestDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
