import { TestBed } from '@angular/core/testing';

import { NotificationService } from './notification.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('NotificationService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ]
  }));

  it('should be created', () => {
    const service: NotificationService = TestBed.get(NotificationService);
    expect(service).toBeTruthy();
  });
});
