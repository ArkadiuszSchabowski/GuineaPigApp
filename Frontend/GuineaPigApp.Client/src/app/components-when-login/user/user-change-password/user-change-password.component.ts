import { Component, OnInit } from '@angular/core';
import { ChangePasswordDto } from 'src/app/_models/change-password-dto';
import { AccountService } from 'src/app/_service/account.service';
import { jwtDecode } from 'jwt-decode';
import { BaseComponent } from 'src/app/_shared/base.component';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { finalize } from 'rxjs';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/_service/token.service';

@Component({
  selector: 'app-user-change-password',
  templateUrl: './user-change-password.component.html',
  styleUrls: ['./user-change-password.component.css'],
})
export class UserChangePasswordComponent
  extends BaseComponent
  implements OnInit
{
  override cloudText: string = 'Super, że dbasz o swoje bezpieczeństwo!';

  model: ChangePasswordDto = new ChangePasswordDto();
  token: any;
  email: string = '';

  hidePassword1: boolean = true;
  hidePassword2: boolean = true;
  hidePassword3: boolean = true;

  constructor(
    guineaPigService: GuineaPigService,
    private accountService: AccountService,
    public themeHelper: ThemeHelper,
    private toastr: ToastrService,
    private router: Router,
    private tokenService: TokenService
  ) {
    super(guineaPigService);
  }
  override ngOnInit(): void {
    super.ngOnInit();
  }

  changePassword(model: ChangePasswordDto) {

    model.email = this.tokenService.getEmailFromToken();

    console.log(model);

    this.accountService
      .changePassword(this.model)
      .pipe(
        finalize(() => {
          this.resetForm();
        })
      )
      .subscribe({
        next: () => {
          this.toastr.success("Twoje hasło zostało zmienione!")
          this.router.navigateByUrl("/user/profile")
        },
        error: (error) => this.toastr.error(error.error)
      });
  }
  resetForm() {
    (this.model.currentPassword = ''),
      (this.model.newPassword = ''),
      (this.model.repeatNewPassword = '');
  }
  changePasswordVisibility(
    field: 'hidePassword1' | 'hidePassword2' | 'hidePassword3'
  ) {
    this[field] = !this[field];
  }
}
