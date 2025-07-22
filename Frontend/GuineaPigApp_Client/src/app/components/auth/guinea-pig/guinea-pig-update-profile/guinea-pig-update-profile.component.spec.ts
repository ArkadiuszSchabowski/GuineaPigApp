import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigUpdateProfileComponent } from './guinea-pig-update-profile.component';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { TokenService } from 'src/app/_services/token.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GuineaPigUpdateProfileComponent', () => {
  let component: GuineaPigUpdateProfileComponent;
  let fixture: ComponentFixture<GuineaPigUpdateProfileComponent>;
  let toastrService: ToastrService;
  let tokenService: TokenService;

  class MockToastrService {
    error(message: string) {
      console.log(message);
    }
  }

  class MockTokenService {
    getEmailFromToken() {
      return 'someEmail@gmail.com';
    }
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuineaPigUpdateProfileComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, FormsModule, HttpClientTestingModule],
      providers: [
        { provide: ToastrService, useClass: MockToastrService },
        { provide: TokenService, useClass: MockTokenService },
      ],
    });
    fixture = TestBed.createComponent(GuineaPigUpdateProfileComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    tokenService = TestBed.inject(TokenService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
