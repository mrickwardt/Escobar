import { Injectable } from '@angular/core';
import { AccountServiceProxy, UserDtoRegister } from 'src/swagger/swag-proxy';

import { AuthenticationService } from '../authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private accountService: AccountServiceProxy,
    private authService: AuthenticationService) {
  }

  public register(user: UserDtoRegister) {
    return this.accountService.register(user);
  }

  getHistory() {
    return this.accountService.history(this.authService.user.id);
  }

  getUser() {
    return this.accountService.user();
  }
}
