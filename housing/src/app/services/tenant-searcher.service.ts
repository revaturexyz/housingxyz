import { Injectable } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import TenantSelected from 'src/interfaces/tenant-selected-info/tenant-selected';
import { Address } from 'src/interfaces/address';


@Injectable({
  providedIn: 'root'
})
export class TenantSearcherService {
  // Get all tenants from api
  getTenants(): Promise<TenantSearching[]> {
    const url = `${environment.endpoints.tenant}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }

  // Get all tenants meeting parameters from api
  getTenantsByParameters(firstName: string, lastName: string, gender: string, filterByTrainingCenter: string | null)
    : Promise<TenantSearching[]> {
    const url = `${environment.endpoints.tenant}api/Tenant`;

    let queryString = `?firstName=${firstName}&lastName=${lastName}`;
    if (gender.toUpperCase() !== 'ALL') {
      queryString = queryString + `&gender=${gender}`;
    }
    // if not null
    if (filterByTrainingCenter) {
      queryString = queryString + `&trainingCenter=${filterByTrainingCenter}`;
    }

    return this.httpClient.get<TenantSearching[]>(url + queryString).toPromise();
  }

  // Get tenant by id from api
  selectTenant(id: string): Promise<TenantSelected> {
    const url = `${environment.endpoints.tenant}api/Tenant/${id}`;
    return this.httpClient.get<TenantSelected>(url).toPromise();
  }

  // Get tenant address by address id from address api
  selectTenantAddress(addressId: string): Promise<Address> {
    throw new Error('Method not implemented.');
  }

  // Delete tenant by id through api
  deleteTenant(id: string): Promise<HttpResponse<object>> {
    const url = `${environment.endpoints.tenant}api/Tenant/Delete/${id}`;

    return this.httpClient.delete(url, { observe: 'response'}).toPromise();
  }

  constructor(private httpClient: HttpClient) { }
}
