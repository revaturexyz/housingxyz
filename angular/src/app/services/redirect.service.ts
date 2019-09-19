import { Injectable } from '@angular/core';
import { Router, Routes } from '@angular/router';
import { Provider } from 'src/interfaces/provider';

@Injectable({
  providedIn: 'root'
})
export class RedirectService {

  provider: Provider;

  constructor(private router: Router) { }

  // this function checks if there is currently a provider in local storage as a cookie.
  // if the provider does not exist in local data, a user must select one before proceeding.
  // if this is the case, it automatically redirects the user to the right page.
  checkProvider() {
    try {
      this.provider = JSON.parse(localStorage.getItem('currentProvider'));
      if (this.provider == null) {
        this.router.navigateByUrl('/login');
      }
      return this.provider;
    } catch {
      this.router.navigateByUrl('/login');
    }
  }
}
