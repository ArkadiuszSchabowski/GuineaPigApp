import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserChangePasswordComponent } from 'src/app/components-when-login/user/user-change-password/user-change-password.component';
import { UserEditProfileComponent } from 'src/app/components-when-login/user/user-edit-profile/user-edit-profile.component';
import { UserLayoutComponent } from 'src/app/components-when-login/user/_user-layout/user-layout.component';
import { UserProfileComponent } from 'src/app/components-when-login/user/user-profile/user-profile.component';
import { UserRemoveProfileComponent } from 'src/app/components-when-login/user/user-remove-profile/user-remove-profile.component';

@NgModule({
  declarations: [
    UserChangePasswordComponent,
    UserEditProfileComponent,
    UserLayoutComponent,
    UserProfileComponent,
    UserRemoveProfileComponent,
  ],
  imports: [CommonModule],
  exports: [
    UserChangePasswordComponent,
    UserEditProfileComponent,
    UserLayoutComponent,
    UserProfileComponent,
    UserRemoveProfileComponent,
  ],
})
export class UserModule {}
