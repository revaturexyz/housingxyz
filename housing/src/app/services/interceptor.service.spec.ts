import { TestBed } from '@angular/core/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {Router} from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { InterceptorService } from './interceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

class BlankComponent {

}

describe('InterceptorService', () => {
  // let service: DataService;
  let httpMock: HttpTestingController;

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
      HttpClientTestingModule],
    providers: [
      {
        provide: HTTP_INTERCEPTORS,
        useClass: InterceptorService,
        multi: true,
      },
    ]
  })

  // service = TestBed.get(DataService);
  httpMock = TestBed.get(HttpTestingController);
});

  it('should be created', () => {
    const service: InterceptorService = TestBed.get(InterceptorService);
    expect(service).toBeTruthy();
  });

  // Can be implemented when there is an API call to make
  /*it('should add an Authorization header', () => {
    service.getPosts().subscribe(response => {
      expect(response).toBeTruthy();
    });
  
    const httpRequest = httpMock.expectOne(`${service.ROOT_URL}/posts`);
  
    expect(httpRequest.request.headers.has('Authorization')).toEqual(true);
  });*/
});
