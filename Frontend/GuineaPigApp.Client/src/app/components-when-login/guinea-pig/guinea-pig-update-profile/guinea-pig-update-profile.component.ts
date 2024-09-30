import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { GuineaPigDto } from 'src/app/_models/guinea-pig-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { ValidateService } from 'src/app/_service/validate.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-guinea-pig-update-profile',
  templateUrl: './guinea-pig-update-profile.component.html',
  styleUrls: ['./guinea-pig-update-profile.component.css'],
})
export class GuineaPigUpdateProfileComponent
  extends BaseComponent
  implements OnInit
{
  override cloudText: string = 'Ej! Przecież moja waga jest dobra!';

  email = '';
  model: GuineaPigDto = new GuineaPigDto();
  guineaPigs: GuineaPigDto[] = [];
  selectedPig: GuineaPigDto | null = null;
  weightGuineaPig: boolean = false;

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    private tokenService: TokenService,
    private toastr: ToastrService,
    private validateService: ValidateService
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getEmailFromToken();
    this.getGuineaPigs();
  }
  getEmailFromToken() {
    this.email = this.tokenService.getEmailFromToken();
  }
  getGuineaPigs() {
    this.guineaPigService.getGuineaPigs(this.email).subscribe({
      next: (response: GuineaPigDto[]) => {
        this.guineaPigs = response;
      },
      error: () => {},
    });
  }
  updateGuineaPigProfile(selectedPig: GuineaPigDto | null) {
    if (selectedPig === null) {
      this.toastr.error("Nie wybrałeś świnki do aktualizacji!");
      return;
    }
    if(this.model.weight === null){
      this.toastr.error("Nie wpisałeś nowej wagi!");
      return;
    }

    if (this.model.weight !== null) {
      this.weightGuineaPig = this.validateService.validateWeightGuineaPig(
        this.model.weight
      );
      if(this.selectedPig){
        this.selectedPig.weight = this.model.weight;
      }
    }

    if (this.weightGuineaPig) {
      this.guineaPigService
        .updateWeight(this.email, selectedPig)
        .pipe(
          finalize(() => {
            console.log('finalize');
            this.model = new GuineaPigDto();
            this.selectedPig = null;
          })
        )
        .subscribe({
          next: () =>
            this.toastr.success('Waga świnki została zaaktualizowana'),
          error: (error) => {
            if (error.error.errors) {
              this.toastr.error('Wprowadzono niepopawne dane!');
            }
          },
        });
    }
  }
}
