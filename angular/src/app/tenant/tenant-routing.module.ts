import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TenantComponent } from './tenant.component';

import { MsalGuard } from '@azure/msal-angular';

const routes: Routes = [
  { path: 'tenant', component: TenantComponent, canActivate: [MsalGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TenantRoutingModule { }
