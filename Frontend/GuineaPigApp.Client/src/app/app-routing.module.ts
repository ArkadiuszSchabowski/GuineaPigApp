import { NgModule } from '@angular/core';
import { MainPageComponent } from './components-when-logout/main-page/main-page.component';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components-when-logout/register/register.component';
import { LoginComponent } from './components-when-logout/login/login.component';
import { GoodProductsComponent } from './components-when-logout/good-products/good-products.component';
import { BadProductsComponent } from './components-when-logout/bad-products/bad-products.component';
import { BeforeBuyGuineaPigComponent } from './components-when-logout/before-buy-guinea-pig/before-buy-guinea-pig.component';
import { GuineaPigLayoutComponent } from './components-when-login/guinea-pig/_guinea-pig-layout/guinea-pig-layout.component';
import { GuineaPigAddProfileComponent } from './components-when-login/guinea-pig/guinea-pig-add-profile/guinea-pig-add-profile.component';
import { GuineaPigProfileComponent } from './components-when-login/guinea-pig/guinea-pig-profile/guinea-pig-profile.component';
import { GuineaPigRemoveProfileComponent } from './components-when-login/guinea-pig/guinea-pig-remove-profile/guinea-pig-remove-profile.component';
import { GuineaPigUpdateProfileComponent } from './components-when-login/guinea-pig/guinea-pig-update-profile/guinea-pig-update-profile.component';
import { UserLayoutComponent } from './components-when-login/user/_user-layout/user-layout.component';
import { UserChangePasswordComponent } from './components-when-login/user/user-change-password/user-change-password.component';
import { UserEditProfileComponent } from './components-when-login/user/user-edit-profile/user-edit-profile.component';
import { UserProfileComponent } from './components-when-login/user/user-profile/user-profile.component';
import { UserRemoveProfileComponent } from './components-when-login/user/user-remove-profile/user-remove-profile.component';
import { authGuard, NoLoginGuard } from './_guards/auth.guard';
import { GuineaPigCheckWeightsComponent } from './components-when-login/guinea-pig/guinea-pig-check-weights/guinea-pig-check-weights.component';

const routes: Routes = [
  {path: "", component: MainPageComponent,
    runGuardsAndResolvers: "always"
  },
  {path: "register", component: RegisterComponent, canActivate: [NoLoginGuard]},
  {path: "login", component: LoginComponent, canActivate: [NoLoginGuard]},
  {path: "good-products", component: GoodProductsComponent},
  {path: "bad-products", component: BadProductsComponent},
  {path: "info", component: BeforeBuyGuineaPigComponent},
  {
    path: "user",
    canActivate: [authGuard],
    component: UserLayoutComponent,
    children: [
      {path: "profile", component: UserProfileComponent},
      {path: "edit-profile", component: UserEditProfileComponent},
      {path: "remove-profile", component: UserRemoveProfileComponent},
      {path: "user-change-password", component: UserChangePasswordComponent},
    ]
  },
  {
  path: "guinea-pig",
  canActivate: [authGuard],
    component: GuineaPigLayoutComponent,
    children: [
      { path: 'profile', component: GuineaPigProfileComponent },
      { path: 'add-profile', component: GuineaPigAddProfileComponent },
      { path: 'update-profile', component: GuineaPigUpdateProfileComponent },
      { path: 'remove-profile', component: GuineaPigRemoveProfileComponent },
      { path: 'check-weights', component: GuineaPigCheckWeightsComponent },
    ]
  },
  {path: "**", component: MainPageComponent}
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
