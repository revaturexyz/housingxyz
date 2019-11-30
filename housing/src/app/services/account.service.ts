import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  // baseUrl: string = `http://localhost:11080/`;
  baseUrl: string = environment.endpoints.account;

// All:
  // GET     Get user's id from token: api/coordinator-accounts/id
  getId$(): Observable<any> {
    return this.http.get(`${this.baseUrl}api/coordinator-accounts/id`);
  }

}
