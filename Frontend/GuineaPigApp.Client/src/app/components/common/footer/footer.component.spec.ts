import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FooterNavigationComponent } from './footer.component';
import { HttpClientModule } from '@angular/common/http';

describe('FooterNavigationComponent', () => {
  let component: FooterNavigationComponent;
  let fixture: ComponentFixture<FooterNavigationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FooterNavigationComponent],
      imports: [HttpClientModule]
    });
    fixture = TestBed.createComponent(FooterNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
