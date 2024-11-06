import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/_models/user-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { UserService } from 'src/app/_service/user.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent extends BaseComponent implements OnInit {
  override cloudText: string = 'Witaj na swoim profilu!';

  email: string = "";
  model: UserDto | undefined = undefined;

  constructor(
    guineaPigService: GuineaPigService,
    public userService: UserService,
    public themeHelper: ThemeHelper,
    private tokenService: TokenService
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getUserInformation();
  }

  getUserInformation() {
    
    this.email = this.tokenService.getEmailFromToken();

    this.userService.getUserInformation(this.email).subscribe({
      next: (response) => {
        this.model = response;
      },
      error: () => {}
    });
  }
}
