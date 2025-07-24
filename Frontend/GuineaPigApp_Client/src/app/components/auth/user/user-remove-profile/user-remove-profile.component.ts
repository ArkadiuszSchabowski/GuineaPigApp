import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { UpdateUserDto } from 'src/app/models/add/update-user-dto';
import { AccountService } from 'src/app/_services/account.service';
import { GuineaPigService } from 'src/app/_services/guinea-pig.service';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { TokenService } from 'src/app/_services/token.service';
import { UserService } from 'src/app/_services/user.service';
import { BaseComponent } from 'src/app/shared/base.component';

@Component({
  selector: 'app-user-remove-profile',
  templateUrl: './user-remove-profile.component.html',
  styleUrls: ['./user-remove-profile.component.scss']
})
export class UserRemoveProfileComponent extends BaseComponent implements OnInit{
  override cloudText: string =
    'Pamiętaj, że tej akcji nie można cofnąć!';

  email: string ="";

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    public userService: UserService,
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router,
    private tokenService: TokenService
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
  }

  removeProfile(){
    this.email = this.tokenService.getEmailFromToken();

    this.accountService.removeProfile(this.email).subscribe({
      next: () => 
        {
          this.accountService.logout();
          this.toastr.success("Twoje konto zostało usunięte!");
          this.router.navigateByUrl("/");
        },
        error: error => {
          if(error.status === 403){
            this.toastr.error(error.error);
          }
        }
    })
  }
}
