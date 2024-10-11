import { TestBed } from '@angular/core/testing';

import { ClosureService } from './closure.service';

describe('ClosureService', () => {
  let service: ClosureService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClosureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
