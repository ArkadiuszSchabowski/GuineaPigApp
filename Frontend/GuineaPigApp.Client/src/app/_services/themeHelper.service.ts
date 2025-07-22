import { Injectable, OnInit } from '@angular/core';
import { ThemeService } from './theme.service';

@Injectable({
  providedIn: 'root',
})
export class ThemeHelper {
  currentTheme: boolean | undefined = undefined;
  images: string[] = [];

  constructor(public theme: ThemeService) {}

  preloadBackgrounds() {
    this.images = [
      'assets/images/backgrounds/no-login/mainPage.jpg',
      'assets/images/backgrounds/no-login/badProducts.jpg',
      'assets/images/backgrounds/no-login/beforeBuyGuineaPig.jpg',
      'assets/images/backgrounds/no-login/goodProducts.jpg',
      'assets/images/backgrounds/no-login/guinea-pig.jpg',
      'assets/images/backgrounds/no-login/login.jpg',
      'assets/images/backgrounds/no-login/register.jpg',
      'assets/images/backgrounds/guineaPigLayout.jpg',
      'assets/images/backgrounds/userLayout.jpg',
    ];

    this.images.forEach((src) => {
      const img = new Image();
      img.src = src;
    });
  }

  setTheme(): void {
    this.theme.isLightTheme$.subscribe({
      next: (response) => {
        this.currentTheme = response;
      },
      error: () => {}
    });
  }
  setBackground(url: string) {
    document.body.style.backgroundImage = `url(${url})`;
    document.body.style.minHeight = '100%';
    document.body.style.backgroundSize = 'cover';
    document.body.style.backgroundRepeat = 'no-repeat';
    document.body.style.backgroundAttachment = 'fixed';
    document.body.style.backgroundPosition = 'center';
    document.body.style.transition = 'background-image 0.6s ease-in-out';
  }
}
