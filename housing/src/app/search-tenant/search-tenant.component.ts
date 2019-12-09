import { Component, OnInit } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { TenantSearcherService } from '../services/tenant-searcher.service';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'dev-search-tenant',
  templateUrl: './search-tenant.component.html',
  styleUrls: ['./search-tenant.component.scss']
})
export class SearchTenantComponent implements OnInit {

  // fields and methods for search functionality
  trainCen = '837c3248-1685-4d08-934a-0f17a6d1836a';
  tenants: TenantSearching[] = [];
  tenantsLoaded = false;

  searchTenantForm = this.formBuilder.group({
    firstName: [''],
    lastName: [''],
    gender: [''],
    filterByTrainingCenter: ['']
  });

  searchTenantsByParameters() {
    this.tenantsLoaded = false;

    const firstName = this.searchTenantForm.get('firstName').value as string;
    const lastName = this.searchTenantForm.get('lastName').value as string;
    const gender = this.searchTenantForm.get('gender').value as string;
    const filterByTrainingCenter = this.searchTenantForm.get('filterByTrainingCenter').value as boolean;

    let trainingCenter: string | null;
    if (filterByTrainingCenter) {
      trainingCenter = this.trainCen;
    } else {
      trainingCenter = null;
    }

    this.service.getTenantsByParameters(firstName, lastName, gender, trainingCenter)
      .then(result => {
        this.tenants = result;
        this.tenantsLoaded = true;
      });
  }

  searchAllTenants() {
    this.tenantsLoaded = false;

    this.service.getTenants()
      .then(result => {
        this.tenants = result;
        this.tenantsLoaded = true;
      });
  }

  routeToSelectTenant(tenantId: string) {
    this.router.navigate(['select-tenant/' + tenantId]);
  }

  constructor(
    private service: TenantSearcherService,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.searchAllTenants();
  }
}
