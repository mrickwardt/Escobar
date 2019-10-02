import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import jwt_decode from 'jwt-decode';
import { Observable, Subject } from 'rxjs';
import { filter, map } from 'rxjs/operators';

import { IBaseUser } from './ICurrentUser';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private loginFailed: boolean;
  public finishedLoading = false;
  public finishedLoadingSubject = new Subject<boolean>();
  public user: IBaseUser;


  private checkValidAccessToken() {
    window.addEventListener('storage', (event) => {
      if (event.key !== 'access_token' && event.key !== null) {
        return;
      }
      // Not redirect to login if sign-out in another page
      if (this.oauthService.getAccessToken() && !this.oauthService.hasValidAccessToken()) {
        this.login();
      }
    });
  }

  constructor(private oauthService: OAuthService, private router: Router) {
    this.checkValidAccessToken();
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.getUser();
  }

  public runInitialLoginSequence(): Promise<boolean> {
    return this.oauthService.loadDiscoveryDocumentAndTryLogin()
      .then((isLoadingFinished) => {
        if (!this.oauthService.hasValidAccessToken()) {
          this.router.navigateByUrl('/user/register');
        }

        return isLoadingFinished;
      })
      .catch(() => {
        this.loginFailed = true;
        return Promise.resolve(false);
      })
      .finally(() => {
        this.finishedLoading = true;
        this.finishedLoadingSubject.next(true);
        this.getUser();
      });
  }

  public getUser() {
    const token = localStorage.getItem('access_token');
    if (token) {
      const userFromJWT = jwt_decode(token);
      this.user = {
        id: userFromJWT.UserId,
        name: userFromJWT.UserName,
      };
    }
    if (this.user) {
      return this.user;
    }
  }
  public getToken() {
    return localStorage.getItem('access_token');
  }

  public login(): void {
    this.oauthService.initImplicitFlow();
  }

  public logout(): void {
    this.oauthService.logOut();
  }

  public hasValidToken(): Observable<boolean> | boolean {
    if (this.finishedLoading) {
      return !this.loginFailed && this.oauthService.hasValidAccessToken();
    }
    return this.finishedLoadingSubject.pipe(
      filter(res => !!res),
      map(() => {
        return !this.loginFailed && this.oauthService.hasValidAccessToken();
      }));
  }

  public isLoggedIn() {
    return this.oauthService.hasValidAccessToken();
  }

  public hasValidAdminToken(): boolean | Observable<boolean> {
    if (this.finishedLoading) {
      return !this.loginFailed && this.oauthService.hasValidAccessToken();
    }
    return this.finishedLoadingSubject.pipe(
      filter(res => !!res),
      map(() => {
        return !this.loginFailed && this.oauthService.hasValidAccessToken();
      }));
  }
}
