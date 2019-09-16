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

  ngOnInit() {
    // get locations belonging to the provider
    this.getProviderOnInit();
    this.getLivingComplexesOnInit();
  }

  getProviderOnInit(): void {
    this.providerService.getProviderById(1)
      .toPromise()
      .then((provider) => this.provider = provider)
      .catch((err) => console.log(err));
  }

  getLivingComplexesOnInit(): void {
    this.providerService.getComplexesByProvider(2)
      .toPromise()
      .then((complexes) => this.complexes = complexes)
      .catch((err) => console.log(err));
  }
}
