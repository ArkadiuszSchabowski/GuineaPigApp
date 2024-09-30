import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { UpdateUserDto } from 'src/app/_models/update-user-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { UserService } from 'src/app/_service/user.service';
import { ValidateService } from 'src/app/_service/validate.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-user-edit-profile',
  templateUrl: './user-edit-profile.component.html',
  styleUrls: ['./user-edit-profile.component.css'],
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

    this.model.email = this.tokenService.getEmailFromToken();

    this.isPersonalInformation = this.validateService.validatePersonalInformation(this.model);
    this.updateUser(this.model);
  }

  updateUser(model: UpdateUserDto) {

    if(this.isPersonalInformation){
      this.userService.updateProfileInformation(model).subscribe({
        next: () => {
          this.toastr.success("Twoje dane zostały zaaktualizowane!")
          this.router.navigateByUrl("/user/profile");
        },
        error: () => {}
      });
    }
  }
}
