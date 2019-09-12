import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthConfig, OAuthModule, OAuthStorage, ValidationHandler, JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from './auth.config';
import { MustBeAdminAuthGuard, MustBeLoggedAuthGuard } from './auth.guard';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    OAuthModule.forRoot()
  ],
  providers: [
    { provide: AuthConfig, useValue: authConfig },
    { provide: OAuthStorage, useValue: localStorage },
    { provide: ValidationHandler, useClass: JwksValidationHandler },
    MustBeLoggedAuthGuard,
    MustBeAdminAuthGuard
  ]
})
export class AuthenticationModule { }
