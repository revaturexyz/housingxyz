import { Component, OnInit, ɵConsole } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';
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

  async ngOnInit() {
    // get locations belonging to the provider
    await this.getProviderOnInit();
    this.getLivingComplexesOnInit();
  }

  async getProviderOnInit() {
    let providerId = 0;
    try {
      providerId = JSON.parse(localStorage.getItem('currentProvider')).providerId;
    } catch {
      this.router.navigate(['/login']);
    }
    
    await this.providerService.getProviderById(providerId)
      .toPromise()
      .then((provider) => this.provider = provider)
      .catch((err) => console.log(err));
  }

  getLivingComplexesOnInit(): void {
    this.providerService.getComplexesByProvider(this.provider.providerId)
      .toPromise()
      .then((complexes) => this.complexes = complexes)
      .catch((err) => console.log(err));
  }
}
