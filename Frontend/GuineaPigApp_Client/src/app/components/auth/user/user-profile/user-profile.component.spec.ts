import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserProfileComponent } from './user-profile.component';
import { FormsModule } from '@angular/forms';
import { TokenService } from 'src/app/_services/token.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('UserProfileComponent', () => {
  let component: UserProfileComponent;
  let fixture: ComponentFixture<UserProfileComponent>;
  let tokenService: TokenService;

  class MockTokenService {
    getEmailFromToken() {
      return 'someEmail@gmail.com';
    }
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserProfileComponent],
      imports: [FormsModule, HttpClientTestingModule],
      providers: [{ provide: TokenService, useClass: MockTokenService }],
    });
    fixture = TestBed.createComponent(UserProfileComponent);
    component = fixture.componentInstance;
    tokenService = TestBed.inject(TokenService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
