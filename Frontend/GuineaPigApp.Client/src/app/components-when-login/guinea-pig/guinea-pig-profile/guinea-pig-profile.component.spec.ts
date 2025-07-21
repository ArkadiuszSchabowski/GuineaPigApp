import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigProfileComponent } from './guinea-pig-profile.component';
import { HttpClientModule } from '@angular/common/http';

describe('GuineaPigProfileComponent', () => {
  let component: GuineaPigProfileComponent;
  let fixture: ComponentFixture<GuineaPigProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuineaPigProfileComponent],
      imports: [HttpClientModule]
    });
    fixture = TestBed.createComponent(GuineaPigProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
