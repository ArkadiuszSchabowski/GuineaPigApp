import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../_service/theme.service';
import { AccountService } from '../../_service/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {

  currentTheme: boolean | undefined = undefined;

  constructor(private theme: ThemeService, public accountService: AccountService) {}

  ngOnInit(): void {

    this.setTheme();

  }

  setTheme() {
    this.theme.isLightTheme$.subscribe({
      next: (response) => {
        this.currentTheme = response;
      },
      error: () => {}
    });
  }

  changeTheme(): void {
    this.currentTheme = this.theme.themeSubject.value;
    this.theme.themeSubject.next(!this.currentTheme);
  }
  logout(){
    this.accountService.logout();
  }
}
