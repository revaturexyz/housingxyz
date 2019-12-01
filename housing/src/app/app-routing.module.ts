import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProviderSelectComponent } from './provider-select/provider-select.component';
import { AddRoomComponent } from './manage-complex/add-room/add-room.component';
import { EditRoomComponent } from './manage-complex/edit-room/edit-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { HomeComponent } from './home/home.component';
import { AddComplexComponent } from './manage-complex/add-complex/add-complex.component';
import { AddProviderComponent } from './add-provider/add-provider.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { AuthGuard } from './guards/auth.guard';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';
import { ComplexDetailsComponent } from './manage-complex/complex-details/complex-details.component';
import { ShowRoomComponent } from './manage-complex/show-room/show-room.component';
import { TrainingLocationsComponent } from './training-locations/training-locations.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'show-rooms', component: UpdateRoomComponent },
  { path: 'provider-select', component: ProviderSelectComponent, canActivate: [AuthGuard] },
  { path: 'add-provider', component: AddProviderComponent },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent },
  { path: 'coordinator-notifications/:id', component: NotificationDetailsComponent },
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
  { path: 'add-complex', component: AddComplexComponent },
  { path: 'complex-details', component: ComplexDetailsComponent},
  { path: 'manage-complex', component: ManageComplexComponent },
  { path: 'add-room', component: AddRoomComponent },
  { path: 'edit-room', component: EditRoomComponent },
  { path: 'show-room', component: ShowRoomComponent },
  { path: 'training-locations', component: TrainingLocationsComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
