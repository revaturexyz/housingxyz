import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RedirectService } from './redirect.service';

describe('RedirectService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ RouterTestingModule ]
  }));

  it('should be created', () => {
    const service: RedirectService = TestBed.get(RedirectService);
    expect(service).toBeTruthy();
  });
});
