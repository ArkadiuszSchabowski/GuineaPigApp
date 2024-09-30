import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { GuineaPigDto } from 'src/app/_models/guinea-pig-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { ValidateService } from 'src/app/_service/validate.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-guinea-pig-add-profile',
  templateUrl: './guinea-pig-add-profile.component.html',
  styleUrls: ['./guinea-pig-add-profile.component.css'],
})
export class GuineaPigAddProfileComponent
  extends BaseComponent
  implements OnInit
{
  override cloudText: string = 'Dodajesz nowego przyjaciela? Super!';
  model: GuineaPigDto = new GuineaPigDto();
  email: string = '';
  guineaPigName: boolean = false;
  guineaPigWeight: boolean = false;

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    private toastr: ToastrService,
    private tokenService: TokenService,
    private validateService: ValidateService
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
  }

  addGuineaPigProfile() {
    this.email = this.tokenService.getEmailFromToken();

    this.guineaPigName = this.validateService.validateName(this.model.name);

    if (this.guineaPigName) {
      if (this.model.weight === null) {
        this.toastr.error('Nie wpisałeś wagi świnki!');
        return;
      }
      if (this.model.weight !== null) {
        this.guineaPigWeight = this.validateService.validateWeightGuineaPig(
          this.model.weight
        );
      }
    }

    if (this.guineaPigWeight && this.guineaPigName) {
      this.guineaPigService
        .addGuineaPig(this.email, this.model)
        .pipe(
          finalize(() => {
            this.model = new GuineaPigDto();
          })
        )
        .subscribe({
          next: () => {
            this.toastr.success('Profil świnki morskiej został dodany!');
          },
          error: (error) => {
            this.toastr.error(error.error);
          },
        });
    }
  }
}
