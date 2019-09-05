import { TestBed } from '@angular/core/testing';

import { TrainingcenterService } from './trainingcenter.service';

describe('TrainingcenterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TrainingcenterService = TestBed.get(TrainingcenterService);
    expect(service).toBeTruthy();
  });
});
