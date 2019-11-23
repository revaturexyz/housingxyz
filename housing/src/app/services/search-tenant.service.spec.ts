import { TestBed } from '@angular/core/testing';

import { SearchTenantService } from './search-tenant.service';

describe('SearchTenantService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SearchTenantService = TestBed.get(SearchTenantService);
    expect(service).toBeTruthy();
  });
});
