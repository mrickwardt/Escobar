import { MediaMatcher } from '@angular/cdk/layout';
import { Component } from '@angular/core';

import { AuthenticationService } from './authentication/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  mobileQuery: MediaQueryList;

  constructor(media: MediaMatcher, public auth: AuthenticationService) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
  }
  logout() {
    this.auth.logout();
  }

}
