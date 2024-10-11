import { TestBed } from '@angular/core/testing';

import { MandatesService } from './mandates.service';

describe('MandatesService', () => {
  let service: MandatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MandatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
