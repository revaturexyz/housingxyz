import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoordinatorNotificationsComponent } from './coordinator-notifications/coordinator-notifications.component';
import { NotificationDetailsComponent } from './coordinator-notifications/notification-details/notification-details.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'coordinator-notifications', component: CoordinatorNotificationsComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
