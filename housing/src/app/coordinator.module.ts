import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SearchTenantComponent } from './search-tenant/search-tenant.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

// Module for Coordinator UI, imported into root module: App.module.ts
@NgModule({
  declarations: [
  SearchTenantComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
  ],
})
export class CoordinatorModule { }
