import { Injectable } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';


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

  constructor(private httpClient: HttpClient) { }
}