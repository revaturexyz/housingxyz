import { Injectable } from '@angular/core';
import { Tenant } from '../../interfaces/tenant';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CoordinatorService {
  //add url from environment - need to add coordinator to endpoints


  httpOptions: any;

  constructor(
    private httpbus: HttpClient,
  ) { }

  //coordinator methods (need backend to finish in order to finish implementation)
  getTenant() {}

  getTenantById() {}

  getTenantByGender() {}

  postTenant() {}

  //need clarification
  //putTenant() {}

  //deleteTenant() {}

}
