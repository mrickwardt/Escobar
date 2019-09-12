import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from './authentication.service';


@Injectable()
export class MustBeLoggedAuthGuard implements CanActivate {
  constructor(private authService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authService.hasValidToken();
  }
}

@Injectable()
export class MustBeAdminAuthGuard implements CanActivate {
  constructor(private authService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authService.hasValidAdminToken();
  }
} 