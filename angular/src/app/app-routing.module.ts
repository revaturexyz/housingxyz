import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AddRoomComponent } from './add-room/add-room.component';
// import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddLocationComponent } from './add-location/add-location.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
// import { AuthenticationGuard } from 'microsoft-adal-angular6';
// import { LocationRoomsComponent } from './location-rooms/location-rooms.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'show-rooms', component: UpdateRoomComponent },
  // { path: 'home', component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'addroom', component: AddRoomComponent}
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
                       ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
