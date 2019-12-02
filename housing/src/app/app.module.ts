import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StickyNavModule } from 'ng2-sticky-nav';
import {
  MatChipsModule, MatProgressSpinnerModule, MatSortModule, MatTableModule, MatDialogModule, MatPaginatorModule, MatFormFieldModule,
  MatSelectModule, MatSidenavModule, MatIconModule, MatButtonModule, MatDividerModule, MatListModule,
  MatExpansionModule, MatInputModule
} from '@angular/material';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CdkTableModule } from '@angular/cdk/table';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { AmenityDialogueComponent } from './amenity-dialogue/amenity-dialogue.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { AuthGuard } from './guards/auth.guard';
import { InterceptorService } from './services/interceptor.service';
import { CoordinatorModule } from './coordinator.module';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';
import { EditProviderComponent } from './edit-provider/edit-provider.component';
import { ProviderStatusComponent } from './provider-status/provider-status.component';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';
import { AddRoomComponent } from './manage-complex/add-room/add-room.component';
import { EditComplexComponent } from './manage-complex/edit-complex/edit-complex.component';
import { ComplexDetailsComponent } from './manage-complex/complex-details/complex-details.component';
import { EditRoomComponent } from './manage-complex/edit-room/edit-room.component';
import { ShowRoomComponent } from './manage-complex/show-room/show-room.component';
import { AddTenantComponent } from '../app/add-tenant/add-tenant.component';
import { AddComplexComponent } from './manage-complex/add-complex/add-complex.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    AmenityDialogueComponent,
    CoordinatorNotificationsComponent,
    NotificationDetailsComponent,
    EditProviderComponent,
    ProviderStatusComponent,
    ManageComplexComponent,
    EditComplexComponent,
    ComplexDetailsComponent,
    EditRoomComponent,
    ShowRoomComponent,
    AddComplexComponent,
    AddRoomComponent,
    AddTenantComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    CdkTableModule,
    MatDialogModule,
    StickyNavModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatRippleModule,
    CoordinatorModule,
    MatTableModule,
    MatChipsModule,
    MatPaginatorModule,
    CdkTableModule,
    MatCardModule,
    MatDialogModule,
    StickyNavModule,
    BrowserAnimationsModule,
    MatRippleModule,
    MatSidenavModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatListModule,
    MatExpansionModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatInputModule,
    MatCheckboxModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ],
  entryComponents: [AmenityDialogueComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
