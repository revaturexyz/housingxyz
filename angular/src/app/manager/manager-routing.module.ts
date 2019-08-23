import { ManagerComponent } from './manager.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MsalGuard } from '@azure/msal-angular';

const routes: Routes = [
  { path: 'manager', component: ManagerComponent, canActivate: [MsalGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagerRoutingModule { }
