import { Component, OnInit } from '@angular/core';
import { GuineaPigService } from '../../../_services/guinea-pig.service';
import { BaseComponent } from 'src/app/shared/base.component';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss'],
})
export class InfoComponent extends BaseComponent implements OnInit {

  backgroundUrl: string = "assets/images/backgrounds/no-login/beforeBuyGuineaPig.jpg"
  override cloudText: string = 'Zapewnij mi proszę odpowiednie warunki do rozwoju!';

  constructor(guineaPigService: GuineaPigService, private themeHelper: ThemeHelper) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();

    this.themeHelper.setBackground(this.backgroundUrl);
  }
}
