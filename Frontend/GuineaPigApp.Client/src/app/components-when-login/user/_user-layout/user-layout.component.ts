import { Component } from '@angular/core';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.css'],
})
export class UserLayoutComponent {
  backgroundUrl: string = 'assets/images/backgrounds/userLayout.jpg';

  constructor(public themeHelper: ThemeHelper) {}
  ngOnInit(): void {
    this.themeHelper.setBackground(this.backgroundUrl);
    this.themeHelper.setTheme();
  }
}
