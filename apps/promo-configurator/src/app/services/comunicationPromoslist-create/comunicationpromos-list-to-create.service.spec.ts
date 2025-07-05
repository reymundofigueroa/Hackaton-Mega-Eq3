import { TestBed } from '@angular/core/testing';

import { ComunicationpromosListToCreateService } from './comunicationpromos-list-to-create.service';

describe('ComunicationpromosListToCreateService', () => {
  let service: ComunicationpromosListToCreateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComunicationpromosListToCreateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
