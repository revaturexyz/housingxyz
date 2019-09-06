import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Pipe } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Room } from 'src/models/room';
import { Provider } from 'src/models/provider';
import { ProviderLocation } from 'src/models/location';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule} from '@angular/forms'
import { StickyNavModule } from 'ng2-sticky-nav';
import { MsAdalAngular6Module,AuthenticationGuard } from 'microsoft-adal-angular6';
import { environment } from '../environments/environment';
import { AddLocationComponent } from './add-location/add-location.component';
import { ShowByLocationComponent } from './show-by-location/show-by-location.component';
import { DatePipe, CommonModule } from '@angular/common';
import { RouterLinkDirectiveStub } from './testing/router-link-directive-stub';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { RoomDetailsComponent } from './room-details/room-details.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    AddLocationComponent,
    ShowByLocationComponent,
    RouterLinkDirectiveStub,
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
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),//withConfig: remove warning message when using formcontrolname and ngModel
    StickyNavModule
  ],
  providers: [Room, Provider, ProviderLocation, AuthenticationGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
