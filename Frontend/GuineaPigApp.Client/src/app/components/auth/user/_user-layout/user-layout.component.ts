import { Component } from '@angular/core';
import { ThemeHelper } from 'src/app/_services/themeHelper.service';

@Component({
  selector: 'app-user-layout',
  templateUrl: './user-layout.component.html',
  styleUrls: ['./user-layout.component.scss'],
})
export class UserLayoutComponent {
  backgroundUrl: string = 'assets/images/backgrounds/userLayout.jpg';

  constructor(public themeHelper: ThemeHelper) {}
  ngOnInit(): void {
    this.themeHelper.setBackground(this.backgroundUrl);
    this.themeHelper.setTheme();
  }
}
