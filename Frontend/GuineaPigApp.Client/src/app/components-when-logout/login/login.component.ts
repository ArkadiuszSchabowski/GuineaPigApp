import { Component, OnInit} from '@angular/core';
import { GuineaPigService } from '../../_service/guinea-pig.service';
import { AccountService } from '../../_service/account.service';
import { Router } from '@angular/router';
import { BaseComponent } from 'src/app/_shared/base.component';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { LoginUserDto } from 'src/app/_models/login-user-dto';
import { finalize } from 'rxjs';
import { ValidateService } from 'src/app/_service/validate.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent implements OnInit {

  override cloudText: string = 'Mam nadzieję, że masz już konto!';
  
  backgroundUrl: string = "assets/images/backgrounds/no-login/login.jpg"
  isCorrectEmail: boolean = false;
  isCorrectPassword: boolean = false;

  hidePassword: boolean = true;
  model: LoginUserDto = new LoginUserDto();

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    public accountService: AccountService,
    private router: Router,
    private validateService: ValidateService,
    private toastr: ToastrService
  ) {
    super(guineaPigService);
  }
  override ngOnInit(): void {
    super.ngOnInit();
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }
  validateModel(){
    this.validateLogin();
    if(this.isCorrectEmail){
      this.validatePassword();
    }
    if(this.isCorrectEmail && this.isCorrectPassword){
      this.login();
    }
  }
  validateLogin(){
    this.isCorrectEmail = this.validateService.validateEmail(this.model.email);
  }
  validatePassword(){
    this.isCorrectPassword = this.validateService.validatePasswordLogin(this.model.password);
    if(!this.isCorrectPassword){
      this.model.password = "";
    }
  }

  login(){
    this.accountService.login(this.model)
    .pipe(
      finalize(() => {
        this.model = new LoginUserDto();
      })
    )
    .subscribe({
      next: () => {
        this.router.navigateByUrl("/");
      },
      error: error => {
        console.log(error);
        if(error.status !== 0){
          this.toastr.error(error.error)
        }
      }
    })
  };

  changePasswordVisibility() {
    this.hidePassword = !this.hidePassword;
  };
}