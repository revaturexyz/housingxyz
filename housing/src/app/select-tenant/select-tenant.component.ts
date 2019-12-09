import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TenantSearcherService } from '../services/tenant-searcher.service';
import TenantSelected from 'src/interfaces/tenant-selected-info/tenant-selected';
import { Address } from 'src/interfaces/address';

@Component({
  selector: 'dev-select-tenant',
  templateUrl: './select-tenant.component.html',
  styleUrls: ['./select-tenant.component.scss']
})
export class SelectTenantComponent implements OnInit {
  tenant: TenantSelected | null = null;
  tenantLoaded = false;
  address: Address;

  // Delete tenant
  deleteConfirmOn = false;

  deleteTrigger() {
    this.deleteConfirmOn = true;
  }

  deleteStop() {
    this.deleteConfirmOn = false;
  }

  deleteGo() {
    this.service.deleteTenant(this.tenant.id)
      .then(value => {
        console.log(value);
        this.router.navigate(['search-tenant']);
      });
  }

  routeToSearchTenant() {
    this.router.navigate(['search-tenant']);
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TenantSearcherService
  ) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    this.service.selectTenant(id)
      .then(x => {
        this.tenant = x;
        this.tenantLoaded = true;
      });
  }

}
