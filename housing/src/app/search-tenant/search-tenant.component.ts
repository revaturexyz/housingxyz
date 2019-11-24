import { Component, OnInit } from '@angular/core';
import TenantSearching from '../../interfaces/tenant-searching';
import { TenantSearcherService } from '../services/tenant-searcher.service';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'dev-search-tenant',
  templateUrl: './search-tenant.component.html',
  styleUrls: ['./search-tenant.component.scss']
})
export class SearchTenantComponent implements OnInit {

  //fields and methods for search functionality

  tenants: TenantSearching[] = [];
  tenantsLoaded: boolean = false;

  searchTenantForm = this.formBuilder.group({
    firstName: [''],
    lastName: [''],
    gender: [''],
    filterByTrainingCenter: ['']
  });

  searchTenantsByParameters() {
    this.tenantsLoaded = false;

    let firstName = this.searchTenantForm.get('firstName').value as string;
    let lastName = this.searchTenantForm.get('lastName').value as string;
    let gender = this.searchTenantForm.get('gender').value as string;
    let filterByTrainingCenter = this.searchTenantForm.get('filterByTrainingCenter').value as boolean;

    this.service.getTenantsByParameters(firstName, lastName, gender, filterByTrainingCenter)
    .then(result => {
      this.tenants = result;
      this.tenantsLoaded = true;
    })
  };

  searchAllTenants() {
    this.tenantsLoaded = false;

    this.service.getTenants()
    .then(result => {
      this.tenants = result;
      this.tenantsLoaded = true;
    })
  }

  //fields and methods for tenant view

  



  constructor(
    private service: TenantSearcherService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.searchAllTenants()
  }

}
