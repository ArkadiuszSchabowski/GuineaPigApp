import { TestBed } from '@angular/core/testing';

import { GuineaPigService } from './guinea-pig.service';
import { HttpClientModule } from '@angular/common/http';

describe('GuineaPigService', () => {
  let service: GuineaPigService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    service = TestBed.inject(GuineaPigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
