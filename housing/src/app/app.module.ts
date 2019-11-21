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
  MatChipsModule, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule, MatTableModule, MatDialogModule, MatFormFieldModule, MatSelectModule,
  MatOptionModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { RoomUpdateFormComponent } from './room-update-form/room-update-form.component';
import { AmenityDialogueComponent } from './amenity-dialogue/amenity-dialogue.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatRippleModule } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { RequestDialogComponent } from './request-dialog/request-dialog.component';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { environment } from 'src/environments/environment';
import { CoordinatorModule } from './coordinator.module';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';

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
    ManageComplexComponent
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
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatRippleModule,
    MsalModule.forRoot({
      clientID: environment.clientID,
      authority: environment.authority,
      validateAuthority: environment.validateAuthority,
      // redirectUri: 'https://housingpre.revature.xyz/',
      // redirectUri: 'http://localhost:4200/',
      cacheLocation: 'localStorage',
      postLogoutRedirectUri: 'http://localhost:4200/login/',
      navigateToLoginRequestUrl: false
    }),
    CoordinatorModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    }
  ],
  entryComponents: [AmenityDialogueComponent, RequestDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
