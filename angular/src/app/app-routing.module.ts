import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { DeleteRoomComponent } from './delete-room/delete-room.component';
import { LoginComponent } from './login/login.component';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddLocationComponent } from './add-location/add-location.component';
// import { LocationRoomsComponent } from './location-rooms/location-rooms.component';
import { ShowByLocationComponent } from './show-by-location/show-by-location.component';


const routes: Routes = [

  { path: "", component: HomeComponent},
  //Will redirect users to azure login
  //{ path: "home", component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: "add-room/:id", component: AddRoomComponent },
  { path: "add-location", component: AddLocationComponent},
  { path: "update-room/:id", component: UpdateRoomComponent },
  { path: "delete-room/:roomId", component: DeleteRoomComponent },
  { path: "login", component: LoginComponent },
  { path: "show-by-location", component: ShowByLocationComponent}
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
