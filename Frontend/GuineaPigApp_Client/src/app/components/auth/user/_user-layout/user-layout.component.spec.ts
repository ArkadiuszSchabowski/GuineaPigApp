import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserLayoutComponent } from './user-layout.component';
import { RouterOutlet } from '@angular/router';

describe('UserLayoutComponent', () => {
  let component: UserLayoutComponent;
  let fixture: ComponentFixture<UserLayoutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserLayoutComponent],
      imports: [RouterOutlet]
    });
    fixture = TestBed.createComponent(UserLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
