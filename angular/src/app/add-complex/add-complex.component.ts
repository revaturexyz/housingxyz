import { Component, OnInit } from '@angular/core';
import { ProviderService } from '../services/provider.service';
import { Provider } from 'src/interfaces/provider';

@Component({
  selector: 'dev-add-complex',
  templateUrl: './add-complex.component.html',
  styleUrls: ['./add-complex.component.scss']
})
export class AddComplexComponent implements OnInit {
  currentProvider: Provider;
  showAddressForm = false;
  
  constructor(
    private providerService: ProviderService
  ) { }

  ngOnInit() {
    this.getProviderOnInit();
  }

  getProviderOnInit(): void {
    this.providerService.getProviderById(2)
      .toPromise()
      .then((provider) => this.currentProvider = provider)
      .catch((err) => console.log(err));
  }
}
