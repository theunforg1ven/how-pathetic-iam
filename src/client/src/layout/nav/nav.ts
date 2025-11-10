import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/core/account-service';
import { LoginCreds } from '../../types/user';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  protected accountService = inject(AccountService);
  protected creds: any = {} as LoginCreds;

  login() {
    this.accountService.login(this.creds).subscribe({
      next: (result) => {
        console.log(result);
        this.creds = {};
      },
      error: (error) => console.error(error.message),
    });
  }

  logout() {
    this.accountService.logout();
  }
}
