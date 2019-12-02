import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';
import { AddComplexComponent } from './manage-complex/add-complex/add-complex.component';
import { ComplexDetailsComponent } from './manage-complex/complex-details/complex-details.component';
import { AddRoomComponent } from './manage-complex/add-room/add-room.component';
import { EditRoomComponent } from './manage-complex/edit-room/edit-room.component';
import { ShowRoomComponent } from './manage-complex/show-room/show-room.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent },
  { path: 'add-complex', component: AddComplexComponent },
  { path: 'complex-details', component: ComplexDetailsComponent},
  { path: 'manage-complex', component: ManageComplexComponent },
  { path: 'add-room', component: AddRoomComponent },
  { path: 'edit-room', component: EditRoomComponent },
  { path: 'show-room', component: ShowRoomComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
