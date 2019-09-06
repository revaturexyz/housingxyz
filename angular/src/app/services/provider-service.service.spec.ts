import { TestBed } from '@angular/core/testing';
import { ProviderServiceService } from './provider-service.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';

describe('ProviderServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule]
  }).compileComponents());

  it('should be created', () => {
    const service: ProviderServiceService = TestBed.get(ProviderServiceService);
    expect(service).toBeTruthy();
  });
});
