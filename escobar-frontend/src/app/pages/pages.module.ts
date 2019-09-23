import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';

import { AuthenticationService } from '../authentication/authentication.service';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';

@NgModule({
  declarations: [PagesComponent],
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    PagesRoutingModule
  ]
})
export class PagesModule {
  constructor(authService: AuthenticationService) {
    authService.runInitialLoginSequence();
  }
}
