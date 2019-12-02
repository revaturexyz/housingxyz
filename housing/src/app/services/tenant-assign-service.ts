import { Injectable } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import TenantSelected from 'src/interfaces/tenant-selected-info/tenant-selected';
import { Address } from 'src/interfaces/address';
import { TenantInRoom } from '../../interfaces/tenant-in-room';
import { RoomWithTenants } from '../../interfaces/room-with-tenant';


@Injectable({
  providedIn: 'root'
})
export class TenantAssignService {

  getTenants() : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.tenant}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }

  getTenantsByParameters(firstName: string, lastName: string, gender: string, filterByTrainingCenter: boolean) : Promise<TenantSearching[]>{
    let url = `${environment.endpoints.tenant}api/Tenant`;
    return this.httpClient.get<TenantSearching[]>(url).toPromise();
  }


  selectTenant(id: string): Promise<TenantSelected> {
    let url = `${environment.endpoints.tenant}api/Tenant/${id}`;
    return this.httpClient.get<TenantSelected>(url).toPromise();
  }

  selectTenantAddress(addressId: string): Promise<Address> {
    throw new Error("Method not implemented.");
  }

  deleteTenant(id: string):  Promise<HttpResponse<Object>> {
    
    let url = `${environment.endpoints.tenant}api/Tenant/Delete/${id}`;
  
    return this.httpClient.delete(url, { observe: 'response'}).toPromise();
  }

  getTenantsNotAssignedRoom(): Promise<TenantInRoom[]>{
    let url = `${environment.endpoints.tenant}api/Tenant/Unassigned`;
    return this.httpClient.get<TenantInRoom[]>(url).toPromise();
  }

  getAvailableRoomsWithTenants(): Promise<RoomWithTenants[]> {
    let url = `${environment.endpoints.tenant}api/Tenant/Assign/AvailableRooms`;
    return this.httpClient.get<RoomWithTenants[]>(url).toPromise();
  }

  constructor(private httpClient: HttpClient) { }
}
