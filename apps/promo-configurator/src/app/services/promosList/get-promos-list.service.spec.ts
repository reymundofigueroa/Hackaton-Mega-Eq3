import { TestBed } from '@angular/core/testing';

import { GetPromosListService } from './get-promos-list.service';

describe('GetPromosListService', () => {
  let service: GetPromosListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetPromosListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
