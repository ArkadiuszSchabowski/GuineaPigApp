import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuineaPigAddProfileComponent } from 'src/app/components/auth/guinea-pig/guinea-pig-add-profile/guinea-pig-add-profile.component';
import { GuineaPigCheckWeightsComponent } from 'src/app/components/auth/guinea-pig/guinea-pig-check-weights/guinea-pig-check-weights.component';
import { GuineaPigLayoutComponent } from 'src/app/components/auth/guinea-pig/_guinea-pig-layout/guinea-pig-layout.component';
import { GuineaPigProfileComponent } from 'src/app/components/auth/guinea-pig/guinea-pig-profile/guinea-pig-profile.component';
import { GuineaPigRemoveProfileComponent } from 'src/app/components/auth/guinea-pig/guinea-pig-remove-profile/guinea-pig-remove-profile.component';
import { GuineaPigUpdateProfileComponent } from 'src/app/components/auth/guinea-pig/guinea-pig-update-profile/guinea-pig-update-profile.component';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AngularMaterialModule } from '../angular-material/material.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    GuineaPigAddProfileComponent,
    GuineaPigCheckWeightsComponent,
    GuineaPigLayoutComponent,
    GuineaPigProfileComponent,
    GuineaPigRemoveProfileComponent,
    GuineaPigUpdateProfileComponent,
  ],
  imports: [CommonModule, AppRoutingModule, AngularMaterialModule, FormsModule],
  exports: [
    GuineaPigAddProfileComponent,
    GuineaPigCheckWeightsComponent,
    GuineaPigLayoutComponent,
    GuineaPigProfileComponent,
    GuineaPigRemoveProfileComponent,
    GuineaPigUpdateProfileComponent,
  ],
})
export class GuineaPigModule {}