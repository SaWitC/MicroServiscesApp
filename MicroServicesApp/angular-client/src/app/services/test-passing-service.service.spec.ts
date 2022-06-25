import { TestBed } from '@angular/core/testing';

import { TestPassingServiceService } from './test-passing-service.service';

describe('TestPassingServiceService', () => {
  let service: TestPassingServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestPassingServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
