import { Injectable } from '@angular/core';
import { AccountServiceProxy, HistoryServiceProxy, UserEditDto, UserRegisterDto } from 'src/swagger/swag-proxy';

import { AuthenticationService } from '../../../authentication/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private historyServiceProxy: HistoryServiceProxy,
    private accountServiceProxy: AccountServiceProxy,
    private authService: AuthenticationService) {
  }

  register(user: UserRegisterDto) {
    return this.accountServiceProxy.register(user);
  }
  edit(user: UserEditDto) {
    return this.accountServiceProxy.edit(user);
  }

  getHistory() {
    return this.historyServiceProxy.userHistory(this.authService.user.id);
  }

  getUser() {
    return this.accountServiceProxy.get();
  }
}
