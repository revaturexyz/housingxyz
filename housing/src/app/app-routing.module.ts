import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProviderSelectComponent } from './provider-select/provider-select.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { HomeComponent } from './home/home.component';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { AddProviderComponent } from './add-provider/add-provider.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { AuthGuard } from './guards/auth.guard';
import { AssignTenantToRoomComponent } from './assign-tenant-to-room/assign-tenant-to-room.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'show-rooms', component: UpdateRoomComponent },
  { path: 'provider-select', component: ProviderSelectComponent},
  { path: 'add-provider', component: AddProviderComponent },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent },
  { path: 'coordinator-notifications/:id', component: NotificationDetailsComponent },
  // dev-assign-tenant-to-room
  { path: 'dev-assign-tenant-to-room', component: AssignTenantToRoomComponent },

  // { path: "location-rooms/:id", component: LocationRoomsComponent }
  { path: 'add-complex', component: AddComplexComponent },
  { path: 'addroom', component: AddRoomComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
