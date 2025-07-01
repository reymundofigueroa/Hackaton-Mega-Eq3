import { TestBed } from '@angular/core/testing';

import { AssignPromoToClientService } from './assign-promo-to-client.service';

describe('AssignPromoToClientService', () => {
  let service: AssignPromoToClientService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AssignPromoToClientService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
