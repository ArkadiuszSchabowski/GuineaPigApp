import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../../_services/theme.service';
import { GuineaPigService } from '../../../_services/guinea-pig.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterNavigationComponent implements OnInit{

  currentTheme: boolean | undefined = undefined;
  componentText: string ="";
  guineaPigUrl: string = "assets/images/guineapig.png"

  constructor(private theme: ThemeService, private guineaPigService: GuineaPigService) {

  }
  ngOnInit(): void {

    this.setTheme();
    this.setComponentCloudText();

  }

  setComponentCloudText() {
    this.guineaPigService.isTextSubject$.subscribe({
      next: response => this.componentText = response,
      error: () => {}
    });
  }

  setTheme() {
    this.theme.isLightTheme$.subscribe({
      next: response => {
        this.currentTheme = response
      },
      error: () => {}
    });
  }
  
  playGuineaPigSound(){
    var sound = new Audio();
    sound.src = "../assets/sounds/sound.mp3";
    sound.load();
    sound.play();
  }
}
