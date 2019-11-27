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

  getTenants() : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.coordinator}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }

  getTenantsByParameters(firstName: string, lastName: string, gender: string, filterByTrainingCenter: boolean) : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.coordinator}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }


  selectTenant(id: string): Promise<TenantSelected> {
    let url = `${environment.endpoints.coordinator}api/Tenant/${id}`;
    return this.httpClient.get<TenantSelected>(url).toPromise();
  }

  selectTenantAddress(addressId: string): Promise<Address> {
    throw new Error("Method not implemented.");
  }

  deleteTenant(id: string):  Promise<HttpResponse<Object>> {
    
    let url = `${environment.endpoints.coordinator}api/Tenant/Delete/${id}`;
  
    return this.httpClient.delete(url, { observe: 'response'}).toPromise();
  }

  constructor(private httpClient: HttpClient) { }
}
