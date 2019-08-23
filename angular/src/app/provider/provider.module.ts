import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ProviderRoutingModule } from './provider-routing.module';

import { ProviderComponent } from './provider.component';

@NgModule({
  declarations: [ProviderComponent],
  imports: [
    CommonModule,
    ProviderRoutingModule
  ]
})
export class ProviderModule { }
