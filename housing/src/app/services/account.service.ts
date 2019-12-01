import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

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
  getId$(): Observable<any> {
    return this.http.get(`${this.baseUrl}api/coordinator-accounts/id`);
  }

  // CoordinatorAccount

  // Get by Id - GET - Unrestricted - /api/coordinator-accounts/{coordinatorId}
  // Get All - Unrestricted - GET - /api/coordinator-accounts/all


// ProviderAccount

  // Delete by Id - Unrestricted - Coordinator only - DELETE - /api/provider-accounts/{providerId}
  // Get by Id - Unrestricted - GET - /api/provider-accounts/{providerId}
  // Update at Id - Coordinator or matching email - PUT - /api/provider-accounts/{providerId}
  // Approve Provider at Id - Coordinator only - PUT - /api/provider-accounts/{providerId}
}
