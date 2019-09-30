import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from './authentication.service';


@Injectable()
export class MustBeLoggedAuthGuard implements CanActivate {
  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const isLogged = this.authService.hasValidToken();
    if (!isLogged) {
      this.router.navigateByUrl('/user/register');
    }
    return isLogged;
  }
}

@Injectable()
export class MustBeAdminAuthGuard implements CanActivate {
  constructor(private authService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authService.hasValidAdminToken();
  }
}
