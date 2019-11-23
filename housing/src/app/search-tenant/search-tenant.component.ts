import { Component, OnInit } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { TenantSearcherService } from '../services/tenant-searcher.service';

@Component({
  selector: 'dev-search-tenant',
  templateUrl: './search-tenant.component.html',
  styleUrls: ['./search-tenant.component.scss']
})
export class SearchTenantComponent implements OnInit {

  tenants: TenantSearching[] = [];
  tenantsLoaded: boolean = false;



  constructor(
    private service: TenantSearcherService
  ) { }

  ngOnInit() {
    this.service.getTenants()
    .then(result => {
      this.tenants = result;
      this.tenantsLoaded = true;
    })

  }

}
