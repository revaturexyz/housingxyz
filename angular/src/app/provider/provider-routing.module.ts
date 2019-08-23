import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ProviderComponent } from './provider.component';

import { MsalGuard } from '@azure/msal-angular';

const routes: Routes = [
  { path: 'provider', component: ProviderComponent, canActivate: [MsalGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProviderRoutingModule { }
