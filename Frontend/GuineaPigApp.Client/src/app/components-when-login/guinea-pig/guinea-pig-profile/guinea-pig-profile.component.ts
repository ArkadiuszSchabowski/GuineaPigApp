import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/_models/user-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { BaseComponent } from 'src/app/_shared/base.component';

@Component({
  selector: 'app-guinea-pig-profile',
  templateUrl: './guinea-pig-profile.component.html',
  styleUrls: ['./guinea-pig-profile.component.scss']
})
export class GuineaPigProfileComponent extends BaseComponent implements OnInit {

  override cloudText: string = "Cieszę się, że sprawdzasz co u mnie!"

  currentTheme: boolean | undefined = undefined;
  model: UserDto | undefined = undefined;

  constructor(guineaPigService: GuineaPigService) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
  }
}
