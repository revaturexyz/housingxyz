import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
<<<<<<< HEAD
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AddRoomComponent } from './add-room/add-room.component';
//import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AddLocationComponent } from './add-location/add-location.component';
=======
import { LoginComponent } from './login/login.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { HomeComponent } from './home/home.component';
// import { AuthenticationGuard } from 'microsoft-adal-angular6';
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
// import { LocationRoomsComponent } from './location-rooms/location-rooms.component';


const routes: Routes = [
<<<<<<< HEAD

  { path: "", component: HomeComponent},
  //Will redirect users to azure login
  //{ path: "home", component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: "add-location", component: AddLocationComponent},
  { path: "login", component: LoginComponent },
  { path: "addroom", component: AddRoomComponent},
  { path: "show-by-location", component: ShowByLocationComponent}
=======
  { path: '', component: HomeComponent },
  { path: 'show-rooms', component: UpdateRoomComponent },
  // { path: 'home', component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'addroom', component: AddRoomComponent}
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
                       ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
