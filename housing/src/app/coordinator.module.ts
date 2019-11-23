import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SearchTenantComponent } from './search-tenant/search-tenant.component';

// Module for Coordinator UI, imported into root module: App.module.ts
@NgModule({
  declarations: [
  SearchTenantComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
  ],
})
export class CoordinatorModule { }
