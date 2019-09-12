import { Injectable } from '@angular/core';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { Subject, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private loginFailed: boolean;
  public finishedLoading = false;
  public finishedLoadingSubject = new Subject<boolean>();


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

  constructor(private oauthService: OAuthService) {
    this.checkValidAccessToken();
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.runInitialLoginSequence();
  }

  public runInitialLoginSequence(): Promise<boolean> {
    return this.oauthService.loadDiscoveryDocumentAndLogin()
      .then((isLoadingFinished) => {
        return isLoadingFinished;
      })
      .catch(() => {
        this.loginFailed = true;
        return Promise.resolve(false);
      })
      .finally(() => {
        this.finishedLoading = true;
        this.finishedLoadingSubject.next(true);
      });
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
