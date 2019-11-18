import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

// Service for AJAX Calls to various Rest APIs needed by Coordinators
export class CoordinatorService {

  GetTenant() {}

  GetTenantById() {}

  GetTenantByGender() {}

  PostTenant() {}

  PutTenant() {}

  DeleteTenant() {}
}
