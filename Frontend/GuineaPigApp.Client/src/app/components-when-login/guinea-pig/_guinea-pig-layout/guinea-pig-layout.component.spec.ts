import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuineaPigLayoutComponent } from './guinea-pig-layout.component';
import { RouterOutlet } from '@angular/router';

describe('GuineaPigLayoutComponent', () => {
  let component: GuineaPigLayoutComponent;
  let fixture: ComponentFixture<GuineaPigLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuineaPigLayoutComponent],
      imports: [RouterOutlet]
    });
    fixture = TestBed.createComponent(GuineaPigLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
