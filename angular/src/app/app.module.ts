import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProviderLocation } from 'src/models/location';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StickyNavModule } from 'ng2-sticky-nav';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { MatCardModule } from '@angular/material/card';
import { AuthenticationGuard } from 'microsoft-adal-angular6';

import { AddComplexComponent } from './add-complex/add-complex.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    AddRoomComponent,
    UpdateRoomComponent,
    RoomDetailsComponent,
    AddComplexComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatCardModule,
    NgbModule,
    // withConfig: remove warning message when using formcontrolname and ngModel
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),
    StickyNavModule
  ],
  providers: [ ProviderLocation, AuthenticationGuard],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
