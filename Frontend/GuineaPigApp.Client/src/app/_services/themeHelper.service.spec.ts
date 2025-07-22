import { TestBed } from '@angular/core/testing';

import { ThemeHelper } from './themeHelper.service';

describe('ThemeHelperService', () => {
  let service: ThemeHelper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThemeHelper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
