import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { ManagerModule } from './manager/manager.module';
import { MsalModule } from '@azure/msal-angular';
import { NgModule } from '@angular/core';
import { ProviderModule } from './provider/provider.module';
import { TenantModule } from './tenant/tenant.module';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    ManagerModule,
    MsalModule.forRoot({
      clientID: '',
      postLogoutRedirectUri: '/'
    }),
    ProviderModule,
    TenantModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
