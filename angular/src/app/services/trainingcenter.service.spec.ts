import { TestBed } from '@angular/core/testing';
import { TrainingcenterService } from './trainingcenter.service';
import { HttpClientTestingModule, HttpTestingController  } from '@angular/common/http/testing';

describe('TrainingcenterService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientTestingModule ]
  }).compileComponents());

  it('should be created', () => {
    const service: TrainingcenterService = TestBed.get(TrainingcenterService);
    expect(service).toBeTruthy();
  });
});
