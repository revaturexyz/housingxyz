import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { TenantSearcherService } from '../services/tenant-searcher.service';
import TenantSelected from 'src/interfaces/tenant-selected-info/tenant-selected';

@Component({
  selector: 'dev-select-tenant',
  templateUrl: './select-tenant.component.html',
  styleUrls: ['./select-tenant.component.scss']
})
export class SelectTenantComponent implements OnInit {
  tenant: TenantSelected;
  tenantLoaded: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TenantSearcherService
  ) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.service.selectTenant(id)
      .then(tenant => {
        this.tenant = tenant;
        this.tenantLoaded = true;
      })
  }

}
