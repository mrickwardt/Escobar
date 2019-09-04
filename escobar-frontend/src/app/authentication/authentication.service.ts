import { Injectable } from '@angular/core';
import { AccountServiceProxy, UserDtoRegister } from 'src/swagger/swag-proxy';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private userCliente: AccountServiceProxy) {
  }

  public register(user: UserDtoRegister) {
    return this.userCliente.register(user);
  }
}
