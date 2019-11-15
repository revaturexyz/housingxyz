import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class CoordinatorService {
  // add url from environment - need to add coordinator to endpoints


  httpOptions: any;

  constructor(
    private httpbus: HttpClient,
    msalService: MsalService
  ) {
    this.httpOptions = {
      headers: new HttpHeaders({
        Authorization: msalService.getUser().userIdentifier
      })
    };
  }

  // coordinator methods


}
