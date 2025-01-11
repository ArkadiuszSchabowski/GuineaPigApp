import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptor } from './_inteceptors/error.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { PolishPaginatorIntl } from './_internationalization/polish-paginator-intl';
import { ToastrModule } from 'ngx-toastr';

import { UserModule } from './_modules/user/user.module';
import { GuineaPigModule } from './_modules/guinea-pig/guinea-pig.module';
import { PublicModule } from './_modules/public/public.module';
import { SharedModule } from './_modules/shared/shared.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    GuineaPigModule,
    UserModule,
    PublicModule,
    SharedModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
  ],
  providers: [
    { provide: MatPaginatorIntl, useClass: PolishPaginatorIntl },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}