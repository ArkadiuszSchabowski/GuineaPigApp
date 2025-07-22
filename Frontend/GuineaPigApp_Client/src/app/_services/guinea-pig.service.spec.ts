import { TestBed } from '@angular/core/testing';

import { GuineaPigService } from './guinea-pig.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GuineaPigService', () => {
  let service: GuineaPigService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GuineaPigService]
    });
    service = TestBed.inject(GuineaPigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
