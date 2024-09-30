import { Component, OnInit } from '@angular/core';
import { ThemeService } from './_service/theme.service';
import { ThemeHelper } from './_service/themeHelper.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  currentTheme: boolean | undefined = undefined;

  constructor(
    private themeService: ThemeService,
    private themeHelper: ThemeHelper
  ) {}

  ngOnInit() {
    this.setTheme();
    this.themeHelper.preloadBackgrounds();
  }
  setTheme() {
    this.themeService.isLightTheme$.subscribe({
      next: (response) => {
        this.currentTheme = response;
      },
      error: () => {},
    });
  }
}
