import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OAuthModule } from 'angular-oauth2-oidc';
import { API_BASE_URL } from 'src/swagger/swag-proxy';
import { SwaggerModule } from 'src/swagger/swagger-module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    OAuthModule.forRoot(),
    SwaggerModule,
    AppRoutingModule
  ],
  providers: [
    { provide: API_BASE_URL, useValue: 'http://localhost:5000/'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
