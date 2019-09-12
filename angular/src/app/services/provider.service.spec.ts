import { TestBed } from '@angular/core/testing';
import { ProviderService } from './provider.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';

describe('ProviderService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }).compileComponents());

  it('should be created', () => {
    const service: ProviderService = TestBed.get(ProviderService);
    expect(service).toBeTruthy();
  });
});
