import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CoordinatorService } from './services/coordinator.service';

// Module for Coordinator UI, imported into root module: App.module.ts
@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
  ],
  providers: [
    HttpClientModule,
    CoordinatorService
  ],
})
export class CoordinatorModule { }
