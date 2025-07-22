import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FooterNavigationComponent } from './footer.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('FooterNavigationComponent', () => {
  let component: FooterNavigationComponent;
  let fixture: ComponentFixture<FooterNavigationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FooterNavigationComponent],
      imports: [HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(FooterNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
