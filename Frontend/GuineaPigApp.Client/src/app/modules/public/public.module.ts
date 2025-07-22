import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BadProductsComponent } from 'src/app/components/public/bad-products/bad-products.component';
import { GoodProductsComponent } from 'src/app/components/public/good-products/good-products.component';
import { InfoComponent } from 'src/app/components/public/info/info.component';
import { LoginComponent } from 'src/app/components/public/login/login.component';
import { MainPageComponent } from 'src/app/components/public/main-page/main-page.component';
import { RegisterComponent } from 'src/app/components/public/register/register.component';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from 'src/app/app-routing.module';

@NgModule({
  declarations: [
    BadProductsComponent,
    GoodProductsComponent,
    InfoComponent,
    LoginComponent,
    MainPageComponent,
    RegisterComponent,
  ],
  imports: [CommonModule, AngularMaterialModule, FormsModule, AppRoutingModule],
  exports: [
    BadProductsComponent,
    GoodProductsComponent,
    InfoComponent,
    LoginComponent,
    MainPageComponent,
    RegisterComponent,
  ],
})
export class PublicModule {}