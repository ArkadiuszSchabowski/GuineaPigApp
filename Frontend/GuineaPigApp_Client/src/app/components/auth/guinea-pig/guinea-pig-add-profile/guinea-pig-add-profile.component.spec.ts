import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigAddProfileComponent } from './guinea-pig-add-profile.component';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GuineaPigAddProfileComponent', () => {
  let component: GuineaPigAddProfileComponent;
  let fixture: ComponentFixture<GuineaPigAddProfileComponent>;
  let toastrService: ToastrService;

  class MockToastrService {
    error(message: string) {
      console.log(message);
    }
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuineaPigAddProfileComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, FormsModule, HttpClientTestingModule],
      providers: [{ provide: ToastrService, useClass: MockToastrService }],
    });
    fixture = TestBed.createComponent(GuineaPigAddProfileComponent);
    component = fixture.componentInstance;
    toastrService = TestBed.inject(ToastrService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
