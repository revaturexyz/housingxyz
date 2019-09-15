import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProviderLocation } from 'src/models/location';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ApiService } from './api.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { StickyNavModule } from 'ng2-sticky-nav';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    AddComplexComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    // withConfig: remove warning message when using formcontrolname and ngModel
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),
    StickyNavModule
  ],
  providers: [ProviderLocation, ApiService, AuthenticationGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
