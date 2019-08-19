import { Injectable } from '@angular/core';
import {  of, Observable } from 'rxjs';


@Injectable()
export class MsAdalAngular6ServiceMock {
  constructor() { }

  acquireToken(): Observable<string> {
      return of("testing Token");
  }
}