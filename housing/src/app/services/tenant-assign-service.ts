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

  getTenantsNotAssignedRoom(): Promise<TenantInRoom[]>{
    let url = `${environment.endpoints.tenant}api/Tenant/Unassigned`;
    return this.httpClient.get<TenantInRoom[]>(url).toPromise();
  }

  getAvailableRoomsWithTenants(): Promise<RoomWithTenants[]> {
    let url = `${environment.endpoints.tenant}api/Tenant/Assign/AvailableRooms`;
    return this.httpClient.get<RoomWithTenants[]>(url).toPromise();
  }

  assignTenant(tenantId: string, roomId: string): Promise<Object> {
      let url = `${environment.endpoints.tenant}api/Tenant/Assign/${tenantId}`;
      return this.httpClient.put(url, roomId).toPromise();
  }

  constructor(private httpClient: HttpClient) { }
}
