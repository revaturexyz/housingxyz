import { Component, OnInit } from '@angular/core';
import { CoordinatorService } from '../services/coordinator.service';

@Component({
  selector: 'dev-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.scss']
})
export class AddTenantComponent implements OnInit {

  constructor(private coordService: CoordinatorService) { }

  add() {
    this.coordService.PostTenant();
  }

  ngOnInit() {
  }

}
