import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserChangePasswordComponent } from 'src/app/components/auth/user/user-change-password/user-change-password.component';
import { UserEditProfileComponent } from 'src/app/components/auth/user/user-edit-profile/user-edit-profile.component';
import { UserLayoutComponent } from 'src/app/components/auth/user/_user-layout/user-layout.component';
import { UserProfileComponent } from 'src/app/components/auth/user/user-profile/user-profile.component';
import { UserRemoveProfileComponent } from 'src/app/components/auth/user/user-remove-profile/user-remove-profile.component';
import { FormsModule } from '@angular/forms';
import { AngularMaterialModule } from '../angular-material/angular-material.module';
import { AppRoutingModule } from 'src/app/app-routing.module';

@NgModule({
  declarations: [
    UserChangePasswordComponent,
    UserEditProfileComponent,
    UserLayoutComponent,
    UserProfileComponent,
    UserRemoveProfileComponent,
  ],
  imports: [CommonModule, FormsModule, AngularMaterialModule, AppRoutingModule],
  exports: [
    UserChangePasswordComponent,
    UserEditProfileComponent,
    UserLayoutComponent,
    UserProfileComponent,
    UserRemoveProfileComponent,
  ],
})
export class UserModule {}