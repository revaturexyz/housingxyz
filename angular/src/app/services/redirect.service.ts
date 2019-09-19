import { Injectable } from '@angular/core';
import { Router, Routes } from '@angular/router';
import { Provider } from 'src/interfaces/provider';

@Injectable({
  providedIn: 'root'
})
export class RedirectService {

  provider: Provider;

  constructor(private router: Router) { }

  checkProvider() {
    try {
      this.provider = JSON.parse(localStorage.getItem('currentProvider'));
      if (this.provider == null) {
        this.router.navigateByUrl('/provider-select');
      }
      return this.provider;
    } catch {
      this.router.navigateByUrl('/provider-select');
    }
  }
}
