import { TestBed } from '@angular/core/testing';

import { CoordinatorService } from './coordinator.service';

describe('TenantService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CoordinatorService = TestBed.get(CoordinatorService);
    expect(service).toBeTruthy();
  });
});
