import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { HomeComponent } from './home/home.component';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { MsalGuard } from '@azure/msal-angular';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'show-rooms', component: UpdateRoomComponent },
  { path: 'login', component: LoginComponent, canActivate: [ MsalGuard ]  },
  { path: 'addroom', component: AddRoomComponent},
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
  { path: 'add-complex', component: AddComplexComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
                       ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
