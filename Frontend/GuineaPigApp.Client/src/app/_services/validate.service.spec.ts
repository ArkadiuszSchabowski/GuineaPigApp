import { TestBed } from '@angular/core/testing';

import { ValidateService } from './validate.service';
import { ToastrModule, ToastrService } from 'ngx-toastr';

describe('ValidateService', () => {
  let service: ValidateService;
  let toastrService: ToastrService;

  class MockToastrService{
    error(message: string){
      console.log(message)
    }
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ToastrModule],
      providers: [{provide: ToastrService, useClass: MockToastrService}]
    });
    toastrService = TestBed.inject(ToastrService);
    service = TestBed.inject(ValidateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
