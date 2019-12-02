import { TestBed } from '@angular/core/testing';

import { TenantSearcherService } from './tenant-searcher.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';

describe('TenantSearcherService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ],
    providers: [
      TenantSearcherService
    ]
  }));

  it('should be created', () => {
    const service: TenantSearcherService = TestBed.get(TenantSearcherService);
    expect(service).toBeTruthy();
  });
});
