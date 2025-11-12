import { ToastService } from './../services/toast-service';
import { inject } from '@angular/core';
import { AccountService } from './../services/account-service';
import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const accountService = inject(AccountService);
  const toast = inject(ToastService);

  if (accountService.currentUser()) {
    return true;
  } else {
    toast.error('Nah cannot go here');
    return false;
  }
};
