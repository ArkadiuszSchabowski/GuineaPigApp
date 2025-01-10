import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuineaPigAddProfileComponent } from 'src/app/components-when-login/guinea-pig/guinea-pig-add-profile/guinea-pig-add-profile.component';
import { GuineaPigCheckWeightsComponent } from 'src/app/components-when-login/guinea-pig/guinea-pig-check-weights/guinea-pig-check-weights.component';
import { GuineaPigLayoutComponent } from 'src/app/components-when-login/guinea-pig/_guinea-pig-layout/guinea-pig-layout.component';
import { GuineaPigProfileComponent } from 'src/app/components-when-login/guinea-pig/guinea-pig-profile/guinea-pig-profile.component';
import { GuineaPigRemoveProfileComponent } from 'src/app/components-when-login/guinea-pig/guinea-pig-remove-profile/guinea-pig-remove-profile.component';
import { GuineaPigUpdateProfileComponent } from 'src/app/components-when-login/guinea-pig/guinea-pig-update-profile/guinea-pig-update-profile.component';

@NgModule({
  declarations: [
    GuineaPigAddProfileComponent,
    GuineaPigCheckWeightsComponent,
    GuineaPigLayoutComponent,
    GuineaPigProfileComponent,
    GuineaPigRemoveProfileComponent,
    GuineaPigUpdateProfileComponent,
  ],
  imports: [CommonModule],
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
