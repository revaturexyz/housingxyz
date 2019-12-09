import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { AuthGuard } from './guards/auth.guard';
import { AssignTenantToRoomComponent } from './assign-tenant-to-room/assign-tenant-to-room.component';

import { EditProviderComponent } from './edit-provider/edit-provider.component';
import { ProviderStatusComponent } from './provider-status/provider-status.component';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';
import { SearchTenantComponent } from './search-tenant/search-tenant.component';
import { SelectTenantComponent } from './select-tenant/select-tenant.component';
import { AddTenantComponent } from './add-tenant/add-tenant.component';

const routes: Routes = [
  // dev-assign-tenant-to-room
  { path: 'dev-assign-tenant-to-room', component: AssignTenantToRoomComponent },

  // { path: "location-rooms/:id", component: LocationRoomsComponent }
  { path: 'search-tenant', component: SearchTenantComponent },
  { path: 'select-tenant/:id', component: SelectTenantComponent },
  { path: '', component: HomeComponent },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent, canActivate: [AuthGuard] },
  { path: 'edit-provider', component: EditProviderComponent, canActivate: [AuthGuard] },
  { path: 'provider-status', component: ProviderStatusComponent, canActivate: [AuthGuard] },
  { path: 'manage-complex', component: ManageComplexComponent, canActivate: [AuthGuard] },
  { path: 'add-tenant', component: AddTenantComponent },
  { path: 'search-tenant', component: SearchTenantComponent },
  { path: 'select-tenant/:id', component: SelectTenantComponent },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent, canActivate: [AuthGuard] },
  { path: 'edit-provider', component: EditProviderComponent, canActivate: [AuthGuard] },
  { path: 'provider-status', component: ProviderStatusComponent, canActivate: [AuthGuard] },
  { path: 'manage-complex', component: ManageComplexComponent, canActivate: [AuthGuard] },
  { path: '', component: HomeComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
