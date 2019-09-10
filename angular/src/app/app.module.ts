import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe } from '@angular/core';
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
import { MsAdalAngular6Module, AuthenticationGuard } from 'microsoft-adal-angular6';
import { environment } from '../environments/environment';
import { DatePipe, CommonModule } from '@angular/common';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';
import { MatChipsModule, MatPaginatorModule, MatProgressSpinnerModule,
  MatSortModule, MatTableModule } from '@angular/material';
import {CdkTableModule} from '@angular/cdk/table';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    AddRoomComponent,
    UpdateRoomComponent,
    RoomDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    MatTableModule,
    MatChipsModule,
    CdkTableModule,
    MatCardModule,
    // withConfig: remove warning message when using formcontrolname and ngModel
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),
    StickyNavModule
  ],
  providers: [ ProviderLocation, AuthenticationGuard],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
