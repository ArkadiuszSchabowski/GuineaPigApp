import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigCheckWeightsComponent } from './guinea-pig-check-weights.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { TokenService } from 'src/app/_services/token.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';

describe('GuineaPigCheckWeightsComponent', () => {
  let component: GuineaPigCheckWeightsComponent;
  let fixture: ComponentFixture<GuineaPigCheckWeightsComponent>;
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
      declarations: [GuineaPigCheckWeightsComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, FormsModule, HttpClientModule],
      providers: [
        { provide: ToastrService, useClass: MockToastrService },
        { provide: TokenService, useClass: MockTokenService },
      ],
    });
    fixture = TestBed.createComponent(GuineaPigCheckWeightsComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    tokenService = TestBed.inject(TokenService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
