import { TestBed } from '@angular/core/testing';

import { TenantSearcherService } from './tenant-searcher.service';

describe('TenantSearcherService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TenantSearcherService = TestBed.get(TenantSearcherService);
    expect(service).toBeTruthy();
  });
});
