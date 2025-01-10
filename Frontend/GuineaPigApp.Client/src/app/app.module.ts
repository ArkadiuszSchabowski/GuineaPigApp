import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AngularMaterialModule } from './_modules/angular-material/angular-material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorInterceptor } from './_inteceptors/error.interceptor';
import { FooterNavigationComponent } from './components-common/footer-navigation/footer-navigation.component';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { NavbarComponent } from './components-common/navbar/navbar.component';
import { NgModule } from '@angular/core';
import { PolishPaginatorIntl } from './_internationalization/polish-paginator-intl';
import { ToastrModule } from 'ngx-toastr';

import { UserModule } from './_modules/user/user.module';
import { GuineaPigModule } from './_modules/guinea-pig/guinea-pig.module';
import { PublicModule } from './_modules/public/public.module';

@NgModule({
  declarations: [
    AppComponent,
    FooterNavigationComponent,
    NavbarComponent,
  ],
  imports: [
    AngularMaterialModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    GuineaPigModule,
    HttpClientModule,
    UserModule,
    PublicModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
  ],
  providers: [
    { provide: MatPaginatorIntl, useClass: PolishPaginatorIntl },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}