import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProviderSelectComponent } from './provider-select/provider-select.component';
import { AddRoomComponent } from './add-room/add-room.component';
import { UpdateRoomComponent } from './update-room/update-room.component';
import { HomeComponent } from './home/home.component';
import { AddComplexComponent } from './add-complex/add-complex.component';
import { AuthGuard } from './guards/auth.guard';
import { ManageComplexComponent } from './manage-complex/manage-complex.component';
import { ComplexDetailsComponent } from './manage-complex/complex-details/complex-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'show-rooms', component: UpdateRoomComponent },
  { path: 'provider-select', component: ProviderSelectComponent, canActivate: [AuthGuard] },
  { path: 'addroom', component: AddRoomComponent },
  // { path: "location-rooms/:id", component: LocationRoomsComponent }
  { path: 'add-complex', component: AddComplexComponent },
  { path: 'addroom', component: AddRoomComponent },
  { path: 'manage-complex', component:ManageComplexComponent},
  { path: 'dev-complex-details', component:ComplexDetailsComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
