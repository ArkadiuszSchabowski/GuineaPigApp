import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from 'src/app/components-common/navbar/navbar.component';
import { FooterNavigationComponent } from 'src/app/components-common/footer-navigation/footer-navigation.component';
import { AppRoutingModule } from 'src/app/app-routing.module';

@NgModule({
  declarations: [NavbarComponent, FooterNavigationComponent],
  imports: [
    CommonModule, AngularMaterialModule, FormsModule, AppRoutingModule
  ],
  exports: [
    NavbarComponent, FooterNavigationComponent
  ]
})
export class SharedModule { }