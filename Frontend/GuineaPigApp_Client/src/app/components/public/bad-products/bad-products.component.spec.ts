import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BadProductsComponent } from './bad-products.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';
import { HttpClientTestingModule} from '@angular/common/http/testing';

describe('BadProductsComponent', () => {
  let component: BadProductsComponent;
  let fixture: ComponentFixture<BadProductsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BadProductsComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(BadProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
