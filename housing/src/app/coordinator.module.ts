import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { SearchTenantComponent } from './search-tenant/search-tenant.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SelectTenantComponent } from './select-tenant/select-tenant.component';
import { TenantSearcherService } from './services/tenant-searcher.service';
import { RouterModule } from '@angular/router';

// Module for Coordinator UI, imported into root module: App.module.ts
@NgModule({
  declarations: [
  SearchTenantComponent,
  SelectTenantComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([])
  ],
  providers: [
    HttpClientModule,
    TenantSearcherService
  ],
})
export class CoordinatorModule { }
