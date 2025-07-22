import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { UpdateUserDto } from 'src/app/models/update-user-dto';
import { GuineaPigService } from 'src/app/_services/guinea-pig.service';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { TokenService } from 'src/app/_services/token.service';
import { UserService } from 'src/app/_services/user.service';
import { ValidateService } from 'src/app/_services/validate.service';
import { BaseComponent } from 'src/app/shared/base.component';

@Component({
  selector: 'app-user-edit-profile',
  templateUrl: './user-edit-profile.component.html',
  styleUrls: ['./user-edit-profile.component.scss'],
})
export class UserEditProfileComponent extends BaseComponent implements OnInit {
  override cloudText: string =
    'Edytuj swoje informacje i bądź zawsze na bieżąco!';

  model: UpdateUserDto = new UpdateUserDto();
  email: string ="";
  isPersonalInformation: boolean = false;

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    public userService: UserService,
    private validateService: ValidateService,
    private toastr: ToastrService,
    private router: Router,
    private tokenService: TokenService
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
  }


  checkPersonalInformation(){

    this.email = this.tokenService.getEmailFromToken();

    this.isPersonalInformation = this.validateService.validatePersonalInformation(this.model);
    this.updateUser(this.email, this.model);
  }

  updateUser(email: string, model: UpdateUserDto) {

    if(this.isPersonalInformation){
      this.userService.updateProfileInformation(email, model).subscribe({
        next: () => {
          this.toastr.success("Twoje dane zostały zaaktualizowane!")
          this.router.navigateByUrl("/user/profile");
        },
        error: () => {}
      });
    }
  }
}
