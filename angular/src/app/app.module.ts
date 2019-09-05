import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Room } from 'src/models/room';
import { Provider } from 'src/models/provider';
import { ProviderLocation } from 'src/models/location';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { StickyNavModule } from 'ng2-sticky-nav';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddRoomComponent } from './add-room/add-room.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    AddRoomComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),//withConfig: remove warning message when using formcontrolname and ngModel
    StickyNavModule
  ],
  providers: [Room, Provider, ProviderLocation, AuthenticationGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
