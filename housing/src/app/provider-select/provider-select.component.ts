import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'dev-provider-select',
  templateUrl: './provider-select.component.html',
  styleUrls: ['./provider-select.component.scss']
})
export class ProviderSelectComponent implements OnInit {
  providers: Provider[] = [];
  providerDisplayString = 'Select a Provider';

  constructor(
    private providerService: ProviderService,
    private router: Router,
    private user: UserService) { }

  ngOnInit() {
    this.providerService.getProviders()
      .toPromise()
      .then((providers) => {
        this.providers = providers;
      })
      .catch((err) => console.log(err));
  }

  selectProvider(provider: Provider) {
    localStorage.setItem('currentProvider', JSON.stringify(provider));
    this.providerDisplayString = provider.companyName + ' | ' + provider.contactNumber;
    this.router.navigateByUrl('/show-rooms');
  }
}
