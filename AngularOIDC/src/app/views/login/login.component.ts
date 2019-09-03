import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  constructor(private oauthService: OAuthService, private router: Router) {
  }

  public login() {
    this.oauthService.initImplicitFlow("login");
  }

  public register() {
    this.router.navigateByUrl('/register');
  }

}
