import { Component, OnInit } from '@angular/core';
import { GuineaPigService } from '../../_service/guinea-pig.service';
import { BaseComponent } from 'src/app/_shared/base.component';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';

@Component({
  selector: 'app-before-buy-guinea-pig',
  templateUrl: './before-buy-guinea-pig.component.html',
  styleUrls: ['./before-buy-guinea-pig.component.scss'],
})
export class BeforeBuyGuineaPigComponent extends BaseComponent implements OnInit {

  backgroundUrl: string = "assets/images/backgrounds/no-login/beforeBuyGuineaPig.jpg"
  override cloudText: string = 'Zapewnij mi proszę odpowiednie warunki do rozwoju!';
  information: any;

  constructor(guineaPigService: GuineaPigService, private themeHelper: ThemeHelper) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getInformationGuineaPigs();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  getInformationGuineaPigs() {
    this.guineaPigService.getInformationGuineaPigs().subscribe({
      next: (response) => {
        this.information = response;
      },
      error: () => {}
    });
  }
}
