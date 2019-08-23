import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { TenantRoutingModule } from './tenant-routing.module';

import { TenantComponent } from './tenant.component';

@NgModule({
  declarations: [
    TenantComponent
  ],
  imports: [
    CommonModule,
    TenantRoutingModule
  ]
})
export class TenantModule { }
