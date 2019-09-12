import { TestBed } from '@angular/core/testing';

import { ComplexService } from './complex.service';

describe('ComplexService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ComplexService = TestBed.get(ComplexService);
    expect(service).toBeTruthy();
  });
});
