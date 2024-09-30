import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../_service/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  const currentUserToken = accountService.currentUserSource.getValue();
  if(currentUserToken){
    return true;
  } else{
    toastr.error("Odmowa dostępu! Zaloguj się, aby uzyskać dostęp!")
    router.navigateByUrl("/");
    return false;
  }
};
export const NoLoginGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  const currentUserToken = accountService.currentUserSource.getValue();
  if(!currentUserToken){
    return true;
  } else{
    toastr.error("Odmowa dostępu! Najpierw musisz się wylogować!")
    router.navigateByUrl("/");
    return false;
  }
};
