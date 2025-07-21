import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigAddProfileComponent } from './guinea-pig-add-profile.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { AngularMaterialModule } from 'src/app/_modules/angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
      imports: [AngularMaterialModule, BrowserAnimationsModule, FormsModule, HttpClientModule],
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
