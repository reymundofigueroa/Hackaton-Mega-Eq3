import { TestBed } from '@angular/core/testing';

import { GetClientsListService } from './get-clients-list.service';

describe('GetClientsListService', () => {
  let service: GetClientsListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetClientsListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
