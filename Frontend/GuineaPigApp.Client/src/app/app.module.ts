import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { PolishPaginatorIntl } from './_internationalization/polish-paginator-intl';
import { AppComponent } from './app.component';

import { BadProductsComponent } from './components-when-logout/bad-products/bad-products.component';
import { GoodProductsComponent } from './components-when-logout/good-products/good-products.component';
import { LoginComponent } from './components-when-logout/login/login.component';
import { MainPageComponent } from './components-when-logout/main-page/main-page.component';
import { RegisterComponent } from './components-when-logout/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { GuineaPigLayoutComponent } from './components-when-login/guinea-pig/_guinea-pig-layout/guinea-pig-layout.component';
import { GuineaPigAddProfileComponent } from './components-when-login/guinea-pig/guinea-pig-add-profile/guinea-pig-add-profile.component';
import { GuineaPigProfileComponent } from './components-when-login/guinea-pig/guinea-pig-profile/guinea-pig-profile.component';
import { GuineaPigRemoveProfileComponent } from './components-when-login/guinea-pig/guinea-pig-remove-profile/guinea-pig-remove-profile.component';
import { GuineaPigUpdateProfileComponent } from './components-when-login/guinea-pig/guinea-pig-update-profile/guinea-pig-update-profile.component';
import { UserLayoutComponent } from './components-when-login/user/_user-layout/user-layout.component';
import { UserChangePasswordComponent } from './components-when-login/user/user-change-password/user-change-password.component';
import { UserEditProfileComponent } from './components-when-login/user/user-edit-profile/user-edit-profile.component';
import { UserProfileComponent } from './components-when-login/user/user-profile/user-profile.component';
import { NavbarComponent } from './components-common/navbar/navbar.component';
import { FooterNavigationComponent } from './components-common/footer-navigation/footer-navigation.component';
import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptor } from './_inteceptors/error.interceptor';
import { UserRemoveProfileComponent } from './components-when-login/user/user-remove-profile/user-remove-profile.component';
import { GuineaPigCheckWeightsComponent } from './components-when-login/guinea-pig/guinea-pig-check-weights/guinea-pig-check-weights.component';
import { InfoComponent } from './components-when-logout/info/info.component';
import { AngularMaterialModule } from './_modules/angular-material/angular-material.module';
import { MatPaginatorIntl } from '@angular/material/paginator';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    RegisterComponent,
    LoginComponent,
    InfoComponent,
    GoodProductsComponent,
    BadProductsComponent,
    NavbarComponent,
    FooterNavigationComponent,
    GuineaPigProfileComponent,
    UserProfileComponent,
    UserChangePasswordComponent,
    UserEditProfileComponent,
    UserRemoveProfileComponent,
    GuineaPigAddProfileComponent,
    GuineaPigUpdateProfileComponent,
    GuineaPigRemoveProfileComponent,
    UserLayoutComponent,
    GuineaPigLayoutComponent,
    GuineaPigCheckWeightsComponent,
  ],
  imports: [
    BrowserModule,
     HttpClientModule, 
     BrowserAnimationsModule,
     AppRoutingModule,
     FormsModule,
     AngularMaterialModule,
     ToastrModule.forRoot({ positionClass: 'toast-bottom-right' })
  ],
  providers: [
    {provide: MatPaginatorIntl, useClass: PolishPaginatorIntl},
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
