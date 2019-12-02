import { TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {Router} from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { InterceptorService } from './interceptor.service';
import { HTTP_INTERCEPTORS, HttpRequest, HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable, of } from 'rxjs';

class BlankComponent {

}

describe('InterceptorService', () => {
  // let service: DataService;
  const authService: any = { getTokenSilently$() { return of('token'); } };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule.withRoutes([
        {
          path: '',
          component: BlankComponent
        }, {
          path: 'login-splash',
          component: BlankComponent
        }]),
        HttpClientModule,
        HttpClientTestingModule],
      providers: [
        {
          provide: AuthService,
          useValue: authService
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: InterceptorService,
          multi: true,
        },
      ]
    });
  });

  it('should be created', () => {
    const service: InterceptorService = TestBed.get(InterceptorService);
    expect(service).toBeTruthy();
  });

  it('adds Authorization header', inject(
    [HttpTestingController, HttpClient],
    (httpMock: HttpTestingController, httpClient: HttpClient) => {

      httpClient.get('https://google.com').subscribe();
      const req = httpMock.expectOne(r => r.headers.has('Authorization'));
      expect(req).toBeTruthy();
      httpMock.verify();
  }));

});
