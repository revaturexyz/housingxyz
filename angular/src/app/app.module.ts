import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { ManagerModule } from './manager/manager.module';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { NgModule } from '@angular/core';
import { ProviderModule } from './provider/provider.module';
import { TenantModule } from './tenant/tenant.module';

import { AppComponent } from './app.component';

import { environment } from 'src/environments/environment';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    ManagerModule,
    MsalModule.forRoot(environment.msalConfig),
    ProviderModule,
    TenantModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: MsalInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
