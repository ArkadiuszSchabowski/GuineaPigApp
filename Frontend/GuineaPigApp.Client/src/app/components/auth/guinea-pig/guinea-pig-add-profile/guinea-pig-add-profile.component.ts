import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { GuineaPigDto } from 'src/app/models/guinea-pig-dto';
import { GuineaPigService } from 'src/app/_services/guinea-pig.service';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { TokenService } from 'src/app/_services/token.service';
import { ValidateService } from 'src/app/_services/validate.service';
import { BaseComponent } from 'src/app/shared/base.component';

@Component({
  selector: 'app-guinea-pig-add-profile',
  templateUrl: './guinea-pig-add-profile.component.html',
  styleUrls: ['./guinea-pig-add-profile.component.scss'],
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

    this.guineaPigWeight = this.validateService.validateWeightGuineaPig(this.model.weight);
    this.guineaPigName = this.validateService.validateName(this.model.name);

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
            if (error.error.errors) {
              this.toastr.error('Wprowadzono niepopawne dane!');
          }
            if(error.status === 409){
              this.toastr.error(error.error);
            }
        }});
    }
  }
}
