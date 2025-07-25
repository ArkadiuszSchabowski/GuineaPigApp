import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoodProductsComponent } from './good-products.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/modules/angular-material/material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GoodProductsComponent', () => {
  let component: GoodProductsComponent;
  let fixture: ComponentFixture<GoodProductsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GoodProductsComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(GoodProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
