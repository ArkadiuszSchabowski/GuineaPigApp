import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { GuineaPigDto } from 'src/app/_models/guinea-pig-dto';
import { RemoveGuineaPigDto } from 'src/app/_models/remove-guinea-pig-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-guinea-pig-remove-profile',
  templateUrl: './guinea-pig-remove-profile.component.html',
  styleUrls: ['./guinea-pig-remove-profile.component.css']
})
export class GuineaPigRemoveProfileComponent extends BaseComponent implements OnInit{

  override cloudText: string = "Pamiętaj, że tej akcji nie można cofnąć!"

  model: GuineaPigDto = new GuineaPigDto();
  pigs: string[] = [];
  email: string = "";
  guineaPigs: GuineaPigDto[] = [];
  selectedPig: RemoveGuineaPigDto = new RemoveGuineaPigDto();

  constructor(guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    private tokenService: TokenService,
    private toastr: ToastrService
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
  getGuineaPigs(){
    this.guineaPigService.getGuineaPigs(this.email).subscribe({
      next: (response: GuineaPigDto[]) => {
        console.log(response)
        this.guineaPigs = response;
      },
      error: error => console.log(error)
    })
  }
  removeGuineaPigProfile() {
    
    this.selectedPig.email = this.tokenService.getEmailFromToken();


    this.guineaPigService.removeGuineaPig(this.selectedPig).pipe(
      finalize(() => {

      })
    ).subscribe({
      next: () => {
        this.getGuineaPigs();
        this.toastr.success("Profil świnki został usunięty!");
      },
      error: () => {
         {
          this.toastr.error("Nie wybrano profilu świnki!");
        }
      }
    })
  }
}
