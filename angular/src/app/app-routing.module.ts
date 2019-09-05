import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AddRoomComponent } from './add-room/add-room.component';
//import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddLocationComponent } from './add-location/add-location.component';
// import { LocationRoomsComponent } from './location-rooms/location-rooms.component';
import { ShowByLocationComponent } from './show-by-location/show-by-location.component';


const routes: Routes = [

  { path: "", component: HomeComponent},
  //Will redirect users to azure login
  //{ path: "home", component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: "add-location", component: AddLocationComponent},
  { path: "login", component: LoginComponent },
  { path: "addroom", component: AddRoomComponent},
  { path: "show-by-location", component: ShowByLocationComponent}
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
                       ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
