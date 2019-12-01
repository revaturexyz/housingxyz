import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/account/provider';
import { Complex } from 'src/interfaces/complex';

@Component({
  selector: 'dev-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  locationList: object;
  roomList: object;
  provider: Provider;
  complexes: Complex[];

  constructor(
    private providerService: ProviderService,
    private router: Router
  ) { }

  ngOnInit() {
    // This is not how redirects should work if no provider is selected.
    // It is likely a guard will need to be implemented to accomplish this task.
    if (this.provider !== null) {
      this.getProviderOnInit(this.provider.providerId).then(p => {
        this.provider = p;
        this.getLivingComplexesOnInit();
      });
    }
  }

  getProviderOnInit(providerId: number): Promise<Provider> {
    return this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => this.provider = provider);
  }

  getLivingComplexesOnInit(): void {
    this.providerService.getComplexesByProvider(this.provider.providerId)
      .toPromise()
      .then((complexes) => this.complexes = complexes)
      .catch((err) => console.log(err));
  }
}
