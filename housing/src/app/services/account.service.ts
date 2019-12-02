import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Provider } from 'src/interfaces/account/provider';
import { Coordinator } from 'src/interfaces/account/coordinator';

/* This class encapsulates calls to the coordinator and provider controllers of the account service. Any
 * getting or setting of accounts can be done through here.
 */
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  // baseUrl: string = `http://localhost:11080/`;
  baseUrl: string = environment.endpoints.account;

// All:
  // GET - Get user's id from token: api/coordinator-accounts/id
  getId$(): Observable<string> {
    return this.http.get<string>(`${this.baseUrl}api/coordinator-accounts/id`);
  }

// Coordinator Accounts:
  // Get by Id - GET - Unrestricted - /api/coordinator-accounts/{coordinatorId}
  getCoordinatorById$(coordinatorId: string): Observable<Coordinator> {
    return this.http.get<Coordinator>(`${this.baseUrl}api/coordinator-accounts/${coordinatorId}`);
  }

  // Get All - Unrestricted - GET - /api/coordinator-accounts/all
  getAllCoordinators$(): Observable<Coordinator[]> {
    return this.http.get<Coordinator[]>(`${this.baseUrl}api/coordinator-accounts/all`);
  }


// Provider Accounts:

  // Delete by Id - Unrestricted - Coordinator only - DELETE - /api/provider-accounts/{providerId}
  deleteProviderById$(providerId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/provider-accounts/${providerId}`);
  }

  // Get by Id - Unrestricted - GET - /api/provider-accounts/{providerId}
  getProviderById$(providerId: string): Observable<Provider> {
    return this.http.get<Provider>(`${this.baseUrl}api/provider-accounts/${providerId}`);
  }

  // Update at Id - Coordinator or matching email - PUT - /api/provider-accounts/{providerId}
  updateProviderById$(provider: Provider): Observable<any> {
    return this.http.put(`${this.baseUrl}api/provider-accounts/${provider.providerId}`, provider);
  }

  // Approve Provider at Id - Coordinator only - PUT - /api/provider-accounts/{providerId}
  approveProviderAtId$(provider: Provider): Observable<any> {
    return this.http.put(`${this.baseUrl}api/provider-accounts/${provider.providerId}/approve`, provider);
  }
}
