import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router } from '@angular/router';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
import { Complex } from 'src/interfaces/complex';
import { RedirectService } from '../services/redirect.service';

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
    private router: Router,
    private redirect: RedirectService
  ) { }

  ngOnInit() {
    this.provider = this.redirect.checkProvider();
    if (this.provider !== null) {
      this.getProviderOnInit(this.provider.providerId).then(p => {
        this.provider = p;
        this.getLivingComplexesOnInit();
      });
    } else {
    }
  }

  getProviderOnInit(providerId: number): Promise<Provider> {
    console.log('this runs before error');
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
