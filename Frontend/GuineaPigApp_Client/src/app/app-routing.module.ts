import { NgModule } from '@angular/core';
import { MainPageComponent } from './components/public/main-page/main-page.component';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './components/public/register/register.component';
import { LoginComponent } from './components/public/login/login.component';
import { GoodProductsComponent } from './components/public/good-products/good-products.component';
import { BadProductsComponent } from './components/public/bad-products/bad-products.component';
import { InfoComponent } from './components/public/info/info.component';
import { GuineaPigLayoutComponent } from './components/auth/guinea-pig/_guinea-pig-layout/guinea-pig-layout.component';
import { GuineaPigAddProfileComponent } from './components/auth/guinea-pig/guinea-pig-add-profile/guinea-pig-add-profile.component';
import { GuineaPigProfileComponent } from './components/auth/guinea-pig/guinea-pig-profile/guinea-pig-profile.component';
import { GuineaPigRemoveProfileComponent } from './components/auth/guinea-pig/guinea-pig-remove-profile/guinea-pig-remove-profile.component';
import { GuineaPigUpdateProfileComponent } from './components/auth/guinea-pig/guinea-pig-update-profile/guinea-pig-update-profile.component';
import { UserLayoutComponent } from './components/auth/user/_user-layout/user-layout.component';
import { UserChangePasswordComponent } from './components/auth/user/user-change-password/user-change-password.component';
import { UserEditProfileComponent } from './components/auth/user/user-edit-profile/user-edit-profile.component';
import { UserProfileComponent } from './components/auth/user/user-profile/user-profile.component';
import { UserRemoveProfileComponent } from './components/auth/user/user-remove-profile/user-remove-profile.component';
import { authGuard, NoLoginGuard } from './guards/auth.guard';
import { GuineaPigCheckWeightsComponent } from './components/auth/guinea-pig/guinea-pig-check-weights/guinea-pig-check-weights.component';

const routes: Routes = [
  {path: "", component: MainPageComponent,
    runGuardsAndResolvers: "always"
  },
  {path: "register", component: RegisterComponent, canActivate: [NoLoginGuard]},
  {path: "login", component: LoginComponent, canActivate: [NoLoginGuard]},
  {path: "good-products", component: GoodProductsComponent},
  {path: "bad-products", component: BadProductsComponent},
  {path: "info", component: InfoComponent},
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
