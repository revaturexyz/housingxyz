import { TestBed, inject, getTestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AuthGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';
import { Observable, of } from 'rxjs';

describe('AuthGuard', () => {
  const authService: any = { isAuthenticated$: of(true) };
  let guard: AuthGuard;
  const routeMock: any = { snapshot: {} };
  const routeStateMock: any = { snapshot: {}, url: '/provider-select' };
  const routerMock = { navigate: jasmine.createSpy('navigate') };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthGuard,
        { provide: Router, useValue: routerMock },
        { provide: AuthService, useValue: authService }]
    });
  });

  guard = new AuthGuard(authService);

  it('should ...', inject([AuthGuard], () => {
    expect(guard).toBeTruthy();
  }));

  it('should allow the authenticated user to access app', async done => {
    (guard.canActivate(routeMock, routeStateMock) as Observable<boolean>).subscribe(res => {
      expect(res).toEqual(true);
      done();
    });
  });
});
