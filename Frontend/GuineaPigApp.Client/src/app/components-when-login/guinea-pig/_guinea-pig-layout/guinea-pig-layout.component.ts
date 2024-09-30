import { Component, OnInit } from '@angular/core';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';

@Component({
  selector: 'app-guinea-pig-layout',
  templateUrl: './guinea-pig-layout.component.html',
  styleUrls: ['./guinea-pig-layout.component.css']
})
export class GuineaPigLayoutComponent implements OnInit{

    backgroundUrl: string = "assets/images/backgrounds/guineaPigLayout.jpg"

constructor(public themeHelper: ThemeHelper){
}
  ngOnInit(): void {
    this.themeHelper.setBackground(this.backgroundUrl);
    this.themeHelper.setTheme();
  }
}
