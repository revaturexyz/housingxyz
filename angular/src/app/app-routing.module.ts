import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
// import { LocationRoomsComponent } from './location-rooms/location-rooms.component';


const routes: Routes = [

  { path: "", component: HomeComponent},
  //Will redirect users to azure login
  //{ path: "home", component: HomeComponent, canActivate: [AuthenticationGuard] },
  { path: "login", component: LoginComponent }
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
                       ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
