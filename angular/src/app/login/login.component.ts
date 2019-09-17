import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';

@Component({
  selector: 'dev-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  providers: Provider[] = [];
  providerDisplayString = 'Select a Provider';

  constructor(
    private providerService: ProviderService
  ) { }

  ngOnInit() {
    this.providerService.getProviders()
      .toPromise()
      .then((providers) => {
        this.providers = providers;
        console.log(providers);
      })
      .catch((err) => console.log(err));
  }

  selectProvider(provider: Provider) {
    localStorage.setItem('currentProvider', JSON.stringify(provider));
    this.providerDisplayString = provider.companyName + ' | ' + provider.contactNumber;
  }
}
