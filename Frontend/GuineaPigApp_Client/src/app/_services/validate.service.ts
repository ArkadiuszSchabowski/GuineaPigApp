import { Injectable } from '@angular/core';
import { RegisterUserDto } from '../models/add/register-user-dto';
import { ToastrService } from 'ngx-toastr';
import { ChangePasswordDto } from '../models/add/change-password-dto';

@Injectable({
  providedIn: 'root',
})
export class ValidateService {
  isCorrectName: boolean = false;
  isCorrectSurname: boolean = false;
  isCorrectCity: boolean = false;

  constructor(private toastr: ToastrService) {}

  validateWeightGuineaPig(weight: number | null): boolean {
    if (weight === null) {
      this.toastr.error('Nie wpisałeś wagi świnki!');
      return false;
    } else if (weight < 50 || weight > 3000) {
      this.toastr.error(
        'Waga świnki musi mieścić się w przedziale 50 do 3000 gramów!'
      );
      return false;
    }
    return true;
  }
}
