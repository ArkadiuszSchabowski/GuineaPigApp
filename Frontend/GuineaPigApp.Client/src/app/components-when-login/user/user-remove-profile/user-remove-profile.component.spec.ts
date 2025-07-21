import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRemoveProfileComponent } from './user-remove-profile.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { AngularMaterialModule } from 'src/app/_modules/angular-material/angular-material.module';

describe('UserRemoveProfileComponent', () => {
  let component: UserRemoveProfileComponent;
  let fixture: ComponentFixture<UserRemoveProfileComponent>;
  let toastrService: ToastrService;

  class MockToastrService {
    error(message: string) {
      console.log(message);
    }
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserRemoveProfileComponent],
      imports: [AngularMaterialModule, FormsModule, HttpClientModule],
      providers: [{ provide: ToastrService, useClass: MockToastrService }],
    });
    fixture = TestBed.createComponent(UserRemoveProfileComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
