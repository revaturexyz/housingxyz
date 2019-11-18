import { Component, OnInit, Input } from '@angular/core';
import { Tenant } from '../../interfaces/tenant';
import { CoordinatorService } from '../services/coordinator.service';

@Component({
  selector: 'dev-show-tenant',
  templateUrl: './show-tenant.component.html',
  styleUrls: ['./show-tenant.component.scss']
})
export class ShowTenantComponent implements OnInit {

  constructor(private coordService: CoordinatorService) { }

  @Input() tenant: Tenant;

  show() {
    return this.coordService.GetTenant();
  }

  //show(tenantId)

  //show(tenantGender)
  
  update() {
    this.coordService.PutTenant();
  }

  delete() {
    this.coordService.DeleteTenant();
  }

  ngOnInit() {
  }

}
