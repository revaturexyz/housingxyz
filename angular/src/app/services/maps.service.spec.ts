import { TestBed } from '@angular/core/testing';

import { MapsService } from './maps.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MapsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientTestingModule ]
  }));

  it('should be created', () => {
    const service: MapsService = TestBed.get(MapsService);
    expect(service).toBeTruthy();
  });
});
