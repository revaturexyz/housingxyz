import { Injectable } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import TenantSelected from 'src/interfaces/tenant-selected-info/tenant-selected';


@Injectable({
  providedIn: 'root'
})
export class TenantSearcherService {
  selectTenant(id: string): Promise<TenantSelected> {
    let url = `${environment.endpoints.coordinator}api/Tenant/${id}`;
    return this.httpClient.get<TenantSelected>(url).toPromise();
  }

  getTenants() : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.coordinator}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }

  getTenantsByParameters(firstName: string, lastName: string, gender: string, filterByTrainingCenter: boolean) : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.coordinator}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }

  constructor(private httpClient: HttpClient) { }
}
