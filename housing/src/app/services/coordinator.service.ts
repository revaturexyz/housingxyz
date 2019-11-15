import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

// Service for AJAX Calls to various Rest APIs needed by Coordinators
export class CoordinatorService {
  // add url from environment - need to add coordinator to endpoints


  httpOptions: any;

  constructor(
    private httpbus: HttpClient
  ) {
    this.httpOptions = {
      headers: new HttpHeaders({
      })
    };
  }

  // coordinator methods


}
