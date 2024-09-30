import { Component, OnInit } from '@angular/core';
import { GuineaPigService } from '../../_service/guinea-pig.service';
import { BaseComponent } from 'src/app/_shared/base.component';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent extends BaseComponent implements OnInit{

  backgroundUrl: string ="assets/images/backgrounds/no-login/mainPage.jpg"
  override cloudText: string = "Cześć! Zaprzyjaźnimy się?"
  currentTheme: boolean | undefined = undefined;

  constructor(guineaPigService: GuineaPigService, private themeHelper: ThemeHelper){
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.themeHelper.setBackground(this.backgroundUrl);
  }
}
