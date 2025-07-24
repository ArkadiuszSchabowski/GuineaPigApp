import { Component, OnInit, signal } from '@angular/core';
import { GuineaPigService } from '../../../_services/guinea-pig.service';
import { AccountService } from '../../../_services/account.service';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/shared/base.component';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { LoginUserDto } from 'src/app/models/add/login-user-dto';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent implements OnInit {
  override cloudText: string = 'Mam nadzieję, że masz już konto!';
  backgroundUrl: string = 'assets/images/backgrounds/no-login/login.jpg';
  validationServerErrors: string[] = [];

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  passwordHiddenSignal = signal(true);

  constructor(
    guineaPigService: GuineaPigService,
    private accountService: AccountService,
    private router: Router,
    public themeHelper: ThemeHelper
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  changePasswordVisibility(event: MouseEvent) {
    this.passwordHiddenSignal.set(!this.passwordHiddenSignal());
    event.stopPropagation();
  }

  login() {
    this.validationServerErrors = [];

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    let credentials: LoginUserDto = {
      email: this.form.get('email')!.value,
      password: this.form.get('password')!.value,
    };

    this.accountService.login(credentials).subscribe({
      next: () => {
        this.router.navigateByUrl('/');
      },
      error: (error) => {
        this.validationServerErrors = error;
      },
    });
  }
}
