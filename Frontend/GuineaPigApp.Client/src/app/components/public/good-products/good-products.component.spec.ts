import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoodProductsComponent } from './good-products.component';
import { HttpClientModule } from '@angular/common/http';
import { AngularMaterialModule } from 'src/app/_modules/angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('GoodProductsComponent', () => {
  let component: GoodProductsComponent;
  let fixture: ComponentFixture<GoodProductsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GoodProductsComponent],
      imports: [AngularMaterialModule, BrowserAnimationsModule, HttpClientModule]
    });
    fixture = TestBed.createComponent(GoodProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
