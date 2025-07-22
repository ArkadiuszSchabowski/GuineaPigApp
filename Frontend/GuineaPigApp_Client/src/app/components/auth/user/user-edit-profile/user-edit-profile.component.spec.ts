import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEditProfileComponent } from './user-edit-profile.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';

describe('UserEditProfileComponent', () => {
  let component: UserEditProfileComponent;
  let fixture: ComponentFixture<UserEditProfileComponent>;
  let toastrService: ToastrService;

  class MockToastrService{
    error(message: string){
      console.log(message);
    }
  }
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserEditProfileComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, FormsModule, HttpClientModule],
      providers: [{provide: ToastrService, useClass: MockToastrService}]
    });
    fixture = TestBed.createComponent(UserEditProfileComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
