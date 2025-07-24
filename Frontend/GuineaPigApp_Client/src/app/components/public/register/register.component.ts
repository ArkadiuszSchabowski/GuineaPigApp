import { Component, OnInit, signal, ViewChild } from '@angular/core';
import { GuineaPigService } from '../../../_services/guinea-pig.service';
import { AccountService } from '../../../_services/account.service';
import { BaseComponent } from 'src/app/shared/base.component';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterStepOneDto } from 'src/app/models/add/register-step-one-dto';
import { MatStepper } from '@angular/material/stepper';
import { RegisterUserDto } from 'src/app/models/add/register-user-dto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent extends BaseComponent implements OnInit {
  @ViewChild('stepper') stepper!: MatStepper;
  backgroundUrl: string = 'assets/images/backgrounds/no-login/register.jpg';
  override cloudText: string = 'Stwórz konto i odblokuj wszystkie funkcje!';
  validationModelErrors: string[] = [];
  conflictError: string = '';

  form = new FormGroup({
    loginForm: new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(25),
      ]),
      repeatPassword: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(25),
      ]),
    }),
    personalForm: new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(25),
      ]),
      surname: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(25),
      ]),
      city: new FormControl('', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(25),
      ]),
    }),
  });

  passwordHiddenSignal = signal(true);
  repeatPasswordHiddenSignal = signal(true);

    changePasswordVisibility(event: MouseEvent) {
    this.passwordHiddenSignal.set(!this.passwordHiddenSignal());
    event.stopPropagation();
  }

    changeRepeatPasswordVisibility(event: MouseEvent) {
    this.repeatPasswordHiddenSignal.set(!this.repeatPasswordHiddenSignal());
    event.stopPropagation();
  }

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    private accountService: AccountService
  ) {
    super(guineaPigService);
  }
  override ngOnInit(): void {
    super.ngOnInit();
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  get loginForm() {
    return this.form.get('loginForm') as FormGroup;
  }

  get personalForm() {
    return this.form.get('personalForm') as FormGroup;
  }

  validateLoginForm() {
    this.validationModelErrors = [];
    this.conflictError = '';

    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    let credentials: RegisterStepOneDto = {
      email: this.loginForm.get('email')!.value,
      password: this.loginForm.get('password')!.value,
      repeatPassword: this.loginForm.get('repeatPassword')!.value,
    };

    this.accountService.validateRegisterStepOne(credentials).subscribe({
      next: () => {
        this.stepper.next();
      },
      error: (error) => {
        if (error.status === 409) {
          this.conflictError = error.error;
          return;
        }
        this.validationModelErrors = error;
      },
    });
  }

  validateForm() {
    if (this.personalForm.invalid) {
      this.personalForm.markAllAsTouched();
      return;
    }

    let credentials: RegisterUserDto = {
      email: this.loginForm.get('email')!.value,
      password: this.loginForm.get('password')!.value,
      repeatPassword: this.loginForm.get('repeatPassword')!.value,
      name: this.personalForm.get('name')!.value,
      surname: this.personalForm.get('surname')!.value,
      city: this.personalForm.get('city')!.value,
    };
    this.accountService.register(credentials).subscribe({
      next: () => {
        this.stepper.next();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
